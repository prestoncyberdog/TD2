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
	public int BEAM = 4;
	public int COIL = 5;
	public int TESLA = 6;
	public int BRIDGE = 7;
	public int TAG = 8;
	public int beamDirection;
	public int maxCooldown;
	public int cooldown = 0;
	public int damage;
	public double range;
	public int[] cooldowns;
	public double[] ranges;
	public int[] damages;
	public float splashRadius;
	public List<ant> targeting;
	public int webbed;
	public bool vertBridge;
	public bool unchanged;
	public tile bridgeStart;
	public tile bridgeEnd;

	public Sprite emptySprite;
	public Sprite startSprite;
	public Sprite endSprite;
	public Sprite[] towerSprites = new Sprite[20];
	public int[] towerCosts;
	public bool refund;

	public Transform Missile;
	public Transform Beam;


	// Use this for initialization 
	void Start() {
		towerCosts = new int[] {1, 10, 20, 30, 50, 50, 40, 50, 50};
		cooldowns = new int[] {0, 20, 45, 150, 80, 0, 0, 150, 17};
		ranges = new double[] {0, 5.2, 3.2, 1.8, 0, 1.8, 0, 0, 2.2};
		damages = new int[] { 0, 1, 1, 5, 5, 1, 30, 0, 1};
		splashRadius = 3.2f;//for splash tower only
		g = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
		status = EMPTY;
		//this.GetComponent<SpriteRenderer>().sprite = emptySprite;

		beamDirection = 0;//defaults to up
		targeting = new List<ant>();
		webbed = 0;
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
						temp.type = 0;
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
							if (g.creeps[j] != null && (g.creeps[j].transform.position - currPos).magnitude < splashRadius)
							{
								hits.Add(g.creeps[j]);
							}
						}
						for (int k = 0;k<hits.Count;k++)
						{
							beam temp = Instantiate(Beam, transform.position, Quaternion.identity).gameObject.GetComponent<beam>();
							temp.transform.position = new Vector3(100, 100, 0);
							temp.beamType = 0;
							temp.lifetime = 5;
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
								temp.setBeam(temp.beamType);
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
							if (g.creeps[i].next != null && ((g.creeps[i].next == g.end || (g.creeps[i].next.transform.position - transform.position).magnitude > range) || g.creeps[i].maxHealth > damage))
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
							temp.beamType = 0;
							temp.GetComponent<SpriteRenderer>().sprite = temp.whiteBeam;
							temp.lifetime = 5;
							temp.setBeam(temp.beamType);
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
		else if (status == FILLED && towerType == BEAM)
		{
			if (cooldown == 0)
			{
				bool fired = false;

				//fire if enemies on path
				tile lastTile;
				tile beamPathTile = this;
				
				while (true)//go through tiles in line
				{
					int i = 0;
					while (i < g.creeps.Length)
					{
						//hits creeps if they are going to the tile
						if (g.creeps[i] != null && ((g.creeps[i].next == beamPathTile && g.creeps[i].progress <= g.creeps[i].maxProgress / 2) || (g.creeps[i].previous == beamPathTile && g.creeps[i].progress >= g.creeps[i].maxProgress / 2)) )
						{
							g.creeps[i].health -= damage;
							fired = true;
						}
						i++;
					}


					//get next tile in line
					lastTile = beamPathTile;
					if (beamDirection == 0)//north
					{
						beamPathTile = beamPathTile.north;
					}
					else if (beamDirection == 1)//east
					{
						beamPathTile = beamPathTile.east;
					}
					else if (beamDirection == 2)//south
					{
						beamPathTile = beamPathTile.south;
					}
					else if (beamDirection == 3)//west
					{
						beamPathTile = beamPathTile.west;
					}

					//check if end of beam
					if (beamPathTile == null || beamPathTile.status != EMPTY)
					{
						break;
					}
				}
				if (fired)
				{
					cooldown = maxCooldown;
					//create beam
					beam temp = Instantiate(Beam, transform.position, Quaternion.identity).gameObject.GetComponent<beam>();
					temp.source = this.transform;
					temp.target = lastTile.transform;
					temp.beamType = 1;
					temp.GetComponent<SpriteRenderer>().sprite = temp.redBeam;
					temp.setBeam(temp.beamType);
					temp.lifetime = 8;

				}
			}
			else
			{
				cooldown -= 1;
			}
		}
		else if (status == FILLED && towerType == COIL)
		{
			for (int i=0;i<g.creeps.Length;i++)
			{
				if (g.creeps[i] != null && ((g.creeps[i].transform.position - transform.position).magnitude <= range) && g.creeps[i].next != g.creeps[i].end)
				{
					bool newTarget = true;
					for (int j = 0; j < targeting.Count; j++)
					{
						if (g.creeps[i] == targeting[j])
						{
							newTarget = false;
						}
					}

					if (newTarget)
					{
						targeting.Add(g.creeps[i]);
						//create beam
						beam temp = Instantiate(Beam, transform.position, Quaternion.identity).gameObject.GetComponent<beam>();
						temp.source = this.transform;
						temp.target = g.creeps[i].transform;
						temp.enemy = g.creeps[i];
						temp.damage = damage;
						temp.maxRange = range;
						temp.beamType = 2;
						temp.lifetime = 0;
						temp.GetComponent<SpriteRenderer>().sprite = temp.blueBeam;
						temp.setBeam(temp.beamType);
					}
				}
			}
		}
		else if (status == FILLED && towerType == TESLA)
		{
			//do nothing 
		}
		else if (status == FILLED && towerType == BRIDGE)
		{
			if (cooldown == 0)
			{
				if (g.waveActive == false)
				{
					configureBridge();
				}
				if (bridgeStart != null)
				{
					//there is a valid bridge to work with
				
				
					bool fired = false;
					//choose enemy
					for (int i = 0; i < g.creeps.Length; i++)
					{
						if (g.creeps[i] != null && g.creeps[i].next == bridgeStart && g.creeps[i].progress <= 1 && g.creeps[i].bridgeCount < 3)
						{
							fired = true;
							//move target ant
							int endIndex = 0;
							for (int j = 0; j < g.route.Length; j++)
							{
								if (g.route[j] != null && g.route[j] == bridgeEnd)
								{
									endIndex = j;
									break;
								}
							}
							g.creeps[i].bridgeCount++;
							g.creeps[i].next = bridgeEnd;
							g.creeps[i].previous = bridgeStart;
							g.creeps[i].routeIndex = endIndex;
							g.creeps[i].progress = g.creeps[i].maxProgress * 2;
							g.insertCreep(i);//resorts creep
						}
					}
					if (fired)
					{
						cooldown = maxCooldown;
					}
				}
			}
			else
			{
				cooldown -= 1;
			}
		}
		if (status == FILLED && towerType == TAG)
		{
			//same behavior as missile except for slow
			if (cooldown == 0)
			{
				bool fired = false;
				//choose enemy
				int targetIndex = 0;
				int minProgress = 1000;
				for (int i = 0; i < g.creeps.Length; i++)
				{
					if (g.creeps[i] != null && (g.creeps[i].transform.position - transform.position).magnitude <= range)
					{

						if (g.creeps[i].maxProgress < minProgress)
						{
							targetIndex = i;
							minProgress = g.creeps[i].maxProgress;
						}
						
						fired = true;
					}
				}
				if (fired)
				{
					cooldown = maxCooldown;
					//shock enemy
					beam temp = Instantiate(Beam, transform.position, Quaternion.identity).gameObject.GetComponent<beam>();
					temp.source = this.transform;
					temp.damage = damage;
					temp.target = g.creeps[targetIndex].transform;
					temp.enemy = g.creeps[targetIndex];
					temp.beamType = 3;
					temp.GetComponent<SpriteRenderer>().sprite = temp.purpleBeam;
					temp.lifetime = 5;
					temp.setBeam(temp.beamType);

					g.creeps[targetIndex].maxProgress += damage * 1;
					g.creeps[targetIndex].progress += damage * 1;
					g.insertCreep(g.creeps[targetIndex].identIndex);
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
		button sample2 = FindObjectOfType<button>();
		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
		{
			g.lastTowerSelected = this;
		}
		//prevents building during waves on creep paths
			if (this == g.start || this == g.end)
		{
			return;
		}
		else if (g.waveActive == true)
		{
			
			for (int i = 0; i < g.route.Length; i++)
			{
				if (g.route[i] == g.end)
				{
					break;
				}
				else if (g.route[i] == this)
				{
					return;
				}
			}
			

			//you cant move boosts during a round
			if (g.currButtonActive == sample2.DBoostCode || g.currButtonActive == sample2.RBoostCode || g.currButtonActive == sample2.SBoostCode)
			{
				return;
			}
		}

		//allows you to build whatever you have selected
		if (Input.GetMouseButtonDown(0))
		{
			//handle boost buttons
			//for each one, must remove boost from any other tower first
			
			if (g.currButtonActive == sample2.DBoostCode)
			{
				if (status == FILLED)
				{
					if (g.damageBoostedTower != null)
					{
						g.damageBoostedTower.damage = damages[g.damageBoostedTower.towerType];
						if (g.damageBoostedTower.towerType == g.damageBoostedTower.TESLA)
						{
							g.damageBoostedTower.makeWebs();
						}
					}
					g.damageBoostedTower = this;
					g.damageIndicator.transform.position = transform.position + new Vector3(-.3f, .3f, -.001f);
					damage = (int)(damage * g.damageBoost);
					if (towerType == TESLA)
					{
						makeWebs();
					}
				}
			}
			else if (g.currButtonActive == sample2.RBoostCode)
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
			else if (g.currButtonActive == sample2.SBoostCode)
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
				//bool valid = g.CheckLegal();
				tile[] routeHolder = new tile[200]; 
				for (int i =0;i<routeHolder.Length;i++)
				{
					routeHolder[i] = g.route[i];
				}
				int valid = g.FindPath(g.start, g.end);
				for (int i = 0; i < routeHolder.Length; i++)
				{
					g.route[i] = routeHolder[i];
				}
				if (valid == 0)
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

				//handle beam
				if (towerType == BEAM)
				{
					unchanged = true;
				}

				//handle tesla
				if (towerType == TESLA)
				{
					makeWebs();
				}

				//handle bridge
				if (towerType == BRIDGE)
				{
					configureBridge();
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
				if (towerType == TESLA)
				{
					if (north != null)
					{
						north.webbed = 0;

					}
					if (south != null)
					{
						south.webbed = 0;

					}
					if (east != null)
					{
						east.webbed = 0;

					}
					if (west != null)
					{
						west.webbed = 0;

					}
				}
				else if (towerType == BEAM)
				{
					orient(0);
				}
				else if (towerType == BRIDGE)
				{
					bridgeStart = null;
					bridgeEnd = null;
				}
				if (g.waveActive == false)
				{
					g.FindPath(g.start, g.end);
				}
			}
		}
		
	}

	public void makeWebs()
	{
		if (north != null && north.north != null && north.north.status == FILLED && north.north.towerType == TESLA)
		{
			north.webbed = Mathf.Max(north.north.damage, damage);
		}
		if (south != null && south.south != null && south.south.status == FILLED && south.south.towerType == TESLA)
		{
			south.webbed = Mathf.Max(south.south.damage, damage);
		}
		if (east != null && east.east != null && east.east.status == FILLED && east.east.towerType == TESLA)
		{
			east.webbed = Mathf.Max(east.east.damage, damage);
		}
		if (west != null && west.west != null && west.west.status == FILLED && west.west.towerType == TESLA)
		{
			west.webbed = Mathf.Max(west.west.damage, damage);
		}
	}

	public void configureBridge()
	{
		if (g.waveActive == false)
		{
			g.FindPath(g.start, g.end);
		}
		bridgeStart = null;
		bridgeEnd = null;
		int eIndex = -1;
		int wIndex = -1;
		int nIndex = -1;
		int sIndex = -1;
		if (east != null && west != null)
		{
			for (int i = 0; i<g.route.Length; i++)
			{
				if (g.route[i] != null && g.route[i] == east)
				{
					eIndex = i;
				}
				else if (g.route[i] != null && g.route[i] == west)
				{
					wIndex = i;
				}
			}
			if (eIndex >= 0 && wIndex >= 0)
			{
				//here we make a bridge
				vertBridge = false;
				transform.rotation = Quaternion.identity;
				transform.Rotate(new Vector3(0, 0, 0));
				if (eIndex > wIndex)
				{
					bridgeStart = east;
					bridgeEnd = west;
				}
				else
				{
					bridgeStart = west;
					bridgeEnd = east;
				}

			}
		}
		if (north != null && south != null)
		{
			for (int i = 0; i < g.route.Length; i++)
			{
				if (g.route[i] != null && g.route[i] == north)
				{
					nIndex = i;
				}
				else if (g.route[i] != null && g.route[i] == south)
				{
					sIndex = i;
				}
			}
			if (nIndex >= 0 && sIndex >= 0 && ((Mathf.Abs(nIndex - sIndex) > Mathf.Abs(eIndex - wIndex)) || bridgeStart == null))
			{
				//here we make a bridge
				vertBridge = true;
				transform.rotation = Quaternion.identity;
				transform.Rotate(new Vector3(0, 0, 90));
				if (nIndex > sIndex)
				{
					bridgeStart = north;
					bridgeEnd = south;
				}
				else
				{
					bridgeStart = south;
					bridgeEnd = north;
				}
			}
		}
	}

	public void orient (int direction)
	{
		transform.rotation = Quaternion.identity;
		transform.Rotate(new Vector3(0, 0, -90 * direction));
		beamDirection = direction;
		unchanged = false;
	}
}
