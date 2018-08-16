using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : MonoBehaviour {

	public ant target;
	public game g;
	public int damage = 1;
	public int effect;
	public int type;

	public Transform Explosion;

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
			transform.position = transform.position + (target.transform.position - transform.position).normalized * (float)(.075f * g.timeFactor);
		}


		//collision with enemy
		if ((transform.position - target.transform.position).magnitude < .1)
		{
			if (type == 0)
			{
				target.health -= damage;
			}
			
			//apply splash damage if effect boosted
			if (effect > 1)
			{
				for (int j = 0; j < g.creeps.Length; j++)//find enemis to splash
				{
					if (g.creeps[j] != null && (g.creeps[j].transform.position - target.transform.position).magnitude < ((effect - 1.0) * 0.3))
					{
						if (g.creeps[j] != target)//dont do double damage
						{
							g.creeps[j].health -= (int)(damage * ((effect - 1.0) * 0.2));
						}
					}
				}

				//also create visual effect
				explosion temp = Instantiate(Explosion, transform.position, Quaternion.identity).gameObject.GetComponent<explosion>();
				temp.transform.localScale = new Vector3(((effect - 1.0f) * 0.3f), ((effect - 1.0f) * 0.3f), 1);
				temp.lifetime = (int)(16.0 / g.timeFactor);

			}

			Destroy(gameObject);
		}
	}
}
