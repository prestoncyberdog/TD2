using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour {

	public int buttonType;
	public game g;
	public Sprite[] towerSprites;
	public GameObject temp;
	public Text buttonInfo;
	public string buttonText;
	public int DBoostCode; 
	public int RBoostCode; 
	public int SBoostCode;
	public int EBoostCode;
	public Vector3 pos;
	public Vector3 textOffset;
	// Use this for initialization
	void Start () {
		EBoostCode = 46;
		DBoostCode = 47;
		RBoostCode = 48;
		SBoostCode = 49;
		g = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
		towerSprites = g.tiles[0].towerSprites;
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
		pos = g.c.WorldToScreenPoint(transform.position + new Vector3(0, -.0f, 0));
		buttonInfo.rectTransform.anchorMin = new Vector2(0, 0);
		buttonInfo.rectTransform.anchorMax = new Vector2(0, 0);
		textOffset = g.c.WorldToScreenPoint(new Vector3(0, -.67f, 0)) - g.c.WorldToScreenPoint(new Vector3(0, 0, 0));

		//Vector3 pos = new Vector3((Screen.width * (transform.position.x - 2) / (Screen.width * 20f / 1280f)), (Screen.height * transform.position.y / (Screen.height * 14f / 894f)), 0);
		buttonInfo.rectTransform.anchoredPosition = pos + textOffset;
	

	}

	// Update is called once per frame
	void Update()
	{
		pos = g.c.WorldToScreenPoint(transform.position + new Vector3 (0, -.0f, 0));


		//pos = new Vector3((Screen.width * (transform.position.x - g.c.transform.position.x) / (Screen.width * 20f / 1280f)), ((Screen.height * (transform.position.y - g.c.transform.position.y)) / (Screen.height * 14f / 894f)), 0);
		buttonInfo.rectTransform.anchoredPosition = pos + textOffset;

		if (g.paused || g.dead)
		{
			return;
		}

		switch (buttonType)
		{
			case 0:
				buttonText = "Blocker-" + g.towerCosts[buttonType];
				break;
			case 1:
				buttonText = "Missile-" + g.towerCosts[buttonType];
				break;
			case 2:
				buttonText = "Splash-" + g.towerCosts[buttonType];
				break;
			case 3:
				buttonText = "Shock-" + g.towerCosts[buttonType];
				break;
			case 4:
				buttonText = "Beam-" + g.towerCosts[buttonType];
				break;
			case 5:
				buttonText = "Coil-" + g.towerCosts[buttonType];
				break;
			case 6:
				buttonText = "Tesla-" + g.towerCosts[buttonType];
				break;
			case 7:
				buttonText = "Bridge-" + g.towerCosts[buttonType];
				break;
			case 8:
				buttonText = "Tag-" + g.towerCosts[buttonType];
				break;
			case 9:
				buttonText = "Missile2-" + g.towerCosts[buttonType];
				break;
			case 10:
				buttonText = "Splash2-" + g.towerCosts[buttonType];
				break;
			case 11:
				buttonText = "Shock2-" + g.towerCosts[buttonType];
				break;
			case 12:
				buttonText = "Beam2-" + g.towerCosts[buttonType];
				break;
			case 13:
				buttonText = "Coil2-" + g.towerCosts[buttonType];
				break;
			case 14:
				buttonText = "Tesla2-" + g.towerCosts[buttonType];
				break;
			case 15:
				buttonText = "Bridge2-" + g.towerCosts[buttonType];
				break;
			case 16:
				buttonText = "Tag2-" + g.towerCosts[buttonType];
				break;
			case 17:
				buttonText = "Missile3-" + g.towerCosts[buttonType];
				break;
			case 18:
				buttonText = "Splash3-" + g.towerCosts[buttonType];
				break;
			case 19:
				buttonText = "Shock3-" + g.towerCosts[buttonType];
				break;
			case 20:
				buttonText = "Beam3-" + g.towerCosts[buttonType];
				break;
			case 21:
				buttonText = "Coil3-" + g.towerCosts[buttonType];
				break;
			case 22:
				buttonText = "Tesla3-" + g.towerCosts[buttonType];
				break;
			case 23:
				buttonText = "Bridge3-" + g.towerCosts[buttonType];
				break;
			case 24:
				buttonText = "Tag3-" + g.towerCosts[buttonType];
				break;
			case 25:
				buttonText = "Missile4-" + g.towerCosts[buttonType];
				break;
			case 26:
				buttonText = "Shocksplash-" + g.towerCosts[buttonType];
				break;
			case 27:
				buttonText = "Tagbeam-" + g.towerCosts[buttonType];
				break;
			case 28:
				buttonText = "Teslacoil-" + g.towerCosts[buttonType];
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
				else if (buttonType == EBoostCode)
				{
					buttonText = "Effect-" + g.effectCost;
				}
				break;
		}

		buttonInfo.text = buttonText;
	}

	void OnMouseOver()
	{
		if (g.paused || g.dead)
		{
			return;
		}

		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
		{
			g.towerSelected = false;
		}

		if (Input.GetMouseButtonDown(0))
		{
			g.currButtonActive = buttonType; 
			//g.sideMenu.highlightObject.transform.position = transform.position + new Vector3(0, 0, .1f); 
			for (int i =0;i<g.sideMenu.buttons.Length;i++)
			{
				if (g.sideMenu.buttons[i] == this)
				{
					g.sideMenu.currButtonIndex = i;
					break;
				}
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			//this handles when boosts are upgraded
			if(buttonType == DBoostCode)
			{
				if (g.gold >= g.damageCost)
				{
					g.gold -= g.damageCost;
					g.damageCost = (int)(g.damageCost * g.boostCostMultiplier);
					g.damageBoost += g.dBoostGain;
					if (g.damageBoostedTower != null)
					{
						g.damageBoostedTower.damage = (int)(g.damages[g.damageBoostedTower.towerType] * g.damageBoost);
					}
				}
			}
			else if (buttonType == RBoostCode)
			{
				if (g.gold >= g.rangeCost)
				{
					g.gold -= g.rangeCost;
					g.rangeCost = (int)(g.rangeCost * g.boostCostMultiplier);
					g.rangeBoost += g.rBoostGain;
					if (g.rangeBoostedTower != null)
					{
						g.rangeBoostedTower.range = (g.ranges[g.rangeBoostedTower.towerType] + g.rangeBoost);
					}
				}
			}
			else if (buttonType == SBoostCode)
			{
				if (g.gold >= g.speedCost)
				{
					g.gold -= g.speedCost;
					g.speedCost  = (int)(g.speedCost * g.boostCostMultiplier);
					g.speedBoost *= g.sBoostGain;
					if (g.speedBoostedTower != null)
					{
						g.speedBoostedTower.maxCooldown = (int)(g.cooldowns[g.speedBoostedTower.towerType] * g.speedBoost);
					}
				}
			}
			else if (buttonType == EBoostCode)
			{
				if (g.gold >= g.effectCost)
				{
					g.gold -= g.effectCost;
					g.effectCost = (int)(g.effectCost * g.boostCostMultiplier);
					g.effectBoost += g.eBoostGain;
					if (g.effectBoostedTower != null)
					{
						g.effectBoostedTower.effect = 1+ (int)g.effectBoost;//(g.ranges[g.rangeBoostedTower.towerType] + g.rangeBoost);
						if ((g.effectBoostedTower.towerType == g.effectBoostedTower.TESLA) || (g.effectBoostedTower.towerType == g.effectBoostedTower.TESLA2)|| (g.effectBoostedTower.towerType == g.effectBoostedTower.TESLA3)|| (g.effectBoostedTower.towerType == g.effectBoostedTower.TESLACOIL))
						{
							g.effectBoostedTower.makeWebs();
						}
					}
				}
			}

			g.currButtonActive = buttonType;
		}
	}

	
}
