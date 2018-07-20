using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam : MonoBehaviour {

	public ant enemy;
	public Transform target;
	public Transform source;
	public int lifetime;
	public int damage = 0;
	public float angle;

	public Sprite whiteBeam;

	// Use this for initialization
	void Start () {
		angle = 0;
		lifetime = 5;
	}
	
	// Update is called once per frame
	void Update () {
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
			setBeam();
		}
	}

	public void setBeam()
	{
		Vector3 start = source.position;
		Vector3 end = target.position;
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
