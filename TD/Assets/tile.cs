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
	public int MISSILE2 = 9;
	public int SPLASH2 = 10;
	public int SHOCK2 = 11;
	public int BEAM2 = 12;
	public int COIL2 = 13;
	public int TESLA2 = 14;
	public int BRIDGE2 = 15;
	public int TAG2 = 16;
	public int MISSILE3 = 17;
	public int SPLASH3 = 18;
	public int SHOCK3 = 19;
	public int BEAM3 = 20;
	public int COIL3 = 21;
	public int TESLA3 = 22;
	public int BRIDGE3 = 23;
	public int TAG3 = 24;
	public int MISSILE4 = 25;
	public int SHOCKSPLASH = 26;
	public int TAGBEAM = 27;
	public int TESLACOIL = 28;
	public int beamDirection;
	public int maxCooldown;
	public int cooldown = 0;
	public int damage;
	public double range;
	public int effect;
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
	public Sprite blockedSprite;
	public Sprite startSprite;
	public Sprite endSprite;
	public Sprite[] towerSprites = new Sprite[50];
	public int[] towerCosts;
	public bool refund;
	public bool blockerRefund;

	public Transform Missile;
	public Transform Beam;


	// Use this for initialization 
	void Start() {
		g = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
		towerCosts = g.towerCosts;
		cooldowns = g.cooldowns;
		ranges = g.ranges;
		damages = g.damages;
		effect = 1;
		if (status != BLOCKED)
		{
			status = EMPTY;
		}
		//this.GetComponent<SpriteRenderer>().sprite = emptySprite;

		beamDirection = 0;//defaults to up
		targeting = new List<ant>();
		webbed = 0;
	}

	// Update is called once per frame
	void Update() {

		if (g.paused)
		{
			return;
		}

		if (status == FILLED && (towerType == MISSILE || towerType == MISSILE2 || towerType == MISSILE3 || towerType == MISSILE4))
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
		else if (status == FILLED && (towerType == SPLASH || towerType == SPLASH2 || towerType == SPLASH3 || towerType == SHOCKSPLASH))
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
						for (int j=0;j<g.creeps.Length;j++)//find enemis to splash
						{
							if (g.creeps[j] != null && (g.creeps[j].transform.position - currPos).magnitude < (range + (effect-1.0)*0.5))
							{
								hits.Add(g.creeps[j]);
							}
						}
						for (int k = 0;k<hits.Count;k++)
						{
							beam temp = Instantiate(Beam, transform.position, Quaternion.identity).gameObject.GetComponent<beam>();
							temp.transform.position = new Vector3(100, 100, 0);
							temp.beamType = 0;
							temp.lifetime = (int)(12.0/g.timeFactor);
							if (towerType == SHOCKSPLASH)
							{
								temp.GetComponent<SpriteRenderer>().sprite = temp.gwBeam;
							}

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
						if (towerType != SHOCKSPLASH)
						{
							break;
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
		else if (status == FILLED && (towerType == SHOCK || towerType == SHOCK2 || towerType == SHOCK3))
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
							temp.lifetime = (int)(12.0 / g.timeFactor);
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
		else if (status == FILLED && (towerType == BEAM || towerType == BEAM2 || towerType == BEAM3 || towerType == TAGBEAM))
		{
			if (cooldown == 0)
			{
				bool fired = false;

				//fire if enemies on path
				tile lastTile;
				tile beamPathTile = this;
				int numWalls = effect - 1;//you can shoot through effect-1 walls
				while (true)//go through tiles in line
				{
					int i = 0;
					while (i < g.creeps.Length)
					{
						//hits creeps if they are going to the tile
						if (g.creeps[i] != null && ((g.creeps[i].next == beamPathTile && g.creeps[i].progress <= g.creeps[i].maxProgress / 2) || (g.creeps[i].previous == beamPathTile && g.creeps[i].progress >= g.creeps[i].maxProgress / 2)) )
						{
							g.creeps[i].health -= damage;
							if (towerType == TAGBEAM)
							{
								g.creeps[i].maxProgress += effect * g.effects[towerType] * 1;
								g.creeps[i].progress += effect * g.effects[towerType] * 1;
								g.insertCreep(g.creeps[i].identIndex);
							}
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
					if (beamPathTile == null)
					{
						break;
					}
					else if (beamPathTile.status != EMPTY)
					{
						if(numWalls > 0)
						{
							numWalls--;
						}
						else
						{
							break;
						}
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
					if (towerType == TAGBEAM)
					{
						temp.GetComponent<SpriteRenderer>().sprite = temp.rpBeam;
					}
					temp.setBeam(temp.beamType);
					temp.lifetime = (int)(16.0 / g.timeFactor);

				}
			}
			else
			{
				cooldown -= 1;
			}
		}
		else if (status == FILLED && (towerType == COIL || towerType == COIL2 || towerType == COIL3 || towerType == TESLACOIL))
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
						temp.effect = effect;
						temp.damage = damage;
						temp.maxRange = range;
						temp.beamType = 2;
						temp.lifetime = 0;
						temp.GetComponent<SpriteRenderer>().sprite = temp.blueBeam;
						if (towerType == TESLACOIL)
						{
							temp.GetComponent<SpriteRenderer>().sprite = temp.bwBeam;
						}
						temp.setBeam(temp.beamType);
					}
				}
			}
		}
		else if (status == FILLED && (towerType == TESLA || towerType == TESLA2 || towerType == TESLA3))
		{
			//do nothing 
		}
		else if (status == FILLED && (towerType == BRIDGE || towerType == BRIDGE2 || towerType == BRIDGE3))
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
						//effect determines how many times each enemy can pass through bridges
						if (g.creeps[i] != null && g.creeps[i].next == bridgeStart && g.creeps[i].progress <= 1 && g.creeps[i].bridgeCount < effect)
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
		else if (status == FILLED && (towerType == TAG || towerType == TAG2 || towerType == TAG3))
		{
			//same behavior as missile except for slow and with beam
			if (cooldown == 0)
			{
				bool fired = false;
				//choose enemy
				int targetIndex = 0;
				int minProgress = 1000;//find lowest maxProgress to find slowest creep
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
					temp.lifetime = (int)(12.0 / g.timeFactor);
					temp.setBeam(temp.beamType);

					//slow enemy
					g.creeps[targetIndex].maxProgress += effect * g.effects[towerType] * 1;
					g.creeps[targetIndex].progress += effect * g.effects[towerType] * 1;
					g.insertCreep(g.creeps[targetIndex].identIndex);
				}
			}
			else
			{
				cooldown -= 1;
			}

		}

		//now check and update stats
		if (g.damageBoostedTower != this)
		{
			damage = g.damages[towerType];
		}
		if (g.rangeBoostedTower != this)
		{
			range = g.ranges[towerType];
		}
		if (g.speedBoostedTower != this)
		{
			maxCooldown = g.cooldowns[towerType];
		}
		if (g.effectBoostedTower != this)
		{
			effect = g.defaultEffects[towerType];
		}
	}

	void OnMouseOver()
	{
		if (g.paused)
		{
			return;
		}

		button sample2 = FindObjectOfType<button>();
		if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && status == FILLED)
		{
			g.lastTowerSelected = this;
			g.towerSelected = true;
			//g.sideMenu.towerInfoString = "Test string";
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
			if ((g.currButtonActive == sample2.DBoostCode || g.currButtonActive == sample2.RBoostCode || g.currButtonActive == sample2.SBoostCode || g.currButtonActive == sample2.EBoostCode) && Input.GetMouseButtonDown(0))
			{

				return;
			}
		}

		//allows you to build whatever you have selected
		if (Input.GetMouseButtonDown(0))
		{
			//handle boost buttons
			//for each one, must remove boost from any other tower first
			
			if (g.currButtonActive == sample2.DBoostCode && status == FILLED && towerType != BLOCKER)
			{
				
				if (g.damageBoostedTower != null)
				{
					g.damageBoostedTower.damage = g.damages[g.damageBoostedTower.towerType];
					if ((g.damageBoostedTower.towerType == g.damageBoostedTower.TESLA)|| (g.damageBoostedTower.towerType == g.damageBoostedTower.TESLA2)|| (g.damageBoostedTower.towerType == g.damageBoostedTower.TESLA3) || (g.damageBoostedTower.towerType == g.damageBoostedTower.TESLACOIL))
					{
						g.damageBoostedTower.makeWebs();
					}
				}
				g.damageBoostedTower = this;
				g.damageIndicator.transform.position = transform.position + new Vector3(-.3f, .3f, -.001f);
				damage = (int)(damage * g.damageBoost);
			
				
			}
			else if (g.currButtonActive == sample2.RBoostCode && status == FILLED && towerType != BLOCKER)
			{
				
				if (g.rangeBoostedTower != null)
				{ 
					g.rangeBoostedTower.range = g.ranges[g.rangeBoostedTower.towerType];
				}
				g.rangeBoostedTower = this;
				g.rangeIndicator.transform.position = transform.position + new Vector3(.3f, .3f, -.001f);
				range = range + g.rangeBoost;
			
			}
			else if (g.currButtonActive == sample2.SBoostCode && status == FILLED && towerType != BLOCKER)
			{
				if (g.speedBoostedTower != null)
				{
					g.speedBoostedTower.maxCooldown = g.cooldowns[g.speedBoostedTower.towerType];
				}
				g.speedBoostedTower = this;
				g.speedIndicator.transform.position = transform.position + new Vector3 (-.3f, -.3f, -.001f);
				maxCooldown = (int)(maxCooldown * g.speedBoost);
				
			}
			else if (g.currButtonActive == sample2.EBoostCode && status == FILLED && towerType != BLOCKER)
			{

				if (g.effectBoostedTower != null)
				{
					g.effectBoostedTower.effect = g.defaultEffects[g.effectBoostedTower.towerType];
				}
				g.effectBoostedTower = this;
				g.effectIndicator.transform.position = transform.position + new Vector3(.3f, -.3f, -.001f);
				effect = (int)(g.defaultEffects[towerType] + g.effectBoost);
				if ((towerType == TESLA) || (towerType == TESLA2) || (towerType == TESLA3) || towerType == TESLACOIL)
				{
					makeWebs();
				}
			}
			//handle tower buttons
			else if (((status == FILLED && towerType == BLOCKER && g.currButtonActive != BLOCKER) || (g.currButtonActive == BLOCKER && status == EMPTY))&& g.currButtonActive < sample2.EBoostCode && (g.gold - g.towerCosts[g.currButtonActive] >= 0))
			{
				status = FILLED;

				//if the blocks the path, do nothing
				//bool valid = g.CheckLegal();
				/*tile[] routeHolder = new tile[200]; 
				for (int i =0;i<routeHolder.Length;i++)
				{
					routeHolder[i] = g.route[i];
				}*/
				int valid;
				if (g.waveActive)
				{
					valid = g.FindPath(g.start, g.end, false);
				}
				else
				{
					valid = g.FindPath(g.start, g.end, true);
				}
				/*for (int i = 0; i < routeHolder.Length; i++)
				{
					g.route[i] = routeHolder[i];
				}*/
				if (valid == 0)
				{
					status = EMPTY;
					return;
				}

				towerType = g.currButtonActive;
				this.GetComponent<SpriteRenderer>().sprite = towerSprites[g.currButtonActive];
				g.gold -= g.towerCosts[towerType];
				damage = g.damages[towerType];
				range = g.ranges[towerType];
				effect = g.defaultEffects[towerType];
				maxCooldown = g.cooldowns[towerType];
				if (g.waveActive == false)
				{
					refund = true;
					if (towerType == BLOCKER)
					{
						blockerRefund = true;
					}
				}

				//handle beam
				if (towerType == BEAM || towerType == BEAM2 || towerType == BEAM3 || towerType == TAGBEAM)
				{
					unchanged = true;
				}

				//handle tesla
				if (towerType == TESLA || towerType == TESLA2 || towerType == TESLA3 || towerType == TESLACOIL)
				{
					makeWebs();
				}

				//handle bridge
				if (towerType == BRIDGE || towerType == BRIDGE2 || towerType == BRIDGE3)
				{
					configureBridge();
				}
			}
		}
		else if (Input.GetMouseButtonDown(1))//delete any tower
		{
			if (status == FILLED)
			{
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
				if (g.effectBoostedTower == this)
				{
					g.effectIndicator.transform.position = new Vector3(100, 100, 0);
				}
				if ((refund && towerType != BLOCKER) || (towerType == BLOCKER && blockerRefund))
				{
					g.gold += g.towerCosts[towerType];
				}
				else
				{
					g.gold += g.sellCosts[towerType];
				}

				if (towerType == TESLA || towerType == TESLA2 || towerType == TESLA3 || towerType == TESLACOIL)
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
				else if (towerType == BEAM || towerType == BEAM2 || towerType == BEAM3 || towerType == TAGBEAM)
				{
					orient(0);
				}
				else if (towerType == BRIDGE || towerType == BRIDGE2 || towerType == BRIDGE3)
				{
					bridgeStart = null;
					bridgeEnd = null;
				}

				if (towerType == BLOCKER)
				{
					status = EMPTY;
					this.GetComponent<SpriteRenderer>().sprite = emptySprite;
				}
				else
				{
					status = FILLED;
					towerType = BLOCKER;
					this.GetComponent<SpriteRenderer>().sprite = towerSprites[0];

				}

				if (g.waveActive == false)
				{
					g.FindPath(g.start, g.end, true);
				}
			}
		}
		
	}

	public void makeWebs()
	{
		if (north != null && north.north != null && north.north.status == FILLED && (north.north.towerType == TESLA || north.north.towerType == TESLA2 || north.north.towerType == TESLA3 || north.north.towerType == TESLACOIL))
		{
			north.webbed = Mathf.Max(north.north.effect*g.effects[north.north.towerType], effect*g.effects[towerType]);
		}
		if (south != null && south.south != null && south.south.status == FILLED && (south.south.towerType == TESLA || south.south.towerType == TESLA2 || south.south.towerType == TESLA3 || south.south.towerType == TESLACOIL))
		{
			south.webbed = Mathf.Max(south.south.effect * g.effects[south.south.towerType], effect * g.effects[towerType]);
		}
		if (east != null && east.east != null && east.east.status == FILLED && (east.east.towerType == TESLA || east.east.towerType == TESLA2 || east.east.towerType == TESLA3 || east.east.towerType == TESLACOIL))
		{
			east.webbed = Mathf.Max(east.east.effect * g.effects[east.east.towerType], effect * g.effects[towerType]);
		}
		if (west != null && west.west != null && west.west.status == FILLED && (west.west.towerType == TESLA || west.west.towerType == TESLA2 || west.west.towerType == TESLA3 || west.west.towerType == TESLACOIL))
		{
			west.webbed = Mathf.Max(west.west.effect * g.effects[west.west.towerType], effect * g.effects[towerType]);
		}
	}

	public void configureBridge()
	{
		if (g.waveActive == false)
		{
			g.FindPath(g.start, g.end, true);
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
