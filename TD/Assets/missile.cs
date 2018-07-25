using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : MonoBehaviour {

	public ant target;
	public game g;
	public int damage = 1;
	public int type;

	// Use this for initialization
	void Start () {
		transform.position = new Vector3(transform.position.x, transform.position.y, -1);
		g = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
	}
	
	// Update is called once per frame
	void Update () {

		//finds new target (farthest forward) if it can, self destructs if none
		if (target == null)
		{
			for (int i = 0; i < g.creeps.Length; i++)
			{
				if (g.creeps[i] != null)
				{
					target = g.creeps[i];
					break;
				}
			}
			if (target == null)
			{
				Destroy(gameObject);
				return;
			}
		}
		else
		{
			//speed controlled here
			transform.position = transform.position + (target.transform.position - transform.position).normalized * .15f;
		}


		//collision with enemy
		if ((transform.position - target.transform.position).magnitude < .1)
		{
			if (type == 0)
			{
				target.health -= damage;
			}
		
			Destroy(gameObject);
		}
	}
}
