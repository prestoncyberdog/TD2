using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour {
	public game g;
	public tile north;
	public tile south;
	public tile east;
	public tile west;
	public bool visited;//for path planning
	public tile prev;//for path planning
	public int dist;//for path planning
	public int EMPTY = 0;
	public int FILLED = 1;
	public int BLOCKED = 2;
	public int status = 0;
	public int towerType = 0;
	public int BLOCKER = 0;
	public int MISSILE = 1;
	public int SPLASH = 2;
	public int SHOCK = 3;
	public int maxCooldown;
	public int cooldown = 0;
	public int damage;
	public double range;
	public int[] cooldowns;
	public double[] ranges;
	public int[] damages;

	public Sprite emptySprite;
	public Sprite startSprite;
	public Sprite endSprite;
	public Sprite[] towerSprites = new Sprite[20];
	public int[] towerCosts;
	public bool refund;

	public Transform Missile;
	public Transform Beam;
	public List<beam> beams;


	// Use this for initialization 
	void Start() {
		towerCosts = new int[] {1, 10, 20, 30};
		cooldowns = new int[] {0, 20, 50, 120};
		ranges = new double[] {0, 5.2, 3.2, 1.8};
		damages = new int[] { 0, 1, 1, 5};
		g = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
		status = EMPTY;
		this.GetComponent<SpriteRenderer>().sprite = emptySprite;

		beams = new List<beam>();
	}

	// Update is called once per frame
	void Update() {
		if (status == FILLED && towerType == MISSILE)
		{
			if (cooldown == 0)
			{
				bool fired = false;
				//choose enemy
				for (int i = 0; i < g.creeps.Length; i++)
				{
					if (g.creeps[i] != null && (g.creeps[i].transform.position - transform.position).magnitude <= range)
					{
						//fire missile (enemy)
						missile temp = Instantiate(Missile, transform.position, Quaternion.identity).gameObject.GetComponent<missile>();
						temp.target = g.creeps[i];
						temp.damage = damage;
						fired = true;
						break;
					}
				}
				if (fired)
				{
					cooldown = maxCooldown;
				}
			}
			else
			{
				cooldown -= 1;
			}

		}
		else if (status == FILLED && towerType == SPLASH)
		{
			if (cooldown == 0)
			{
				bool fired = false;
				//choose enemy
				for (int i = 0; i < g.creeps.Length; i++)
				{
					if (g.creeps[i] != null && (g.creeps[i].transform.position - transform.position).magnitude <= range)
					{
						//shock enemy
						List<ant> hits = new List<ant>();
						Vector3 currPos = g.creeps[i].transform.position;
						for (int j=0;j<g.creeps.Length;j++)
						{
							if (g.creeps[j] != null && (g.creeps[j].transform.position - currPos).magnitude < 2.2)
							{
								hits.Add(g.creeps[j]);
							}
						}
						for (int k = 0;k<hits.Count;k++)
						{
							beam temp = Instantiate(Beam, transform.position, Quaternion.identity).gameObject.GetComponent<beam>();
							temp.transform.position = new Vector3(100, 100, 0);
							if (hits[k] != g.creeps[i])
							{
								temp.source = g.creeps[i].transform;
							}
							else
							{
								temp.source = this.transform;
								temp.damage += damage;
							}

							if (g.creeps[i].health > 0)
							{
								temp.target = hits[k].transform;
								temp.damage += damage;
								temp.enemy = hits[k];
								temp.setBeam();
							}

						}
						fired = true;
						break;

					}
				}
				if (fired)
				{
					cooldown = maxCooldown;
				}
			}
			else
			{
				cooldown -= 1;
			}
		}
		else if (status == FILLED && towerType == SHOCK)
		{
			if (cooldown == 0)
			{
				bool fired = false;
				bool decided = false;
				//choose all enemies
				for (int i = 0; i < g.creeps.Length; i++)
				{
					if (g.creeps[i] != null && (g.creeps[i].transform.position - transform.position).magnitude <= range)
					{
						//if front enemy will soon be out of range, fire
						if (decided == false)
						{
							if (g.creeps[i].next != null && (g.creeps[i].next == g.end || (g.creeps[i].next.transform.position - transform.position).magnitude > range))
							{
								fired = true;
							}
							decided = true;
						}

						if (fired)
						{
							//shock enemy
							beam temp = Instantiate(Beam, transform.position, Quaternion.identity).gameObject.GetComponent<beam>();
							temp.source = this.transform;
							temp.damage += damage;
							temp.target = g.creeps[i].transform;
							temp.enemy = g.creeps[i];
							temp.GetComponent<SpriteRenderer>().sprite = temp.whiteBeam;

							temp.setBeam();
						}


					}
				}
				if (fired)
				{
					cooldown = maxCooldown;
				}
			}
			else
			{
				cooldown -= 1;
			}
		}
	}

	void OnMouseOver()
	{
		//prevents building during waves on creep paths
		if (this == g.start || this == g.end)
		{
			return;
		}
		else if (g.waveActive == true)
		{
			//any ant will do
			ant sample = FindObjectOfType<ant>();
			if (sample != null)
			{
				for (int i = 0; i < sample.route.Length; i++)
				{
					if (sample.route[i] == g.end)
					{
						break;
					}
					else if (sample.route[i] == this)
					{
						return;
					}
				}
			}
		}

		//allows you to build whatever you have selected
		if (Input.GetMouseButtonDown(0))
		{
			//handle boost buttons
			//for each one, must remove boost from any other tower first
			if (g.currButtonActive == 4)
			{
				if (status == FILLED)
				{
					if (g.damageBoostedTower != null)
					{
						g.damageBoostedTower.damage = damages[g.damageBoostedTower.towerType];
					}
					g.damageBoostedTower = this;
					g.damageIndicator.transform.position = transform.position + new Vector3(-.3f, .3f, -.001f);
					damage = (int)(damage * g.damageBoost);
				}
			}
			else if (g.currButtonActive == 5)
			{
				if (status == FILLED)
				{
					if (g.rangeBoostedTower != null)
					{ 
						g.rangeBoostedTower.range = ranges[g.rangeBoostedTower.towerType];
					}
					g.rangeBoostedTower = this;
					g.rangeIndicator.transform.position = transform.position + new Vector3(.3f, .3f, -.001f);
					range = range + g.rangeBoost;
				}
			}
			else if (g.currButtonActive == 6)
			{
				if (status == FILLED)
				{
					if (g.speedBoostedTower != null)
					{
						g.speedBoostedTower.maxCooldown = cooldowns[g.speedBoostedTower.towerType];
					}
					g.speedBoostedTower = this;
					g.speedIndicator.transform.position = transform.position + new Vector3 (-.3f, -.3f, -.001f);
					maxCooldown = (int)(maxCooldown * g.speedBoost);
				}
			}
			//handle tower buttons
			else if (status == EMPTY && (g.gold - towerCosts[g.currButtonActive] >= 0))
			{
				status = FILLED;

				//if the blocks the path, do nothing
				bool valid = g.CheckLegal();
				if (valid == false)
				{
					status = EMPTY;
					return;
				}

				towerType = g.currButtonActive;
				this.GetComponent<SpriteRenderer>().sprite = towerSprites[g.currButtonActive];
				g.gold -= towerCosts[towerType];
				damage = damages[towerType];
				range = ranges[towerType];
				maxCooldown = cooldowns[towerType];
				if (g.waveActive == false)
				{
					refund = true;
				}
			}
		}
		else if (Input.GetMouseButtonDown(1))//delete anything
		{
			if (status == FILLED)
			{
				status = EMPTY;
				if(g.damageBoostedTower == this)
				{
					g.damageIndicator.transform.position = new Vector3(100, 100, 0);
				}
				if (g.rangeBoostedTower == this)
				{
					g.rangeIndicator.transform.position = new Vector3(100, 100, 0);
				}
				if (g.speedBoostedTower == this)
				{
					g.speedIndicator.transform.position = new Vector3(100, 100, 0);
				}
				this.GetComponent<SpriteRenderer>().sprite = emptySprite;
				if (refund)
				{
					g.gold += towerCosts[towerType];
				}
			}
		}
		
	}
}
