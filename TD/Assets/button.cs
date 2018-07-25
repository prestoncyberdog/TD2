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
	public int DBoostCode; 
	public int RBoostCode; 
	public int SBoostCode;

	// Use this for initialization
	void Start () {
		DBoostCode = 17;
		RBoostCode = 18;
		SBoostCode = 19;
		g = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
		this.GetComponent<SpriteRenderer>().sprite = towerSprites[buttonType];
		temp = new GameObject("temp");
		temp.transform.SetParent(FindObjectOfType<Canvas>().transform);
		buttonInfo = temp.AddComponent<Text>();

		buttonInfo.fontSize = 12;
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
				buttonText = "Beam-" + sample.towerCosts[buttonType];
				break;
			case 5:
				buttonText = "Coil-" + sample.towerCosts[buttonType];
				break;
			case 6:
				buttonText = "Tesla-" + sample.towerCosts[buttonType];
				break;
			case 7:
				buttonText = "Bridge-" + sample.towerCosts[buttonType];
				break;
			case 8:
				buttonText = "Tag-" + sample.towerCosts[buttonType];
				break;
			default:
				//cover boost buttons
				if (buttonType == DBoostCode)
				{
					buttonText = "Damage-" + g.damageCost;
				}
				else if (buttonType == RBoostCode)
				{
					buttonText = "Range-" + g.rangeCost;
				}
				else if (buttonType == SBoostCode)
				{
					buttonText = "Speed-" + g.speedCost;
				}
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
			if(buttonType == DBoostCode)
			{
				if (g.gold >= g.damageCost)
				{
					g.gold -= g.damageCost;
					g.damageCost *= 2;
					g.damageBoost += g.dBoostGain;
					if (g.damageBoostedTower != null)
					{
						g.damageBoostedTower.damage = (int)(g.damageBoostedTower.damages[g.damageBoostedTower.towerType] * g.damageBoost);
						if (g.damageBoostedTower.towerType == g.damageBoostedTower.TESLA)
						{
							g.damageBoostedTower.makeWebs();
						}
					}
				}
			}
			else if (buttonType == RBoostCode)
			{
				if (g.gold >= g.rangeCost)
				{
					g.gold -= g.rangeCost;
					g.rangeCost *= 2;
					g.rangeBoost += g.rBoostGain;
					if (g.rangeBoostedTower != null)
					{
						g.rangeBoostedTower.range = (g.rangeBoostedTower.ranges[g.rangeBoostedTower.towerType] + g.rangeBoost);
					}
				}
			}
			else if (buttonType == SBoostCode)
			{
				if (g.gold >= g.speedCost)
				{
					g.gold -= g.speedCost;
					g.speedCost *= 2;
					g.speedBoost *= g.sBoostGain;
					if (g.speedBoostedTower != null)
					{
						g.speedBoostedTower.maxCooldown = (int)(g.speedBoostedTower.cooldowns[g.speedBoostedTower.towerType] * g.speedBoost);
					}
				}
			}
			
			g.currButtonActive = buttonType;
		}
	}

	
}
