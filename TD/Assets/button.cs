using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour {

	public int buttonType;
	public game g;
	public Sprite blockerSprite;
	public Sprite missileTowerSprite;
	public Sprite[] towerSprites = new Sprite[4];

	// Use this for initialization
	void Start () {
		g = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
		this.GetComponent<SpriteRenderer>().sprite = towerSprites[buttonType];
		/*if (buttonType == 1)
		{
			this.GetComponent<SpriteRenderer>().sprite = missileTowerSprite;
		}*/
	}

	// Update is called once per frame
	void Update()
	{
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			g.currButtonActive = buttonType;
		}
	}

	
}
