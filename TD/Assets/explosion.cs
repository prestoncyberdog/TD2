using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour {

	public int lifetime;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .5f);
	}
	
	// Update is called once per frame
	void Update () {
		lifetime--;
		if (lifetime <= 0)
		{
			Destroy(gameObject);
		}
	}
}
