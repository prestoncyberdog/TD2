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
	public const int MAX_PROGRESS = 10;
	public int routeIndex;
	public int health = 1;
	public int maxHealth = 1;

	public bar healthBar;
	public Transform Bar;




	Vector3 zboy = new Vector3(0, 0, -.01f);
	// Use this for initialization
	void Start () {
		/*
		LineRenderer temp2;
		temp2 = gameObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
		temp2.material = new Material(Shader.Find("Particles/Additive"));
		//temp2.material = new Material(Shader.Find("Unlit"));
;		temp2.endColor = new Color(1, 1, 1);
		temp2.startColor = new Color(1, 1, 1);
		temp2.startWidth = .1f;
		temp2.endWidth = .1f;
		temp2.positionCount = 3;
		Vector3[] things = new Vector3[] { end.transform.position, transform.position, start.transform.position };
		//temp2.SetPosition(0, transform.position);
		//temp2.SetPosition(1, start.transform.position);
		//temp2.SetPosition(2, end.transform.position);
		temp2.SetPositions(things);*/


		healthBar = Instantiate(Bar, transform.position, Quaternion.identity).gameObject.GetComponent<bar>();
		healthBar.parent = this;

		transform.position = new Vector3(transform.position.x, transform.position.y, -.01f);
		g = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
		start = GameObject.Find("StartTile").GetComponent<tile>();
		end = GameObject.Find("EndTile").GetComponent<tile>();
		route = new tile[200];//max route length < width * height < 200
		FindPath(start, end);
		progress = MAX_PROGRESS;
		routeIndex = 1;
		previous = route[0];
		next = route[1];
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (health <= 0)
		{
			kill();
			return;
		}

		//movement of creeps
		if (progress > 0)
		{
			Vector3 step = (next.transform.position+zboy - transform.position)/progress;
			transform.Translate(step);
			progress -= 1;
		}
		else
		{
			if (next == end)
			{
				g.lives -= 1;
				Destroy(gameObject);
			}
			progress = MAX_PROGRESS;
			previous = next;
			next = route[routeIndex + 1];
			routeIndex += 1;
		}

		
	}

	
	void kill()
	{
		g.gold += 1;
		Destroy(gameObject);
	}



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
	}
}
