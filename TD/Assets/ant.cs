using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ant : MonoBehaviour {

	public tile start;
	public tile end;
	public tile[] route;
	public game g;
	public tile previous;
	public tile next;
	public int progress;
	public int maxProgress;
	public int routeIndex;
	public int health = 1;
	public int maxHealth = 1;
	public int identIndex = 0;
	public int bridgeCount;
	public int spacing;

	public bar healthBar;
	public Transform Bar;
	public Transform Beam;



	Vector3 zboy = new Vector3(0, 0, -.01f);
	// Use this for initialization
	void Start () {
		this.transform.localScale = new Vector3(.15f, .15f, 1);

		healthBar = Instantiate(Bar, transform.position, Quaternion.identity).gameObject.GetComponent<bar>();
		healthBar.parent = this;

		transform.position = new Vector3(transform.position.x, transform.position.y, -.01f);
		g = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
		start = g.start;
		end = g.end;
		route = new tile[200];//max route length < width * height < 200

		//keep all creeps on the same route for the round
		if (g.route[0] == null)
		{
			g.FindPath(start, end, true);
		}
		
		route = g.route;
		progress = maxProgress;
		routeIndex = 1;
		previous = route[0];
		next = route[1];
		orient();
		bridgeCount = 0;
	}

	// Update is called once per frame
	void Update ()
	{
		if (g.paused)
		{
			return;
		}

		route = g.route;
		if (health <= 0)
		{
			progress = 100000;
			g.insertCreep(identIndex);
			kill();
			return;
		}

		if (maxProgress > g.spawnMaxProgress * 10)
		{
			maxProgress = g.spawnMaxProgress * 10;
		}

		//choose sprite to display
		this.GetComponent<SpriteRenderer>().sprite = setSprite(maxProgress, spacing, g.lowSpeed, g.highSpeed, g.lowSpace, g.highSpace, g.enemySprites, g.timeFactor);



		//movement of creeps
		if (progress <= 0)
		{
			if (next == end)
			{
				g.lives -= 1;
				Destroy(gameObject);
				return;
			}
			progress = maxProgress;
			//check for webbing
			if (next.webbed > 0)
			{
				progress += next.webbed;
				g.insertCreep(identIndex);
				//create visual beams to tesla towers
				beam temp = Instantiate(Beam, transform.position, Quaternion.identity).gameObject.GetComponent<beam>();
				temp.target = this.transform;
				temp.damage = 0;
				temp.enemy = this;
				temp.beamType = 0;
				temp.GetComponent<SpriteRenderer>().sprite = temp.weirdBeam;
				temp.lifetime = progress;

				beam temp2 = Instantiate(Beam, transform.position, Quaternion.identity).gameObject.GetComponent<beam>();
				temp2.target = this.transform;
				temp2.damage = 0;
				temp2.enemy = this;
				temp2.beamType = 0;
				temp2.GetComponent<SpriteRenderer>().sprite = temp.weirdBeam;
				temp2.lifetime = progress;

				//north-south or east-west
				if (next.north != null && next.north.status == next.FILLED && (next.north.towerType == next.TESLA || next.north.towerType == next.TESLA2 || next.north.towerType == next.TESLA3 || next.north.towerType == next.TESLACOIL))
				{
					temp.source = next.north.transform;
					temp2.source = next.south.transform;
				}
				else
				{
					temp.source = next.east.transform;
					temp2.source = next.west.transform;
				}
				temp.setBeam(0);
				temp2.setBeam(0);

			}
			previous = next;
			next = route[routeIndex + 1];
			routeIndex += 1;
			orient();
		}

		if (progress > 0)
		{
			Vector3 step = (next.transform.position + zboy - transform.position) / progress;
			transform.Translate(step, Space.World);
			progress -= 1;
		}

		
	}

	
	void kill()
	{
		g.gold += 1;
		Destroy(gameObject);
	}


	public static Sprite setSprite(int speed, int space, int lowSpeed2, int highSpeed2, int lowSpace2, int highSpace2, Sprite[] sprites2, double timingFactor)
	{
		int speedIndex = 1;
		int spaceIndex = 1;

		if (speed < lowSpeed2 * 2/timingFactor)
		{
			speedIndex = 0;
		}
		else if (speed > highSpeed2 * 2 / timingFactor)
		{
			speedIndex = 2;
		}

		if (space < lowSpace2 * 2 / timingFactor)
		{
			spaceIndex = 0;
		}
		else if (space > highSpace2 * 2 / timingFactor)
		{
			spaceIndex = 2;
		}

		int spriteChoice = 3 * speedIndex + spaceIndex;
		return sprites2[spriteChoice];
	}

	public void orient()
	{
		int direction = 0;
		if (next == previous.east)
		{
			direction = 1;
		}
		else if(next == previous.south)
		{
			direction = 2;
		}
		else if(next == previous.west)
		{
			direction = 3;
		}

		transform.rotation = Quaternion.identity;
		gameObject.transform.Rotate(Vector3.forward, direction * -90f);
	}
}
