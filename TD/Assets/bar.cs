using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bar : MonoBehaviour {

	public ant parent;
	public Vector3 offset; 
	public Vector3 scale; 
	// Use this for initialization
	void Start () {
		offset = new Vector3(0, .35f, 0);
		scale = new Vector3(.12f, .02f, 1);
		transform.localScale = scale;
		transform.position = parent.transform.position + offset;
	}
	
	// Update is called once per frame
	void Update () {
		if(parent == null)
		{
			Destroy(gameObject);
			return;
		}
		transform.position = parent.transform.position + offset;
		float percentage = (float)parent.health / (float)parent.maxHealth;
		if (parent.health < 0)
		{
			percentage = 0;
		}
		scale = new Vector3(percentage * .1f, .02f, 1);
		transform.localScale = scale;
	}
}
