using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game : MonoBehaviour {

	
	public GameObject[] allTiles;
	public tile[] tiles;
	public int tileSpace = 1;
	public Transform Ant;
	public tile start;
	public tile end;
	public ant[] creeps;
	public tile[] route;

	public int[][] waves;
	public int numWaves;
	public int waveIndex = 0;
	public int toSpawnCount;
	public int spawnSpacing;
	public int spawnCD;
	public bool waveActive;
	public int spawnHealth;
	public int MaxLives;

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
		numWaves = 10;
		waveIndex = -1;
		waves = new int[numWaves][];
		waves[0] = new int[] { 10, 10, 1 };
		waves[1] = new int[] { 10, 6, 1 };
		waves[2] = new int[] { 15, 10, 1 };
		waves[3] = new int[] { 20, 10, 2 };
		waves[4] = new int[] { 20, 12, 3 };
		waves[5] = new int[] { 30, 10, 5 };
		waves[6] = new int[] { 30, 8, 6 };
		waves[7] = new int[] { 50, 10, 7 };
		waves[8] = new int[] { 50, 10, 10 };
		waves[9] = new int[] { 100, 10, 15 };

		currButtonActive = 0;
		start = GameObject.Find("StartTile").GetComponent<tile>();
		end = GameObject.Find("EndTile").GetComponent<tile>();
		start.GetComponent<SpriteRenderer>().sprite = start.startSprite;
		end.GetComponent<SpriteRenderer>().sprite = end.endSprite;
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
	}
	
	// Update is called once per frame
	void Update () {

		//manage spawning of enemies
		if (toSpawnCount > 0)
		{
			if (spawnCD == 0)
			{
				creeps[creeps.Length - toSpawnCount] = Instantiate(Ant, start.transform.position, Quaternion.identity).gameObject.GetComponent<ant>();
				creeps[creeps.Length - toSpawnCount].maxHealth = spawnHealth;
				creeps[creeps.Length - toSpawnCount].health = spawnHealth;
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
		waveActive = false;
		for (int i=0; i<creeps.Length;i++)
		{
			if (creeps[i] != null)
			{
				waveActive = true;
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
				route = null;
				SpawnCreeps(waves[waveIndex][0], waves[waveIndex][1], waves[waveIndex][2]);
				for(int i=0;i<tiles.Length;i++)
				{
					tiles[i].refund = false;
					tiles[i].cooldown = 0;
				}
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

	//returns true if there is a valid path, false if not
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
	}

	//sets conditions for enemies to be spawned over time
	//actual instantiation happens in update
	void SpawnCreeps (int count, int spacing, int health)
	{
		creeps = new ant[count];
		toSpawnCount = count;
		spawnSpacing = spacing;
		waveActive = true;
		spawnHealth = health;
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
}
