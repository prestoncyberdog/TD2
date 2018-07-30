using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game : MonoBehaviour {

	
	public GameObject[] allTiles;
	public tile[] tiles;
	public int tileSpace = 1;
	public Transform Ant;
	public Transform Beam;
	public tile start;
	public tile end;
	public ant[] creeps;
	public tile[] route;
	public beam[] beams;
	public menu sideMenu;

	public int[][] waves;
	public int numWaves;
	public int waveIndex = 0;
	public int toSpawnCount;
	public int spawnSpacing;
	public int spawnCD;
	public bool waveActive;
	public int spawnHealth;
	public int spawnMaxProgress;
	public int MaxLives;
	public int sortCD;
	public int maxSortCD;
	public int currBlockerSize;

	public tile lastTowerSelected;
	public bool paused;

	//menu stuff
	public int currButtonActive;
	public int lives;
	public int gold;

	//special boosts
	public double damageBoost;
	public double rangeBoost;
	public double speedBoost;
	public tile damageBoostedTower;
	public tile rangeBoostedTower;
	public tile speedBoostedTower;
	public boostIndicator damageIndicator;
	public boostIndicator rangeIndicator;
	public boostIndicator speedIndicator;
	public Transform Indicator;
	public int damageCost;
	public int rangeCost;
	public int speedCost;
	public double dBoostGain;
	public double rBoostGain;
	public double sBoostGain;
	public double boostCostMultiplier;
	//assumed: all boosts scale consistently at multiplying cost

	//bonuses (mayors)
	public int[][] bonuses;
	public Transform Bonus;
	bonus option1;
	bonus option2;
	public int bonusFrequency;

	public int gameMode;

	public int[] towerCosts;
	public int[] cooldowns;
	public double[] ranges;
	public int[] damages;


	// Use this for initialization
	void Start () {
		damageIndicator = Instantiate(Indicator, new Vector3(100, 100, 0), Quaternion.identity).gameObject.GetComponent<boostIndicator>();
		rangeIndicator = Instantiate(Indicator, new Vector3(100, 100, 0), Quaternion.identity).gameObject.GetComponent<boostIndicator>();
		speedIndicator = Instantiate(Indicator, new Vector3(100, 100, 0), Quaternion.identity).gameObject.GetComponent<boostIndicator>();
		rangeIndicator.GetComponent<SpriteRenderer>().sprite = rangeIndicator.range;
		speedIndicator.GetComponent<SpriteRenderer>().sprite = speedIndicator.speed;
		damageIndicator.transform.localScale = new Vector3(0.2f, 0.2f, 1);
		rangeIndicator.transform.localScale = new Vector3(0.2f, 0.2f, 1);
		speedIndicator.transform.localScale = new Vector3(0.2f, 0.2f, 1);
		damageCost = 60;
		rangeCost = 30;
		speedCost = 30;
		dBoostGain = 1;
		rBoostGain = 0.5;
		sBoostGain = .75;
		boostCostMultiplier = 2;

		bonuses = new int [17][];
		bonuses[0] = new int[] { 5, 20, 0 };//shock (doubles as default)
		bonuses[1] = new int[] { 5, 20, 0 };//beam
		bonuses[2] = new int[] { 5, 20, 0 };//coil
		bonuses[3] = new int[] { 10, 50, 0 };//damage boost
		bonuses[4] = new int[] { 15, 50, 0 };//range boost
		bonuses[5] = new int[] { 10, 50, 0 };//speed boost
		bonuses[6] = new int[] { 5, 20, 0 };//missile2
		bonuses[7] = new int[] { 5, 20, 0 };//splash2
		bonuses[8] = new int[] { 20, 35, 0 };//tesla
		bonuses[9] = new int[] { 20, 35, 0 };//bridge
		bonuses[10] = new int[] { 5, 20, 0 };//tag
		bonuses[11] = new int[] { 20, 35, 0 };//shock2
		bonuses[12] = new int[] { 20, 35, 0 };//beam2
		bonuses[13] = new int[] { 20, 35, 0 };//coil2
		bonuses[14] = new int[] { 35, 50, 0 };//tesla2
		bonuses[15] = new int[] { 35, 50, 0 };//bridge2 
		bonuses[16] = new int[] { 20, 35, 0 };//tag2
		bonusFrequency = 5;
		gameMode = 1;//This determines whether bonuses are enabled
		//0 means start with all towers, 1 means play with bonuses

		damageBoost = 1;//multiplicative
		rangeBoost = 0;//additive
		speedBoost = 1;//this multiplies cooldown
		MaxLives = 20;
		lives = MaxLives;
		gold = 30;
		numWaves = 50;
		waveIndex = -1;
		waves = new int[numWaves][];

		// these numbers mean: # of creeps, spacing, health, speed(high means slow)
		waves[0] = new int[] { 10, 10, 1, 10 };//1
		waves[1] = new int[] { 10, 6, 1, 10 };//2
		waves[2] = new int[] { 15, 10, 1, 10 };//3
		waves[3] = new int[] { 20, 10, 2, 10 };//4
		waves[4] = new int[] { 50, 12, 3, 12 }; //5
		waves[5] = new int[] { 30, 10, 5, 10 };//6
		waves[6] = new int[] { 30, 8, 6, 10 };//7
		waves[7] = new int[] { 50, 10, 7, 10 };//8
		waves[8] = new int[] { 50, 5, 10, 12 };//9
		waves[9] = new int[] { 100, 10, 15, 10 };//10
		waves[10] = new int[] { 50, 12, 25, 10 };//11
		waves[11] = new int[] { 50, 10, 30, 10 };//12
		waves[12] = new int[] { 50, 6, 30, 10 };//13
		waves[13] = new int[] { 50, 10, 35, 10 };//14
		waves[14] = new int[] { 150, 8, 40, 10 };//15
		waves[15] = new int[] { 100, 5, 50, 10 };//16
		waves[16] = new int[] { 10, 50, 150, 10 };//17
		waves[17] = new int[] { 100, 10, 80, 10 };//18
		waves[18] = new int[] { 100, 6, 80, 8 };//19
		waves[19] = new int[] { 200, 8, 100, 10 };//20
		waves[20] = new int[] { 30, 2, 120, 10 };//21
		waves[21] = new int[] { 150, 10, 130, 10 };//22
		waves[22] = new int[] { 50, 8, 50, 4 };//23
		waves[23] = new int[] { 150, 10, 140, 10 };//24
		waves[24] = new int[] { 250, 8, 160, 10 };//25
		waves[25] = new int[] { 100, 10, 170, 10 };//26
		waves[26] = new int[] { 50, 15, 180, 9 };//27
		waves[27] = new int[] { 150, 5, 180, 10 };//28
		waves[28] = new int[] { 50, 7, 180, 8 };//29
		waves[29] = new int[] { 300, 9, 220, 10 };//30
		waves[30] = new int[] { 100, 15, 150, 5 };//31
		waves[31] = new int[] { 150, 8, 250, 10 };//32
		waves[32] = new int[] { 100, 40, 800, 12 };//33
		waves[33] = new int[] { 100, 10, 280, 9 };//34
		waves[34] = new int[] { 350, 6, 320, 11 };//35
		waves[35] = new int[] { 100, 10, 350, 10 };//36
		waves[36] = new int[] { 100, 10, 365, 9 };//37
		waves[37] = new int[] { 100, 10, 380, 8 };//38
		waves[38] = new int[] { 150, 5, 400, 10 };//39
		waves[39] = new int[] { 400, 8, 450, 10 };//40
		waves[40] = new int[] { 100, 10, 500, 8 };//41
		waves[41] = new int[] { 150, 6, 550, 10 };//42
		waves[42] = new int[] { 100, 10, 580, 10 };//43
		waves[43] = new int[] { 100, 10, 700, 11 };//44
		waves[44] = new int[] { 450, 10, 720, 10 };//45
		waves[45] = new int[] { 150, 10, 780, 10 };//46
		waves[46] = new int[] { 150, 10, 800, 9 };//47
		waves[47] = new int[] { 150, 10, 850, 8 };//48
		waves[48] = new int[] { 100, 2, 850, 10 };//49
		waves[49] = new int[] { 500, 5, 900, 10 };//50




		currButtonActive = 0;
		toSpawnCount = 0;
		spawnSpacing = 10;
		waveActive = false;
		spawnCD = spawnSpacing;
		allTiles = GameObject.FindGameObjectsWithTag("Tile");
		tiles = new tile[allTiles.Length];

		//create tile grid
		for (int i = 0;i<allTiles.Length;i++)
		{
			tiles[i] = (allTiles[i].GetComponent<tile>());
		}
		CreateGraph(tiles);

		//blocker, missile, splash, shock, beam, coil, tesla, bridge, tag, missile2, splash2, shock2, beam2, coil2, tesla2, bridge2, tag2
		towerCosts = new int[] { 2, 10, 15, 35, 40, 50, 40, 50, 35, 50, 50, 200, 200, 200, 200, 200, 200 };
		cooldowns = new int[] { 0, 20, 45, 75, 80, 0, 0, 80, 30, 20, 45, 70, 80, 0, 0, 40, 25 };
		ranges = new double[] { 0, 3.2, 2.3, 1.8, 0, 1.8, 0, 0, 1.2, 4.2, 3.3, 2.3, 0, 2.8, 0, 0, 2.2 };
		damages = new int[] { 0, 1, 1, 8, 5, 8, 30, 0, 2, 10, 2, 15, 20, 12, 60, 0, 3 };

		maxSortCD = 10;
		sortCD = maxSortCD;
		route = new tile[200];//max route length < width * height < 200
		beams = new beam[200];
		randomizeBoard();
		start.GetComponent<SpriteRenderer>().sprite = start.startSprite;
		end.GetComponent<SpriteRenderer>().sprite = end.endSprite;
		paused = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (paused)
		{
			return;
		}

		//manage spawning of enemies
		if (toSpawnCount > 0)
		{
			if (spawnCD == 0)
			{
				int tempIndex = 0;
				for (int i=0;i<creeps.Length;i++)
				{
					tempIndex = i;
					if (creeps[i] == null)
					{
						break;
					}
				}
				creeps[tempIndex] = Instantiate(Ant, start.transform.position, Quaternion.identity).gameObject.GetComponent<ant>();
				creeps[tempIndex].maxHealth = spawnHealth;
				creeps[tempIndex].health = spawnHealth;
				creeps[tempIndex].maxProgress = spawnMaxProgress;
				creeps[tempIndex].identIndex = tempIndex;
				spawnCD = spawnSpacing;
				toSpawnCount -= 1;
			}
			else
			{
				spawnCD -= 1;
			}
		}

		//check if a wave is in progress
		//if so, tile on path cannot be edited
		bool waveWasWactive = waveActive;
		if (toSpawnCount == 0)
		{
			waveActive = false;
			for (int i = 0; i < creeps.Length; i++)
			{
				if (creeps[i] != null)
				{
					waveActive = true;
				}
			}

			//here is the border between a wave and an in-between time
			if (waveActive == false && waveWasWactive)
			{
				FindPath(start, end, true);
				if (gameMode == 1 && (waveIndex+1)%bonusFrequency == 0)
				{
					
					offerBonuses();
					
				}
			}
		}

		if (waveActive)
		{
			if (sortCD <= 0)
			{
				SortCreeps();
				sortCD = maxSortCD;
			}
			else
			{
				sortCD--;
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (waveActive == false)
			{
				if (waveIndex + 1 < numWaves)
				{
					waveIndex += 1;
				}
				//SpawnCreeps(10, 10, 3);
				route = new tile[200];//max route length < width * height < 200
				SpawnCreeps(waves[waveIndex][0], waves[waveIndex][1], waves[waveIndex][2], waves[waveIndex][3]);
				for(int i=0;i<tiles.Length;i++)
				{
					tiles[i].refund = false;
					tiles[i].blockerRefund = false;
					tiles[i].cooldown = 0;
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (lastTowerSelected.status == lastTowerSelected.FILLED && (lastTowerSelected.towerType == lastTowerSelected.BEAM || lastTowerSelected.towerType == lastTowerSelected.BEAM2) && (waveActive == false || lastTowerSelected.unchanged == true))
			{
				lastTowerSelected.orient(0);
			}
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			if (lastTowerSelected.status == lastTowerSelected.FILLED && (lastTowerSelected.towerType == lastTowerSelected.BEAM || lastTowerSelected.towerType == lastTowerSelected.BEAM2) && (waveActive == false || lastTowerSelected.unchanged == true))
			{
				lastTowerSelected.orient(1);
			}
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (lastTowerSelected.status == lastTowerSelected.FILLED && (lastTowerSelected.towerType == lastTowerSelected.BEAM || lastTowerSelected.towerType == lastTowerSelected.BEAM2) && (waveActive == false || lastTowerSelected.unchanged == true))
			{
				lastTowerSelected.orient(2);
			}
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (lastTowerSelected.status == lastTowerSelected.FILLED && (lastTowerSelected.towerType == lastTowerSelected.BEAM || lastTowerSelected.towerType == lastTowerSelected.BEAM2) && (waveActive == false || lastTowerSelected.unchanged == true))
			{
				lastTowerSelected.orient(3);
			}
		}

	}


	//sets conditions for enemies to be spawned over time
	//actual instantiation happens in update
	void SpawnCreeps (int count, int spacing, int health, int progression)
	{
		creeps = new ant[count];
		toSpawnCount = count;
		spawnSpacing = spacing;
		waveActive = true;
		spawnHealth = health;
		spawnMaxProgress = progression;
	}

	//Create edges in tile graph
	void CreateGraph(tile[] tiles)
	{
		int len = tiles.Length;
		for (int i=0;i<len-1;i++)
		{
			for (int j=i+1;j<len;j++)
			{
				if((tiles[j].transform.position.x == tiles[i].transform.position.x + tileSpace) && (tiles[j].transform.position.y == tiles[i].transform.position.y))
				{
					tiles[i].east = tiles[j];
					tiles[j].west = tiles[i];
				}
				if ((tiles[j].transform.position.x == tiles[i].transform.position.x - tileSpace) && (tiles[j].transform.position.y == tiles[i].transform.position.y))
				{
					tiles[i].west = tiles[j];
					tiles[j].east = tiles[i];
				}
				if ((tiles[j].transform.position.x == tiles[i].transform.position.x) && (tiles[j].transform.position.y == tiles[i].transform.position.y + tileSpace))
				{
					tiles[i].north = tiles[j];
					tiles[j].south = tiles[i];
				}
				if ((tiles[j].transform.position.x == tiles[i].transform.position.x) && (tiles[j].transform.position.y == tiles[i].transform.position.y - tileSpace))
				{
					tiles[i].south = tiles[j];
					tiles[j].north = tiles[i];
				}

			}
		}
	}

	//plan a path from location 'from' to location 'to'
	//returns 1 if successful, 0 if no path was found
	//updates route as it goes
	public int FindPath(tile from, tile to, bool changeRoute)
	{
		tile current;
		//set all tiles to unvisited, prev to null, dist to 10000
		for (int i = 0; i < tiles.Length; i++)
		{
			tiles[i].visited = false;
			tiles[i].prev = null;
			tiles[i].dist = 10000;
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
				//wont reach this code if no path is found

				//dont mess with visual beams if display is active
				//dont change route either
				if(changeRoute == false)
				{
					return 1;
				}

				//clear path display beams
				for (int i = 0; i < beams.Length; i++)
				{
					if (beams[i] != null)
					{
						Destroy(beams[i].gameObject);
					}
				}

				tile backTemp = current;
				while (backTemp != null)
				{
					if (backTemp.prev != null)
					{
						beam temp = Instantiate(Beam, transform.position, Quaternion.identity).gameObject.GetComponent<beam>();
						temp.source = backTemp.transform;
						temp.target = backTemp.prev.transform;
						temp.beamType = 4;
						temp.GetComponent<SpriteRenderer>().sprite = temp.orangeBeam;
						temp.setBeam(temp.beamType);
						beams[backTemp.dist] = temp;
					}
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
	}

	//this is used whenever a creep is slowed or misplaced
	//insetion/bubble sort to correct the placement of element wrongIndex
	public void insertCreep (int wrongIndex)
	{
		int endIndex = -1;
		for (int i = 0;i<route.Length;i++)
		{
			if (route[i] != null && route[i] == end)
			{
				endIndex = i;
				break;
			}
		}

		for (int i = wrongIndex;i<(creeps.Length -1);i++)
		{
			if (creeps[i+1]==null)
			{
				return;
			}
			//compare i and i+1
			int score1 = ((endIndex - creeps[i].routeIndex) * creeps[i].maxProgress) + creeps[i].progress;
			int score2 = ((endIndex - creeps[i+1].routeIndex) * creeps[i+1].maxProgress) + creeps[i+1].progress;

			if (score1 > score2)
			{
				ant holder = creeps[i];
				creeps[i] = creeps[i + 1];
				creeps[i + 1] = holder;
				creeps[i].identIndex = i;
				creeps[i + 1].identIndex = i + 1;
			}
			else
			{
				return;
			}
		}
	}

	public void SortCreeps ()
	{
		int endIndex = -1;
		for (int i = 0; i < route.Length; i++)
		{
			if (route[i] != null && route[i] == end)
			{
				endIndex = i;
				break;
			}
		}

		for (int i=0;i<creeps.Length;i++)
		{
			int j = i;
			while (j > 0)
			{
				int score1 = 0;
				int score2 = 0;
				if (creeps[j] != null)
				{
					score1 = ((endIndex - creeps[j].routeIndex) * creeps[j].maxProgress) + creeps[j].progress;
				}
				else
				{
					score1 = 100000;

				}

				if (creeps[j-1] != null)
				{
					score2 = ((endIndex - creeps[j - 1].routeIndex) * creeps[j - 1].maxProgress) + creeps[j - 1].progress;
				}
				else
				{
					score2 = 100000;

				}

				if (score1 < score2)
				{
					ant holder = creeps[j];
					creeps[j] = creeps[j - 1];
					creeps[j - 1] = holder;
					creeps[j].identIndex = j;
					creeps[j - 1].identIndex = j - 1;
				}
				else
				{
					break;
				}
			}
		}
	}

	public tile randomTile (tile current)
	{
		float num = 0;
		int i = 0;
		while (i < 200)
		{
			num = Random.Range(0, 4);
			if (num < 1)
			{
				if (current.north != null)
				{
					current = current.north;
				}
				else
				{
					i--;
				}
			}
			else if (num < 2)
			{
				if (current.south != null)
				{
					current = current.south;
				}
				else
				{
					i--;
				}
			}
			else if (num < 3)
			{
				if (current.east != null)
				{
					current = current.east;
				}
				else
				{
					i--;
				}
			}
			else
			{
				if (current.west != null)
				{
					current = current.west;
				}
				else
				{
					i--;
				}
			}
			i++;
		}
		return current;
	}

	public void randomizeBoard()
	{
		tile centTile = GameObject.Find("centerTile").GetComponent<tile>();
		//createBlocker(15, 5, randomTile(centTile));
		/*createBlocker(Random.Range(8, 12), Random.Range(3, 8), randomTile(centTile));
		createBlocker(Random.Range(3, 8), Random.Range(1, 2), randomTile(centTile));
		createBlocker(Random.Range(3, 8), Random.Range(1, 2), randomTile(centTile));
		createBlocker(Random.Range(3, 8), Random.Range(1, 2), randomTile(centTile));*/
		createBlocker(Random.Range(2, 4), Random.Range(1, 2), randomTile(centTile));
		createBlocker(Random.Range(2, 4), Random.Range(1, 2), randomTile(centTile));
		createBlocker(Random.Range(2, 4), Random.Range(1, 2), randomTile(centTile));
		createBlocker(Random.Range(2, 4), Random.Range(1, 2), randomTile(centTile));
		createBlocker(Random.Range(2, 4), Random.Range(1, 2), randomTile(centTile));
		createBlocker(Random.Range(2, 4), Random.Range(1, 2), randomTile(centTile));
		createBlocker(Random.Range(2, 4), Random.Range(1, 2), randomTile(centTile));
		createBlocker(Random.Range(6, 10), Random.Range(1, 2), randomTile(centTile));


		for (int i = 0;i<tiles.Length;i++)
		{
			if (tiles[i].north == null || tiles[i].south == null || tiles[i].east == null || tiles[i].west == null)
			{
				tiles[i].status = tiles[i].EMPTY;
				tiles[i].GetComponent<SpriteRenderer>().sprite = tiles[i].emptySprite;
			}
		}

		start = randomTile(centTile);
		end = start;
		while (end == start || end.north == start || end.south == start || end.east == start || end.west == start)
		{
			end = randomTile(centTile);
		}

		while (FindPath(start, end, true) == 0)
		{
			start = randomTile(centTile);
			end = start;
			while (end == start || end.north == start || end.south == start || end.east == start || end.west == start)
			{
				end = randomTile(centTile);
			}
		}
	}

	public void createBlocker(int maxSize, int minSize, tile baseTile)
	{
		currBlockerSize = 0;
		while (currBlockerSize < minSize)
		{
			for (int i = 0;i<tiles.Length;i++)
			{
				tiles[i].visited = false;
			}
			baseTile.visited = true;
			createBlockerRecur(maxSize, baseTile);
		}
	}

	public void createBlockerRecur (int maxSize, tile baseTile)
	{
		//move to edge of blocked area to expand it
		if (baseTile.status == baseTile.BLOCKED)
		{
			if (baseTile.north != null && baseTile.north.status == baseTile.BLOCKED && currBlockerSize < maxSize - 1 && baseTile.north.visited == false)
			{
				baseTile.north.visited = true;
				createBlockerRecur(maxSize, baseTile.north);
			}
			if (baseTile.south != null && baseTile.south.status == baseTile.BLOCKED && currBlockerSize < maxSize - 1 && baseTile.south.visited == false)
			{
				baseTile.south.visited = true;
				createBlockerRecur(maxSize, baseTile.south);
			}
			if (baseTile.east != null && baseTile.east.status == baseTile.BLOCKED && currBlockerSize < maxSize - 1 && baseTile.east.visited == false)
			{
				baseTile.east.visited = true;
				createBlockerRecur(maxSize, baseTile.east);
			}
			if (baseTile.west != null && baseTile.west.status == baseTile.BLOCKED && currBlockerSize < maxSize - 1 && baseTile.west.visited == false)
			{
				baseTile.west.visited = true;
				createBlockerRecur(maxSize, baseTile.west);
			}
		}


		//expand area if possible
		baseTile.status = baseTile.BLOCKED;
		baseTile.GetComponent<SpriteRenderer>().sprite = baseTile.blockedSprite;
		if (baseTile.north != null && baseTile.north.north != null && baseTile.north.status == baseTile.EMPTY && currBlockerSize < maxSize - 1 && Random.Range(0, 2) < 1)
		{
			baseTile.north.visited = true;
			currBlockerSize += 1;
			createBlockerRecur(maxSize, baseTile.north);
		}
		if (baseTile.south != null && baseTile.south.south != null && baseTile.south.status == baseTile.EMPTY && currBlockerSize < maxSize - 1 && Random.Range(0, 2) < 1)
		{
			baseTile.south.visited = true;
			currBlockerSize += 1;
			createBlockerRecur(maxSize, baseTile.south);
		}
		if (baseTile.east != null && baseTile.east.east != null && baseTile.east.status == baseTile.EMPTY && currBlockerSize < maxSize - 1 && Random.Range(0, 2) < 1)
		{
			baseTile.east.visited = true;
			currBlockerSize += 1;
			createBlockerRecur(maxSize, baseTile.east);
		}
		if (baseTile.west != null && baseTile.west.west != null && baseTile.west.status == baseTile.EMPTY && currBlockerSize < maxSize - 1 && Random.Range(0, 2) < 1)
		{
			baseTile.west.visited = true;
			currBlockerSize += 1;
			createBlockerRecur(maxSize, baseTile.west);
		}
		
	}

	public void offerBonuses ()
	{
		paused = true;
		int choice;

		choice = randomBonus();
		option1 = Instantiate(Bonus, new Vector3(-3, 0, -2), Quaternion.identity).gameObject.GetComponent<bonus>();
		option1.bonusIndex = choice;
		bonuses[choice][2] = 1;

		choice = randomBonus();
		option2 = Instantiate(Bonus, new Vector3(3, 0, -2), Quaternion.identity).gameObject.GetComponent<bonus>();
		option2.bonusIndex = choice;
		bonuses[choice][2] = 1;
	}


	//chooses a bonus based on waveindex
	public int randomBonus ()
	{
		int options = 0;
		for (int i =0;i<bonuses.Length;i++)
		{
			if (bonuses[i][0] <= waveIndex+1 && bonuses[i][1] >= waveIndex+1 && bonuses[i][2] == 0)
			{
				options++;
			}
		}
		int choice = Random.Range(0, options);

		for(int j = 0;j<bonuses.Length;j++)
		{
			if (bonuses[j][0] <= waveIndex+1 && bonuses[j][1] >= waveIndex+1 && bonuses[j][2] == 0)
			{
				if (choice < 1)
				{
					return j;
				}
				else
				{
					choice--;
				}
			}
		}
		return choice;//should never happen
	}

	public void resume()
	{
		if (option1.chosen)
		{
			bonuses[option2.bonusIndex][2] = 0;
		}
		else
		{
			bonuses[option1.bonusIndex][2] = 0;
		}
		Destroy(option1.temp.gameObject);
		Destroy(option2.temp.gameObject);
		Destroy(option1.image);
		Destroy(option2.image);
		Destroy(option1.gameObject);
		Destroy(option2.gameObject);
		paused = false;
	}
}
