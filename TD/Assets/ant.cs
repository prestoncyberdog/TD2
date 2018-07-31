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

	public bar healthBar;
	public Transform Bar;
	public Transform Beam;




	Vector3 zboy = new Vector3(0, 0, -.01f);
	// Use this for initialization
	void Start () {
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
				if (next.north != null && next.north.status == next.FILLED && (next.north.towerType == next.TESLA || next.north.towerType == next.TESLA2 || next.north.towerType == next.TESLA3))
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
		}
		
		if (progress > 0)
		{
			Vector3 step = (next.transform.position + zboy - transform.position) / progress;
			transform.Translate(step);
			progress -= 1;
		}

		
	}

	
	void kill()
	{
		g.gold += 1;
		Destroy(gameObject);
	}

	//this code has been improved upon elsewhere but is still here in case i find a bug later
	/*
	//plan a path from location 'from' to location 'to'
	//returns 1 if successful, 0 if no path was found
	//updates route as it goes
	public int FindPath (tile from, tile to)
	{
		tile current;
		//set all tiles to unvisited, prev to null, dist to 10000
		for (int i=0;i<g.tiles.Length;i++)
		{
			g.tiles[i].visited = false;
			g.tiles[i].prev = null;
			g.tiles[i].dist = 10000;
		}

		//enqueue from
		Queue q = new Queue();
		from.dist = 0;
		from.visited = true;
		q.Enqueue(from);

		//while queue is not empty
		while (q.Count > 0)
		{
			current = (tile)q.Dequeue();//dequeue, check for end, return 1 if so
			if (current == to)
			{
				//fill in correct path in route
				//wont change route if no path is found
				tile backTemp = current;
				while (backTemp != null)
				{
					route[backTemp.dist] = backTemp;
					backTemp = backTemp.prev;
				}

				return 1;
			}

			//enqueue valid neighbors
			if (current.north != null && current.north.visited == false && current.north.status == current.EMPTY)
			{
				current.north.visited = true;
				current.north.dist = current.dist + 1;
				current.north.prev = current;
				q.Enqueue(current.north);
			}
			if (current.south != null && current.south.visited == false && current.south.status == current.EMPTY)
			{
				current.south.visited = true;
				current.south.dist = current.dist + 1;
				current.south.prev = current;
				q.Enqueue(current.south);
			}
			if (current.east != null && current.east.visited == false && current.east.status == current.EMPTY)
			{
				current.east.visited = true;
				current.east.dist = current.dist + 1;
				current.east.prev = current;
				q.Enqueue(current.east);
			}
			if (current.west != null && current.west.visited == false && current.west.status == current.EMPTY)
			{
				current.west.visited = true;
				current.west.dist = current.dist + 1;
				current.west.prev = current;
				q.Enqueue(current.west);
			}
		}
		
		//if queue runs out without finding end, return 0
		return 0;
	}*/
}
