using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour {

	public int buttonType;
	public game g;
	public Sprite blockerSprite;
	public Sprite missileTowerSprite;
	public Sprite[] towerSprites = new Sprite[20];
	public GameObject temp;
	public Text buttonInfo;
	public string buttonText;

	// Use this for initialization
	void Start () {
		g = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
		this.GetComponent<SpriteRenderer>().sprite = towerSprites[buttonType];
		temp = new GameObject("temp");
		temp.transform.SetParent(FindObjectOfType<Canvas>().transform);
		buttonInfo = temp.AddComponent<Text>();
		tile sample = FindObjectOfType<tile>();

		buttonInfo.fontSize = 14;
		buttonInfo.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		temp.layer = 5;
		buttonInfo.color = Color.black;
		buttonInfo.alignment = TextAnchor.MiddleCenter;
		//buttonInfo.rectTransform.anchorMin = new Vector3(0, 0, 0);
		//buttonInfo.rectTransform.anchorMax = new Vector3(0, 0, 0);
		Vector3 pos = new Vector3((Screen.width * (transform.position.x - 2) / 20), (Screen.height * transform.position.y / 14), 0);
		buttonInfo.rectTransform.anchoredPosition = pos + new Vector3(0, -42, 0);
	

	}

	// Update is called once per frame
	void Update()
	{
		tile sample = FindObjectOfType<tile>();
		switch (buttonType)
		{
			case 0:
				buttonText = "Blocker-" + sample.towerCosts[buttonType];
				break;
			case 1:
				buttonText = "Missile-" + sample.towerCosts[buttonType];
				break;
			case 2:
				buttonText = "Splash-" + sample.towerCosts[buttonType];
				break;
			case 3:
				buttonText = "Shock-" + sample.towerCosts[buttonType];
				break;
			case 4:
				buttonText = "Damage-" + g.damageCost;
				break;
			case 5:
				buttonText = "Range-"  + g.rangeCost;
				break;
			case 6:
				buttonText = "Speed-" +  g.speedCost;
				break;
		}
		buttonInfo.text = buttonText;
	}

	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			g.currButtonActive = buttonType;
		}
		if (Input.GetMouseButtonDown(1))
		{
			switch (buttonType)
			{
				case 4:
					if (g.gold > g.damageCost)
					{
						g.gold -= g.damageCost;
						g.damageCost *= 2;
						g.damageBoost += g.dBoostGain;
						if (g.damageBoostedTower != null)
						{
							g.damageBoostedTower.damage = (int)(g.damageBoostedTower.damages[g.damageBoostedTower.towerType] * g.damageBoost);
						}
					}
					break;
				case 5:
					if (g.gold > g.rangeCost)
					{
						g.gold -= g.rangeCost;
						g.rangeCost *= 2;
						g.rangeBoost += g.rBoostGain;
						if (g.rangeBoostedTower != null)
						{
							g.rangeBoostedTower.range = (g.rangeBoostedTower.ranges[g.rangeBoostedTower.towerType] + g.rangeBoost);
						}
					}
					break;
				case 6:
					if (g.gold > g.speedCost)
					{
						g.gold -= g.speedCost;
						g.speedCost *= 2;
						g.speedBoost *= g.sBoostGain;
						if (g.speedBoostedTower != null)
						{
							g.speedBoostedTower.maxCooldown = (int)(g.speedBoostedTower.cooldowns[g.speedBoostedTower.towerType] * g.speedBoost);
						}
					}
					break;
			}
			g.currButtonActive = buttonType;
		}
	}

	
}
