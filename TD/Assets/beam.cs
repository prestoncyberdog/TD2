using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam : MonoBehaviour {

	public ant enemy;
	public game g;
	public Transform target;
	public Transform source;
	public int lifetime;
	public int damage = 0;
	public float angle;
	public int beamType;
	public double maxRange;
	public int effect;

	//default is green
	public Sprite whiteBeam;
	public Sprite redBeam;
	public Sprite blueBeam;
	public Sprite weirdBeam;
	public Sprite purpleBeam;
	public Sprite orangeBeam;
	public Sprite gwBeam;
	public Sprite rpBeam;
	public Sprite bwBeam;

	// Use this for initialization
	void Start () {
		angle = 0;
		g = GameObject.FindGameObjectWithTag("game").GetComponent<game>();

	}

	// Update is called once per frame
	void Update () {
		if (beamType == 0)//splash or shock tower
		{
			lifetime--;
			if (target == null)
			{
				Destroy(gameObject);
			}
			else if (source == null)
			{
				enemy.health -= damage;
				Destroy(gameObject);
			}
			else if (lifetime <= 0)
			{
				enemy.health -= damage;
				Destroy(gameObject);
			}
			else
			{
				setBeam(beamType);
			}
		}
		else if (beamType == 1)//beam tower, no enemy variable here
		{
			lifetime--;
			if (target == null)
			{
				Destroy(gameObject);
			}
			else if (source.gameObject.GetComponent<tile>().status == source.gameObject.GetComponent<tile>().EMPTY)
			{
				Destroy(gameObject);
			}
			else if (lifetime <= 0)
			{
				Destroy(gameObject);
			}
			else
			{
				setBeam(beamType);
			}
		}
		else if (beamType == 2)//coil tower, lifetime and damage work differently
		{
			lifetime++;
			if (target == null)
			{
				Destroy(gameObject);
			}
			else if (source.gameObject.GetComponent<tile>().status == source.gameObject.GetComponent<tile>().EMPTY)
			{
				//enemy.health -= damage*lifetime/10;
				enemy.health -= damage * Mathf.CeilToInt(((Mathf.Pow(lifetime, 2f + (effect - 1.0f)*.1f)) + 0.0f) / 5625.0f);
				Destroy(gameObject);
			}
			else if ((target.transform.position - source.transform.position).magnitude > maxRange || (enemy.next == enemy.end && enemy.progress < 3))//range check
			{
				//enemy.health -= damage*lifetime/10;
				enemy.health -= Mathf.CeilToInt(damage * ((Mathf.Pow(lifetime, 2f + (effect - 1.0f) * .1f)) + 0.0f) / 5625.0f);
				source.gameObject.GetComponent<tile>().targeting.Remove(enemy);
				Destroy(gameObject);
			}
			else
			{
				setBeam(beamType);
			}
		}
		else if (beamType == 3)//tag tower
		{
			lifetime--;
			if (target == null)
			{
				Destroy(gameObject);
			}
			else if (source.gameObject.GetComponent<tile>().status == source.gameObject.GetComponent<tile>().EMPTY)
			{
				enemy.health -= damage;
				Destroy(gameObject);
			}
			else if (lifetime <= 0)
			{
				enemy.health -= damage;
				Destroy(gameObject);
			}
			else
			{
				setBeam(beamType);
			}
		}
		else if (beamType == 4)//path display
		{
			//do nothing
		}
	}

	public void setBeam(int type)
	{
		Vector3 start = source.position + new Vector3 (0, 0, -0.001f);
		Vector3 end = target.position;
		if (type == 1)
		{
			end = end + ((end - start).normalized * 0.5f);
		}
		else if (type == 4)
		{
			start = start + new Vector3(0, 0, 0.002f);
		}
		transform.position = (start + end) / 2;
		float dist = (start - end).magnitude / 5;
		float angleNeeded = Vector3.Angle(end - start, new Vector3(1, 0, 0));
		if (end.y < start.y)
		{
			angleNeeded *= -1;
		}
		//angleNeeded -= transform.rotation.z;
		transform.rotation = Quaternion.identity;
		transform.Rotate(new Vector3(0, 0, angleNeeded));
		transform.localScale = new Vector3(dist, .02f, 1);

		
	}
}
