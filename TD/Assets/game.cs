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

	public tile lastTowerSelected;

	//menu stuff
	public int currButtonActive;
	public Text waveText;
	public int lives;
	public Text livesText;
	public int gold;
	public Text goldText;

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
	//assumed: all boosts scale consistently at doubling cost


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
		damageCost = 50;
		rangeCost = 50;
		speedCost = 30;
		dBoostGain = 1;
		rBoostGain = 0.5;
		sBoostGain = .75;

		damageBoost = 1;//multiplicative
		rangeBoost = 0;//additive
		speedBoost = 1;//this multiplies cooldown
		MaxLives = 20;
		lives = MaxLives;
		gold = 2500;
		numWaves = 20;
		waveIndex = -1;
		waves = new int[numWaves][];
		// these numbers mean: # of creeps, spacing, health, speed(high means slow)
		waves[0] = new int[] { 10, 10, 1, 10 };//1
		waves[1] = new int[] { 10, 6, 1, 10 };//2
		waves[2] = new int[] { 15, 10, 1, 10 };//3
		waves[3] = new int[] { 20, 10, 2, 10 };//4
		waves[4] = new int[] { 20, 12, 3, 10 }; //5
		waves[5] = new int[] { 30, 10, 5, 10 };//6
		waves[6] = new int[] { 30, 8, 6, 10 };//7
		waves[7] = new int[] { 50, 10, 7, 10 };//8
		waves[8] = new int[] { 50, 10, 10, 10 };//9
		waves[9] = new int[] { 100, 10, 15, 10 };//10
		waves[10] = new int[] { 50, 12, 25, 10 };//11
		waves[11] = new int[] { 50, 10, 30, 10 };//12
		waves[12] = new int[] { 50, 6, 30, 10 };//13
		waves[13] = new int[] { 50, 10, 35, 10 };//14
		waves[14] = new int[] { 100, 10, 40, 10 };//15
		waves[15] = new int[] { 200, 5, 50, 10 };//16
		waves[16] = new int[] { 10, 50, 150, 10 };//17
		waves[17] = new int[] { 100, 10, 80, 10 };//18
		waves[18] = new int[] { 100, 8, 80, 8 };//19
		waves[19] = new int[] { 100, 10, 100, 10 };//20




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
		SetText();

		maxSortCD = 10;
		sortCD = maxSortCD;
		tile centTile = GameObject.Find("centerTile").GetComponent<tile>();
		start = randomTile(centTile);
		end = start;
		while (end == start || end.north == start || end.south == start || end.east == start || end.west == start)
		{
			end = randomTile(centTile);
		}
		start.GetComponent<SpriteRenderer>().sprite = start.startSprite;
		end.GetComponent<SpriteRenderer>().sprite = end.endSprite;
		route = new tile[200];//max route length < width * height < 200
		beams = new beam[200];
		FindPath(start, end);
	}
	
	// Update is called once per frame
	void Update () {

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
		//if so, map cannot be edited
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
					tiles[i].cooldown = 0;
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (lastTowerSelected.status == lastTowerSelected.FILLED && lastTowerSelected.towerType == lastTowerSelected.BEAM && (waveActive == false || lastTowerSelected.unchanged == true))
			{
				lastTowerSelected.orient(0);
			}
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			if (lastTowerSelected.status == lastTowerSelected.FILLED && lastTowerSelected.towerType == lastTowerSelected.BEAM && (waveActive == false || lastTowerSelected.unchanged == true))
			{
				lastTowerSelected.orient(1);
			}
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (lastTowerSelected.status == lastTowerSelected.FILLED && lastTowerSelected.towerType == lastTowerSelected.BEAM && (waveActive == false || lastTowerSelected.unchanged == true))
			{
				lastTowerSelected.orient(2);
			}
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (lastTowerSelected.status == lastTowerSelected.FILLED && lastTowerSelected.towerType == lastTowerSelected.BEAM && (waveActive == false || lastTowerSelected.unchanged == true))
			{
				lastTowerSelected.orient(3);
			}
		}

		//manage info board
		SetText();
	}

	void SetText ()
	{
		waveText.text = "Round: " + (waveIndex+1)  + "/" + numWaves;
		livesText.text = "Lives: " + lives;
		goldText.text = "Gold: " + gold;
	}

	//this code has been improved upon elsewhere but is still here in case i find a bug later
	/*//returns true if there is a valid path, false if not
	public bool CheckLegal ()
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
		start.dist = 0;
		start.visited = true;
		q.Enqueue(start);

		//while queue is not empty
		while (q.Count > 0)
		{
			current = (tile)q.Dequeue();//dequeue, check for end, return 1 if so
			if (current == end)
			{
				return true;
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
		return false ;
	}*/

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
	public int FindPath(tile from, tile to)
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
}
