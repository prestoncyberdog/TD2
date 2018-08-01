using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class bonus : MonoBehaviour
{

	public game g;
	public int bonusIndex;
	public GameObject temp;
	public Text bonusInfo;
	public bool setup;
	public GameObject imageObject;
	public SpriteRenderer image;
	public bool chosen;



	// Use this for initialization
	void Start()
	{
		chosen = false;
		setup = false;
		g = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
		imageObject = new GameObject();
		image = imageObject.AddComponent<SpriteRenderer>();
		image.transform.position = transform.position + new Vector3(0, 2, -0.1f);
		image.transform.localScale = new Vector3(3, 3, 1);

		temp = new GameObject("temp");
		temp.transform.SetParent(FindObjectOfType<Canvas>().transform);
		bonusInfo = temp.AddComponent<Text>();

		bonusInfo.fontSize = 24;
		bonusInfo.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		temp.layer = 5;
		bonusInfo.color = Color.black;
		bonusInfo.alignment = TextAnchor.MiddleCenter;
		//bonusInfo.rectTransform.localScale = new Vector3(3, 3, 1);
		bonusInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300);
		bonusInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300);


		Vector3 pos = new Vector3((Screen.width * (transform.position.x - 2) / (Screen.width * 20f / 1280f)), (Screen.height * transform.position.y / (Screen.height * 14f / 894f)), 0);
		bonusInfo.rectTransform.anchoredPosition = pos + new Vector3(0, Screen.height * -150f / 894f, 0);

	}

	// Update is called once per frame
	void Update()
	{
		if (setup == false)
		{
			setup = true;
			setupBonus();
		}
	}

	void OnMouseOver()
	{

		if (Input.GetMouseButtonDown(0))
		{
			enactBonus();
			g.resume();
		}
	}

	void setupBonus ()
	{

		switch (bonusIndex)
		{
			case 0://shock tower
				bonusInfo.text = "Shock Tower\nShoots all enemies within its range\nDamage: " + g.damages[3] + "\nRange: " + g.ranges[3] + "\nCooldown: " + g.cooldowns[3] + "\nCost: " + g.towerCosts[3];
				image.sprite = g.tiles[0].towerSprites[3];
				break;
			case 1://beam tower
				bonusInfo.text = "Beam Tower\nShoots all enemies in the direction its facing\nDamage: " + g.damages[4] + "\nCooldown: " + g.cooldowns[4] + "\nCost: " + g.towerCosts[4];
				image.sprite = g.tiles[0].towerSprites[4];
				break;
			case 2://coil tower
				bonusInfo.text = "Coil Tower\nShoots all enemies that leave its range based on how long they were in range\nBase Damage: " + g.damages[5] + "\nRange: " + g.ranges[5] + "\nCost: " + g.towerCosts[5];
				image.sprite = g.tiles[0].towerSprites[5];
				break;
			case 3://damage boost
				bonusInfo.text = "Damage Boost\nIncreases damage of a single tower" + "\nDamage Multiplier: " + g.damageBoost + "\nUpgrade amount: " + g.dBoostGain + "\nCost to upgrade: " + g.damageCost;
				image.sprite = g.tiles[0].towerSprites[47];
				break;
			case 4://range boost
				bonusInfo.text = "Range Boost\nIncreases range of a single tower" + "\nBonus Range: " + g.rangeBoost + "\nUpgrade amount: " + g.rBoostGain + "\nCost to upgrade: " + g.rangeCost;
				image.sprite = g.tiles[0].towerSprites[48];
				break;
			case 5://speed boost
				bonusInfo.text = "Speed Boost\nDecreases cooldown of a single tower" + "\nCooldown Multiplier: " + g.speedBoost + "\nUpgrade amount(multiplies): " + g.sBoostGain + "\nCost to upgrade: " + g.speedCost;
				image.sprite = g.tiles[0].towerSprites[49];
				break;
			case 6://missile2 tower
				bonusInfo.text = "Missile Tower 2\nShoots damaging missiles\nDamage: " + g.damages[9] + "\nRange: " + g.ranges[9] + "\nCooldown: " + g.cooldowns[9] + "\nCost: " + g.towerCosts[9];
				image.sprite = g.tiles[0].towerSprites[9];
				break;
			case 7://splash2 tower
				bonusInfo.text = "Splash Tower 2\nShoots enemy and splashes onto other nearby enemies\nDamage: " + g.damages[10] + "\nRange/Radius: " + g.ranges[10] + "\nCooldown: " + g.cooldowns[10] + "\nCost: " + g.towerCosts[10];
				image.sprite = g.tiles[0].towerSprites[10];
				break;
			case 8://tesla tower
				bonusInfo.text = "Tesla Tower\nSlows enemies that pass between it and another Tesla\nSlow Power: " + g.effects[6] + "\nCost: " + g.towerCosts[6];
				image.sprite = g.tiles[0].towerSprites[6];
				break;
			case 9://bridge tower
				bonusInfo.text = "Bridge Tower\nSends enemies across itself, if the other side is earlier on their route" + "\nCooldown: " + g.cooldowns[7] + "\nCost: " + g.towerCosts[7];
				image.sprite = g.tiles[0].towerSprites[7];
				break;
			case 10://tag tower
				bonusInfo.text = "Tag Tower\nShoots and permanently slows an enemy\nDamage: " + g.damages[8] + "\nSlow Power: " + g.effects[8] + "\nRange: " + g.ranges[8] + "\nCooldown: " + g.cooldowns[8] + "\nCost: " + g.towerCosts[8];
				image.sprite = g.tiles[0].towerSprites[8];
				break;
			case 11://shock2 tower
				bonusInfo.text = "Shock Tower 2\nShoots all enemies within its range\nDamage: " + g.damages[11] + "\nRange: " + g.ranges[11] + "\nCooldown: " + g.cooldowns[11] + "\nCost: " + g.towerCosts[11];
				image.sprite = g.tiles[0].towerSprites[11];
				break;
			case 12://beam2 tower
				bonusInfo.text = "Beam Tower 2\nShoots all enemies in the direction its facing\nDamage: " + g.damages[12] + "\nCooldown: " + g.cooldowns[12] + "\nCost: " + g.towerCosts[12];
				image.sprite = g.tiles[0].towerSprites[12];
				break;
			case 13://coil2 tower
				bonusInfo.text = "Coil Tower 2\nShoots all enemies that leave its range based on how long they were in range\nBase Damage: " + g.damages[13] + "\nRange: " + g.ranges[13] + "\nCost: " + g.towerCosts[13];
				image.sprite = g.tiles[0].towerSprites[13];
				break;
			case 14://tesla2 tower
				bonusInfo.text = "Tesla Tower 2\nSlows enemies that pass between it and another Tesla\nSlow Power: " + g.effects[14] + "\nCost: " + g.towerCosts[14];
				image.sprite = g.tiles[0].towerSprites[14];
				break;
			case 15://bridge2 tower
				bonusInfo.text = "Bridge Tower 2\nSends enemies across itself, if the other side is earlier on their route" + "\nCooldown: " + g.cooldowns[15] + "\nCost: " + g.towerCosts[15];
				image.sprite = g.tiles[0].towerSprites[15];
				break;
			case 16://tag2 tower
				bonusInfo.text = "Tag Tower 2\nShoots and permanently slows an enemy\nDamage: " + g.damages[16] + "\nSlow Power: " + g.effects[16] + "\nRange: " + g.ranges[16] + "\nCooldown: " + g.cooldowns[16] + "\nCost: " + g.towerCosts[16];
				image.sprite = g.tiles[0].towerSprites[16];
				break;
			case 17://missile3 tower
				bonusInfo.text = "Missile Tower 3\nShoots damaging missiles\nDamage: " + g.damages[17] + "\nRange: " + g.ranges[17] + "\nCooldown: " + g.cooldowns[17] + "\nCost: " + g.towerCosts[17];
				image.sprite = g.tiles[0].towerSprites[17];
				break;
			case 18://splash3 tower
				bonusInfo.text = "Splash Tower 3\nShoots enemy and splashes onto other nearby enemies\nDamage: " + g.damages[18] + "\nRange/Radius: " + g.ranges[18] + "\nCooldown: " + g.cooldowns[18] + "\nCost: " + g.towerCosts[18];
				image.sprite = g.tiles[0].towerSprites[18];
				break;
			case 19://shock3 tower
				bonusInfo.text = "Shock Tower 3\nShoots all enemies within its range\nDamage: " + g.damages[19] + "\nRange: " + g.ranges[19] + "\nCooldown: " + g.cooldowns[19] + "\nCost: " + g.towerCosts[19];
				image.sprite = g.tiles[0].towerSprites[19];
				break;
			case 20://beam3 tower
				bonusInfo.text = "Beam Tower 3\nShoots all enemies in the direction its facing\nDamage: " + g.damages[20] + "\nCooldown: " + g.cooldowns[20] + "\nCost: " + g.towerCosts[20];
				image.sprite = g.tiles[0].towerSprites[20];
				break;
			case 21://coil3 tower
				bonusInfo.text = "Coil Tower 3\nShoots all enemies that leave its range based on how long they were in range\nBase Damage: " + g.damages[21] + "\nRange: " + g.ranges[21] + "\nCost: " + g.towerCosts[21];
				image.sprite = g.tiles[0].towerSprites[21];
				break;
			case 22://tesla3 tower
				bonusInfo.text = "Tesla Tower 3\nSlows enemies that pass between it and another Tesla\nSlow Power: " + g.effects[22] + "\nCost: " + g.towerCosts[22];
				image.sprite = g.tiles[0].towerSprites[22];
				break;
			case 23://bridge3 tower
				bonusInfo.text = "Bridge Tower 3\nSends enemies across itself, if the other side is earlier on their route" + "\nCooldown: " + g.cooldowns[23] + "\nCost: " + g.towerCosts[23];
				image.sprite = g.tiles[0].towerSprites[23];
				break;
			case 24://tag3 tower
				bonusInfo.text = "Tag Tower 3\nShoots and permanently slows an enemy\nDamage: " + g.damages[24] + "\nSlow Power: " + g.effects[24] + "\nRange: " + g.ranges[24] + "\nCooldown: " + g.cooldowns[24] + "\nCost: " + g.towerCosts[24];
				image.sprite = g.tiles[0].towerSprites[24];
				break;
			case 25://effect boost
				bonusInfo.text = "Effect Boost\nIncreases special effect of a single tower" + "\nAdded Effect Multiplier: " + g.effectBoost + "\nUpgrade amount: " + g.eBoostGain + "\nCost to upgrade: " + g.effectCost;
				image.sprite = g.tiles[0].towerSprites[46];
				break;
			case 26://missile4 tower
				bonusInfo.text = "Missile Tower 4\nShoots damaging missiles\nDamage: " + g.damages[25] + "\nRange: " + g.ranges[25] + "\nCooldown: " + g.cooldowns[25] + "\nCost: " + g.towerCosts[25];
				image.sprite = g.tiles[0].towerSprites[25];
				break;
			case 27://shocksplash tower
				bonusInfo.text = "Shocksplash Tower\nShoots  all enemies in range and splashes onto other nearby enemies\nDamage: " + g.damages[26] + "\nRange/Radius: " + g.ranges[26] + "\nCooldown: " + g.cooldowns[26] + "\nCost: " + g.towerCosts[26];
				image.sprite = g.tiles[0].towerSprites[26];
				break;
			case 28://tagbeam tower
				bonusInfo.text = "Tagbeam Tower\nShoots and permanently slows all enemies in the direction its facing\nDamage: " + g.damages[27] + "\nSlow Power: " + g.effects[27] + "\nCooldown: " + g.cooldowns[27] + "\nCost: " + g.towerCosts[27];
				image.sprite = g.tiles[0].towerSprites[27];
				break;
			case 29://teslacoil tower
				bonusInfo.text = "Teslacoil Tower\nActs as both a powerful Coil and Tesla tower\nBase Damage: " + g.damages[28] + "\nSlow Power: " + g.effects[28] + "\nRange: " + g.ranges[28] + "\nCost: " + g.towerCosts[28];
				image.sprite = g.tiles[0].towerSprites[28];
				break;
		}


	}
	

	void enactBonus()
	{
		button temp;
		chosen = true;

		switch (bonusIndex)
		{
			case 0://shock tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 3;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[3];
				g.sideMenu.buttonsUsed++;
				g.bonuses[19][2] = 0;//enable shock3
				g.bonuses[27][2] = 0;//enable shocksplash
				break;
			case 1://beam tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 4;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[4];
				g.sideMenu.buttonsUsed++;
				g.bonuses[20][2] = 0;//enable beam3
				if (g.bonuses[24][2] == 0)//if tag3 also unlocked
				{
					g.bonuses[28][2] = 0;//enable tagbeam
				}
				break;
			case 2://coil tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 5;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[5];
				g.sideMenu.buttonsUsed++;
				g.bonuses[21][2] = 0;//enable coil3
				if (g.bonuses[22][2] == 0)//if tesla also unlocked
				{
					g.bonuses[29][2] = 0;//enable teslacoil
				}
				break;
			case 3://damage boost
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = g.sideMenu.buttons[0].DBoostCode;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[g.sideMenu.buttons[0].DBoostCode];
				g.sideMenu.buttonsUsed++;
				break;
			case 4://range boost
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = g.sideMenu.buttons[0].RBoostCode;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[g.sideMenu.buttons[0].RBoostCode];
				g.sideMenu.buttonsUsed++;
				break;
			case 5://speed boost
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = g.sideMenu.buttons[0].SBoostCode;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[g.sideMenu.buttons[0].SBoostCode];
				g.sideMenu.buttonsUsed++;
				break;
			case 6://missile2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 9;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[9];
				g.sideMenu.buttonsUsed++;
				g.bonuses[26][2] = 0;//enable missile4
				break;
			case 7://splash2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 10;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[10];
				g.sideMenu.buttonsUsed++;
				break;
			case 8://tesla tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 6;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[6];
				g.sideMenu.buttonsUsed++;
				g.bonuses[22][2] = 0;//enable tesla3
				if (g.bonuses[21][2] == 0)//if coil3 also unlocked
				{
					g.bonuses[29][2] = 0;//enable teslacoil
				}
				break;
			case 9://bridge tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 7;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[7];
				g.sideMenu.buttonsUsed++;
				g.bonuses[23][2] = 0;//enable bridge3
				break;
			case 10://tag tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 8;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[8];
				g.sideMenu.buttonsUsed++;
				g.bonuses[24][2] = 0;//enable tag3
				if (g.bonuses[20][2] == 0)//if beam3 also unlocked
				{
					g.bonuses[28][2] = 0;//enable tagbeam
				}
				break;
			case 11://shock2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 11;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[11];
				g.sideMenu.buttonsUsed++;
				g.bonuses[19][2] = 0;//enable shock3
				g.bonuses[27][2] = 0;//enable shocksplash
				break;
			case 12://beam2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 12;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[12];
				g.sideMenu.buttonsUsed++;
				g.bonuses[20][2] = 0;//enable beam3
				if (g.bonuses[24][2] == 0)//if tag3 also unlocked
				{
					g.bonuses[28][2] = 0;//enable tagbeam
				}
				break;
			case 13://coil2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 13;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[13];
				g.sideMenu.buttonsUsed++;
				g.bonuses[21][2] = 0;//enable coil3
				if (g.bonuses[22][2] == 0)//if tesla also unlocked
				{
					g.bonuses[29][2] = 0;//enable teslacoil
				}
				break;
			case 14://tesla2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 14;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[14];
				g.sideMenu.buttonsUsed++;
				g.bonuses[22][2] = 0;//enable tesla3
				if (g.bonuses[21][2] == 0)//if coil3 also unlocked
				{
					g.bonuses[29][2] = 0;//enable teslacoil
				}
				break;
			case 15://bridge2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 15;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[15];
				g.sideMenu.buttonsUsed++;
				g.bonuses[23][2] = 0;//enable bridge3
				break;
			case 16://tag2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 16;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[16];
				g.sideMenu.buttonsUsed++;
				g.bonuses[24][2] = 0;//enable tag3
				if (g.bonuses[20][2] == 0)//if beam3 also unlocked
				{
					g.bonuses[28][2] = 0;//enable tagbeam
				}
				break;
			case 17://missile3 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 17;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[17];
				g.sideMenu.buttonsUsed++;
				g.bonuses[26][2] = 0;//enable missile4
				break;
			case 18://splash3 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 18;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[18];
				g.sideMenu.buttonsUsed++;
				break;
			case 19://shock3 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 19;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[19];
				g.sideMenu.buttonsUsed++;
				break;
			case 20://beam3 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 20;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[20];
				g.sideMenu.buttonsUsed++;
				break;
			case 21://coil3 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 21;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[21];
				g.sideMenu.buttonsUsed++;
				break;
			case 22://tesla3 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 22;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[22];
				g.sideMenu.buttonsUsed++;
				break;
			case 23://bridge3 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 23;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[23];
				g.sideMenu.buttonsUsed++;
				break;
			case 24://tag3 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 24;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[24];
				g.sideMenu.buttonsUsed++;
				break;
			case 25://effect boost
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = g.sideMenu.buttons[0].EBoostCode;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[g.sideMenu.buttons[0].EBoostCode];
				g.sideMenu.buttonsUsed++;
				break;
			case 26://missile4 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 25;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[25];
				g.sideMenu.buttonsUsed++;
				break;
			case 27://shocksplash tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 26;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[26];
				g.sideMenu.buttonsUsed++;
				break;
			case 28://tagbeam tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 27;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[27];
				g.sideMenu.buttonsUsed++;
				break;
			case 29://teslacoil tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 28;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[28];
				g.sideMenu.buttonsUsed++;
				break;
		}
	}
}

