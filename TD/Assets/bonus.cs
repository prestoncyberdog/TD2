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
	public Sprite[] bonusSprites = new Sprite[10];


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
				bonusInfo.text = "Coil Tower\nDamages all enemies that leave its range based on how long they were in range\nBase Damage: " + g.damages[5] + "\nRange: " + g.ranges[5] + "\nCost: " + g.towerCosts[5];
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
				bonusInfo.text = "Splash Tower 2\nShoots enemy and splashes onto other nearby enemies\nDamage: " + g.damages[10] + "\nRange: " + g.ranges[10] + "\nSplash radius: " + (g.ranges[10] + (g.defaultEffects[10] - 1.0) * 0.5) + "\nCooldown: " + g.cooldowns[10] + "\nCost: " + g.towerCosts[10];
				image.sprite = g.tiles[0].towerSprites[10];
				break;
			case 8://tesla tower
				bonusInfo.text = "Tesla Tower\nSlows enemies that pass between it and another Tesla\nSlow Power: " + g.effects[6] + "\nCost: " + g.towerCosts[6];
				image.sprite = g.tiles[0].towerSprites[6];
				break;
			case 9://bridge tower
				bonusInfo.text = "Bridge Tower\nSends enemies across itself, if the other side is earlier on their route" + "\nCooldown: " + g.cooldowns[7] + "\nMax sendbacks per enemy: " + g.defaultEffects[7] + "\nCost: " + g.towerCosts[7];
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
				bonusInfo.text = "Coil Tower 2\nDamages all enemies that leave its range based on how long they were in range\nBase Damage: " + g.damages[13] + "\nRange: " + g.ranges[13] + "\nCost: " + g.towerCosts[13];
				image.sprite = g.tiles[0].towerSprites[13];
				break;
			case 14://tesla2 tower
				bonusInfo.text = "Tesla Tower 2\nSlows enemies that pass between it and another Tesla\nSlow Power: " + g.effects[14] + "\nCost: " + g.towerCosts[14];
				image.sprite = g.tiles[0].towerSprites[14];
				break;
			case 15://bridge2 tower
				bonusInfo.text = "Bridge Tower 2\nSends enemies across itself, if the other side is earlier on their route" + "\nCooldown: " + g.cooldowns[15] + "\nMax sendbacks per enemy: " + g.defaultEffects[15] + "\nCost: " + g.towerCosts[15];
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
				bonusInfo.text = "Splash Tower 3\nShoots enemy and splashes onto other nearby enemies\nDamage: " + g.damages[18] + "\nRange: " + g.ranges[18] + "\nSplash radius: " + (g.ranges[18] + (g.defaultEffects[18] - 1.0) * 0.5) + "\nCooldown: " + g.cooldowns[18] + "\nCost: " + g.towerCosts[18];
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
				bonusInfo.text = "Coil Tower 3\nDamages all enemies that leave its range based on how long they were in range\nBase Damage: " + g.damages[21] + "\nRange: " + g.ranges[21] + "\nCost: " + g.towerCosts[21];
				image.sprite = g.tiles[0].towerSprites[21];
				break;
			case 22://tesla3 tower
				bonusInfo.text = "Tesla Tower 3\nSlows enemies that pass between it and another Tesla\nSlow Power: " + g.effects[22] + "\nCost: " + g.towerCosts[22];
				image.sprite = g.tiles[0].towerSprites[22];
				break;
			case 23://bridge3 tower
				bonusInfo.text = "Bridge Tower 3\nSends enemies across itself, if the other side is earlier on their route" + "\nCooldown: " + g.cooldowns[23] + "\nMax sendbacks per enemy: " + g.defaultEffects[23] + "\nCost: " + g.towerCosts[23];
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
				bonusInfo.text = "Shocksplash Tower\nShoots and splashes on all enemies in range\nDamage: " + g.damages[26] + "\nRange: " + g.ranges[26] + "\nSplash radius: " + (g.ranges[26] + (g.defaultEffects[26] - 1.0) * 0.5) + "\nCooldown: " + g.cooldowns[26] + "\nCost: " + g.towerCosts[26];
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
			case 30://missile damage *1.2
				bonusInfo.text = "Multiply damage of all missile towers by 1.2";
				image.sprite = g.tiles[0].towerSprites[1];
				break;
			case 31://splash range +.5
				bonusInfo.text = "Increase range/splash radius of all splash towers by 0.5";
				image.sprite = g.tiles[0].towerSprites[2];
				break;
			case 32://shock cd -14
				bonusInfo.text = "Decrease cooldown of all shock towers by 14";
				image.sprite = g.tiles[0].towerSprites[3];
				break;
			case 33://beam damage +5
				bonusInfo.text = "Increase damage of all beam towers by 5";
				image.sprite = g.tiles[0].towerSprites[4];
				break;
			case 34://coil damage +4
				bonusInfo.text = "Increase base damage of all coil towers by 4";
				image.sprite = g.tiles[0].towerSprites[5];
				break;
			case 35://tag damage *2
				bonusInfo.text = "Double damage of all tag towers";
				image.sprite = g.tiles[0].towerSprites[8];
				break;
			case 36://+300 gold
				bonusInfo.text = "Gain 300 gold";
				image.sprite = bonusSprites[0];
				image.transform.localScale = new Vector3(3.0f/6.0f, 3.0f/6.0f, 1);
				break;
			case 37://+50 lives
				bonusInfo.text = "Gain 50 lives";
				image.sprite = bonusSprites[1];
				image.transform.localScale = new Vector3(3.0f / 8.0f, 3.0f / 8.0f, 1);
				break;
			case 38://tag1 +2 slow power
				bonusInfo.text = "Increase slow power of basic tag towers by " + (int)(4 / g.timeFactor);
				image.sprite = g.tiles[0].towerSprites[8];
				break;
			case 39://tesla +16 slow power
				bonusInfo.text = "Increase slow power of all tesla towers by "+ (int)(32 / g.timeFactor);
				image.sprite = g.tiles[0].towerSprites[6];
				break;
			case 40://bridge *.75 cooldown
				bonusInfo.text = "Multiply cooldown of all bridge towers by 0.75";
				image.sprite = g.tiles[0].towerSprites[7];
				break;
			case 41://splash +1 damage
				bonusInfo.text = "Increase damage of all splash towers by 1";
				image.sprite = g.tiles[0].towerSprites[2];
				break;
			case 42://beam +1 effect +5 damage
				bonusInfo.text = "Increase damage of all beam towers by 5 and effect by 1 (can shoot through 1 blocked tile)";
				image.sprite = g.tiles[0].towerSprites[4];
				break;
			case 43://bridge +1 effect
				bonusInfo.text = "Increase effect of all bridge towers by 1 (enemies can be sent back an extra time)";
				image.sprite = g.tiles[0].towerSprites[7];
				break;
			case 44: //missile3 +10 damage +1 range
				bonusInfo.text = "Increase damage of all missile 3 towers by 10 and range by 1";
				image.sprite = g.tiles[0].towerSprites[17];
				break;
			case 45://shock3 +10 damage
				bonusInfo.text = "Increase damage of all shock 3 towers by 10";
				image.sprite = g.tiles[0].towerSprites[19];
				break;
			case 46://damage boost +1
				bonusInfo.text = "Increase damage boost by 1";
				image.sprite = g.tiles[0].towerSprites[47];
				break;
			case 47://range boost +1
				bonusInfo.text = "Increase range boost by 1";
				image.sprite = g.tiles[0].towerSprites[48];
				break;
			case 48: //speed boost *.5
				bonusInfo.text = "Multiply speed boost by 0.5";
				image.sprite = g.tiles[0].towerSprites[49];
				break;
			case 49://effect boost +1
				bonusInfo.text = "Increase effect boost by 1";
				image.sprite = g.tiles[0].towerSprites[46];
				break;
			case 50://beam3 *.75 cd
				bonusInfo.text = "Multiply cooldown of all beam 3 towers by 0.75";
				image.sprite = g.tiles[0].towerSprites[20];
				break;
			case 51://coil +1 range
				bonusInfo.text = "Increase range of all coil towers by 1";
				image.sprite = g.tiles[0].towerSprites[5];
				break;
			case 52: //tag +2 slow power
				bonusInfo.text = "Increase slow power of all tag towers by "+ (int)(4 / g.timeFactor) ;
				image.sprite = g.tiles[0].towerSprites[24];
				break;
			case 53://+500 gold
				bonusInfo.text = "Gain 500 gold";
				image.sprite = bonusSprites[0];
				image.transform.localScale = new Vector3(3.0f / 6.0f, 3.0f / 6.0f, 1);
				break;
			case 54://all towers *1.2 damage * 1.5 slow power
				bonusInfo.text = "Multiply damage of all towers by 1.2 and slow power by 1.5";
				image.sprite = g.tiles[0].towerSprites[47];
				break;
			case 55://all towers +1 range
				bonusInfo.text = "Increase range of all towers by 1";
				image.sprite = g.tiles[0].towerSprites[48];
				break;
			case 56: //all towers *.8 cooldown
				bonusInfo.text = "Multiply cooldown of all towers by .8";
				image.sprite = g.tiles[0].towerSprites[49];
				break;
			case 57://all towers +1 effect
				bonusInfo.text = "Increase effect of all towers by 1";
				image.sprite = g.tiles[0].towerSprites[46];
				break;
			case 58://tesla +20 slow power 80% sell cost 
				bonusInfo.text = "Increase slow power of all tesla towers by 20 and all tesla towers now sell for 80% of their cost";
				image.sprite = g.tiles[0].towerSprites[6];
				break;
			case 59://remove all blocked tiles
				bonusInfo.text = "Replace all dead tiles with blockers";
				image.sprite = g.tiles[0].blockedSprite;
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
				g.bonuses[32][2] = 0;//enable shock cd -15
				break;
			case 1://beam tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 4;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[4];
				g.sideMenu.buttonsUsed++;
				g.bonuses[20][2] = 0;//enable beam3
				g.bonuses[33][2] = 0;//enable beam damage +5
				g.bonuses[42][2] = 0;//enable beam +1 effect +5 damage
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
				g.bonuses[34][2] = 0;//enable coil damage +4
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
				g.bonuses[46][2] = 0;//enable damage boost +1
				break;
			case 4://range boost
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = g.sideMenu.buttons[0].RBoostCode;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[g.sideMenu.buttons[0].RBoostCode];
				g.sideMenu.buttonsUsed++;
				g.bonuses[47][2] = 0;//enable range boost +1
				break;
			case 5://speed boost
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = g.sideMenu.buttons[0].SBoostCode;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[g.sideMenu.buttons[0].SBoostCode];
				g.sideMenu.buttonsUsed++;
				g.bonuses[48][2] = 0;//enable speed boost * .5
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
				g.bonuses[41][2] = 0;//enable splash damage +1
				break;
			case 8://tesla tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 6;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[6];
				g.sideMenu.buttonsUsed++;
				g.bonuses[22][2] = 0;//enable tesla3
				g.bonuses[39][2] = 0;//enable tesla slow power +15
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
				g.bonuses[40][2] = 0;//enable bridge cooldown * .75
				g.bonuses[43][2] = 0;//enable bridge +1 effect
				break;
			case 10://tag tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 8;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[8];
				g.sideMenu.buttonsUsed++;
				g.bonuses[24][2] = 0;//enable tag3
				g.bonuses[35][2] = 0;//enable tag damage *2
				g.bonuses[38][2] = 0;//enable tag1 slow power +2
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
				g.bonuses[42][2] = 0;//enable beam +1 effect +5 damage
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
				g.bonuses[51][2] = 0;//enable coil +.5 range
				break;
			case 14://tesla2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 14;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[14];
				g.sideMenu.buttonsUsed++;
				g.bonuses[22][2] = 0;//enable tesla3
				g.bonuses[58][2] = 0;//enable tesla +20 slow power 80% sell cost
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
				g.bonuses[43][2] = 0;//enable bridge +1 effect
				break;
			case 16://tag2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 16;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[16];
				g.sideMenu.buttonsUsed++;
				g.bonuses[24][2] = 0;//enable tag3
				g.bonuses[52][2] = 0;//enable tag +2 slow power
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
				g.bonuses[44][2] = 0;//enable missile3 +10 damage +1 range
				break;
			case 18://splash3 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 18;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[18];
				g.sideMenu.buttonsUsed++;
				g.bonuses[41][2] = 0;//enable splash damage +1
				break;
			case 19://shock3 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 19;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[19];
				g.sideMenu.buttonsUsed++;
				g.bonuses[45][2] = 0;//enable shock3 +10 damage
				break;
			case 20://beam3 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 20;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[20];
				g.sideMenu.buttonsUsed++;
				g.bonuses[50][2] = 0;//enable beam3 *.75 cooldown
				break;
			case 21://coil3 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 21;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[21];
				g.sideMenu.buttonsUsed++;
				g.bonuses[51][2] = 0;//enable coil +.5 range
				break;
			case 22://tesla3 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 22;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[22];
				g.sideMenu.buttonsUsed++;
				g.bonuses[58][2] = 0;//enable tesla +20 slow power 80% sell cost
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
				g.bonuses[52][2] = 0;//enable tag +2 slow power
				break;
			case 25://effect boost
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = g.sideMenu.buttons[0].EBoostCode;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[g.sideMenu.buttons[0].EBoostCode];
				g.sideMenu.buttonsUsed++;
				g.bonuses[49][2] = 0;//enable effect boost +1
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
			case 30://missile damage *1.2
				g.damages[1] = Mathf.CeilToInt((g.damages[1] * 1.2f));
				g.damages[9] = Mathf.CeilToInt((g.damages[9] * 1.2f));
				g.damages[17] = Mathf.CeilToInt((g.damages[17] * 1.2f));
				g.damages[25] = Mathf.CeilToInt((g.damages[25] * 1.2f));
				break;
			case 31://splash range +.5
				g.ranges[2] += .5;
				g.ranges[10] += .5;
				g.ranges[18] += .5;
				g.ranges[26] += .5;
				break;
			case 32://shock cd -14
				g.cooldowns[3] -= 14;
				g.cooldowns[11] -= 14;
				g.cooldowns[19] -= 14;
				g.cooldowns[26] -= 14;
				break;
			case 33://beam damage +5
				g.damages[4] += 5;
				g.damages[12] += 5;
				g.damages[20] += 5;
				g.damages[27] += 5;
				break;
			case 34://coil damage +4
				g.damages[5] += 4;
				g.damages[13] += 4;
				g.damages[21] += 4;
				g.damages[28] += 4;
				break;
			case 35://tag damage *2
				g.damages[8] *= 2;
				g.damages[16] *= 2;
				g.damages[24] *= 2;
				g.damages[27] *= 2;
				break;
			case 36://+300 gold
				g.gold += 300;
				break;
			case 37://+50 lives
				g.lives += 50;
				break;
			case 38://tag1 +2 slow power
				g.effects[8] += (int)(4 / g.timeFactor);
				break;
			case 39://tesla +16 slow power
				g.effects[6] += (int)(32 / g.timeFactor);
				g.effects[14] += (int)(32 / g.timeFactor);
				g.effects[22] += (int)(32 / g.timeFactor);
				g.effects[28] += (int)(32 / g.timeFactor);
				for (int j = 0; j < g.tiles.Length; j++)//reapply webbing to all tiles
				{
					if (g.tiles[j].towerType == g.tiles[j].TESLA || g.tiles[j].towerType == g.tiles[j].TESLA2 || g.tiles[j].towerType == g.tiles[j].TESLA3 || g.tiles[j].towerType == g.tiles[j].TESLACOIL)
					{
						g.tiles[j].makeWebs();
					}
				}
				break;
			case 40://bridge *.75 cooldown
				g.cooldowns[7] = (int)(g.cooldowns[7] * 0.75);
				g.cooldowns[15] = (int)(g.cooldowns[15] * 0.75);
				g.cooldowns[23] = (int)(g.cooldowns[23] * 0.75);
				break;
			case 41://splash +1 damage
				g.damages[2] += 1;
				g.damages[10] += 1;
				g.damages[18] += 1;
				g.damages[26] += 1;
				break;
			case 42://beam +1 effect +5 damage
				g.damages[4] += 5;
				g.damages[12] += 5;
				g.damages[20] += 5;
				g.damages[27] += 5;
				g.defaultEffects[4] += 1;
				g.defaultEffects[12] += 1;
				g.defaultEffects[20] += 1;
				g.defaultEffects[27] += 1;
				break;
			case 43://bridge +1 effect
				g.defaultEffects[7] += 1;
				g.defaultEffects[15] += 1;
				g.defaultEffects[23] += 1;
				break;
			case 44: //missile3 +10 damage +1 range
				g.damages[17] += 10;
				g.ranges[17] += 1;
				break;
			case 45://shock3 +10 damage
				g.damages[19] += 10;
				break;
			case 46://damage boost +1
				g.damageBoost += 1;
				break;
			case 47://range boost +1
				g.rangeBoost += 1;
				break;
			case 48: //speed boost *.5
				g.speedBoost *= 0.5;
				break;
			case 49://effect boost +1
				g.effectBoost += 1;
				break;
			case 50://beam3 *.75 cd
				g.cooldowns[20] = (int)(g.cooldowns[20] * 0.75);
				break;
			case 51://coil +1 range
				g.ranges[5] += 1;
				g.ranges[13] += 1;
				g.ranges[21] += 1;
				g.ranges[28] += 1;
				break;
			case 52: //tag +2 slow power
				g.effects[8] += (int)(4 / g.timeFactor);
				g.effects[16] += (int)(4 / g.timeFactor);
				g.effects[24] += (int)(4 / g.timeFactor);
				g.effects[27] += (int)(4 / g.timeFactor);
				break;
			case 53://+500 gold
				g.gold += 500;
				break;
			case 54://all towers *1.2 damage * 1.5 slow power
				for (int i = 0;i<g.damages.Length;i++)
				{
					g.damages[i] = (int)(g.damages[i] * 1.2);
					g.effects[i] = Mathf.CeilToInt(g.effects[i] * 1.5f);
					if (g.effects[i] % (4.0 / g.timeFactor) != 0)
					{
						g.effects[i] += (int)((4.0 / g.timeFactor) - (g.effects[i] % (4 / g.timeFactor)));
					}
				}
				for (int j = 0; j < g.tiles.Length; j++)//reapply damage to all tiles
				{
					g.tiles[j].damage = g.damages[g.tiles[j].towerType];
					if (g.damageBoostedTower == g.tiles[j])
					{
						g.tiles[j].damage = (int)(g.tiles[j].damage + g.damageBoost);
					}
					if (g.tiles[j].towerType == g.tiles[j].TESLA || g.tiles[j].towerType == g.tiles[j].TESLA2 || g.tiles[j].towerType == g.tiles[j].TESLA3 || g.tiles[j].towerType == g.tiles[j].TESLACOIL)
					{
						g.tiles[j].makeWebs();
					}
				}
				break;
			case 55://all towers +1 range
				for (int i = 0; i < g.ranges.Length; i++)
				{
					g.ranges[i] = (g.ranges[i] + 1);
				}
				for (int j = 0; j < g.tiles.Length; j++)//reapply range to all tiles
				{
					g.tiles[j].range = g.ranges[g.tiles[j].towerType];
					if (g.rangeBoostedTower == g.tiles[j])
					{
						g.tiles[j].range = (g.tiles[j].range + g.rangeBoost);
					}
				}
				break;
			case 56: //all towers *.8 cooldown
				for (int i = 0; i < g.cooldowns.Length; i++)
				{
					g.cooldowns[i] = (int)(g.cooldowns[i] * .8);
					if (g.cooldowns[i] % (4 / g.timeFactor) != 0)
					{
						g.cooldowns[i] -= (int)(g.cooldowns[i] % (4 / g.timeFactor));
					}
				}
				for (int j = 0; j < g.tiles.Length; j++)//reapply speed to all tiles
				{
					g.tiles[j].maxCooldown = g.cooldowns[g.tiles[j].towerType];
					if (g.speedBoostedTower == g.tiles[j])
					{
						g.tiles[j].maxCooldown = (int)(g.tiles[j].maxCooldown*g.speedBoost);
					}
				}
				break;
			case 57://all towers +1 effect
				for (int i = 0; i < g.defaultEffects.Length; i++)
				{
					g.defaultEffects[i] = (int)(g.defaultEffects[i] + 1);
				}
				for (int j = 0; j < g.tiles.Length; j++)//reapply effect to all tiles
				{
					g.tiles[j].effect = g.defaultEffects[g.tiles[j].towerType];
					if (g.effectBoostedTower == g.tiles[j])
					{
						g.tiles[j].effect = (int)(g.tiles[j].effect + g.effectBoost);
					}
					if (g.tiles[j].towerType == g.tiles[j].TESLA || g.tiles[j].towerType == g.tiles[j].TESLA2 || g.tiles[j].towerType == g.tiles[j].TESLA3 || g.tiles[j].towerType == g.tiles[j].TESLACOIL)
					{
						g.tiles[j].makeWebs();
					}
				}
				break;
			case 58://tesla +20 slow power 80% sell cost
				g.effects[6] += (int)(40 / g.timeFactor);
				g.effects[14] += (int)(40 / g.timeFactor);
				g.effects[22] += (int)(40 / g.timeFactor);
				g.effects[28] += (int)(40 / g.timeFactor);
				g.sellCosts[6] = (int)(g.towerCosts[6] * .8);
				g.sellCosts[14] = (int)(g.towerCosts[14] * .8);
				g.sellCosts[22] = (int)(g.towerCosts[22] * .8);
				g.sellCosts[28] = (int)(g.towerCosts[28] * .8);
				break;
			case 59://remove all blocked tiles
				for(int i = 0;i<g.tiles.Length;i++)
				{
					if (g.tiles[i].status == g.tiles[i].BLOCKED)
					{
						g.tiles[i].status = g.tiles[i].FILLED;
						g.tiles[i].towerType = g.tiles[i].BLOCKER;
						g.tiles[i].GetComponent<SpriteRenderer>().sprite = g.tiles[i].towerSprites[g.tiles[i].BLOCKER];
					}
				}
				break;
		}
	}
}

