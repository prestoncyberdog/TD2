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


		Vector3 pos = new Vector3((Screen.width * (transform.position.x - 2) / 20), (Screen.height * transform.position.y / 14), 0);
		bonusInfo.rectTransform.anchoredPosition = pos + new Vector3(0, -150, 0);

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
				bonusInfo.text = "Shock Tower\nShoots all enemies within its range\nDamage: = " + g.tiles[0].damages[3] + "\nRange: " + g.tiles[0].ranges[3] + "\nCooldown: " + g.tiles[0].cooldowns[3] + "\nCost: " + g.tiles[0].towerCosts[3];
				image.sprite = g.tiles[0].towerSprites[3];
				break;
			case 1://beam tower
				bonusInfo.text = "Beam Tower\nShoots all enemies in the direction its facing\nDamage: " + g.tiles[0].damages[4] + "\nCooldown: " + g.tiles[0].cooldowns[4] + "\nCost: " + g.tiles[0].towerCosts[4];
				image.sprite = g.tiles[0].towerSprites[4];
				break;
			case 2://coil tower
				bonusInfo.text = "Coil Tower\nShoots all enemies that leave its range based on how long they were in range\nBase Damage: = " + g.tiles[0].damages[5] + "\nRange: " + g.tiles[0].ranges[5] + "\nCost: " + g.tiles[0].towerCosts[5];
				image.sprite = g.tiles[0].towerSprites[5];
				break;
			case 3://damage boost
				bonusInfo.text = "Damage Boost\nIncreases damage of a single tower" + "\nDamage Multiplier: " + g.damageBoost + "\nUpgrade amount: " + g.dBoostGain + "\nCost to upgrade: " + g.damageCost;
				image.sprite = g.tiles[0].towerSprites[17];
				break;
			case 4://range boost
				bonusInfo.text = "Range Boost\nIncreases range of a single tower" + "\nBonus Range: " + g.rangeBoost + "\nUpgrade amount: " + g.rBoostGain + "\nCost to upgrade: " + g.rangeCost;
				image.sprite = g.tiles[0].towerSprites[18];
				break;
			case 5://speed boost
				bonusInfo.text = "Speed Boost\nDecreases cooldown of a single tower" + "\nCooldown Multiplier: " + g.speedBoost + "\nUpgrade amount(multiplies): " + g.sBoostGain + "\nCost to upgrade: " + g.speedCost;
				image.sprite = g.tiles[0].towerSprites[19];
				break;
			case 6://missile2 tower
				bonusInfo.text = "Missile Tower 2\nShoots damaging missiles\nDamage: " + g.tiles[0].damages[9] + "\nRange: " + g.tiles[0].ranges[9] + "\nCooldown: " + g.tiles[0].cooldowns[9] + "\nCost: " + g.tiles[0].towerCosts[9];
				image.sprite = g.tiles[0].towerSprites[9];
				break;
			case 7://splash2 tower
				bonusInfo.text = "Splash Tower 2\nShoots enemy and splashes onto other nearby enemies\nDamage: = " + g.tiles[0].damages[10] + "\nRange/Radius: " + g.tiles[0].ranges[10] + "\nCooldown: " + g.tiles[0].cooldowns[10] + "\nCost: " + g.tiles[0].towerCosts[10];
				image.sprite = g.tiles[0].towerSprites[10];
				break;
			case 8://tesla tower
				bonusInfo.text = "Tesla Tower\nSlows enemies that pass between it and another Tesla\nSlow Power: = " + g.tiles[0].damages[6] + "\nCost: " + g.tiles[0].towerCosts[6];
				image.sprite = g.tiles[0].towerSprites[6];
				break;
			case 9://bridge tower
				bonusInfo.text = "Bridge Tower\nSends enemies across itself, if the other side is earlier on their route" + "\nCooldown: " + g.tiles[0].cooldowns[7] + "\nCost: " + g.tiles[0].towerCosts[7];
				image.sprite = g.tiles[0].towerSprites[7];
				break;
			case 10://tag tower
				bonusInfo.text = "Tag Tower\nShoots and permanently slows an enemy\nDamage/Slow Power: = " + g.tiles[0].damages[8] + "\nRange: " + g.tiles[0].ranges[8] + "\nCooldown: " + g.tiles[0].cooldowns[8] + "\nCost: " + g.tiles[0].towerCosts[8];
				image.sprite = g.tiles[0].towerSprites[8];
				break;
			case 11://shock2 tower
				bonusInfo.text = "Shock Tower 2\nShoots all enemies within its range\nDamage: = " + g.tiles[0].damages[11] + "\nRange: " + g.tiles[0].ranges[11] + "\nCooldown: " + g.tiles[0].cooldowns[11] + "\nCost: " + g.tiles[0].towerCosts[11];
				image.sprite = g.tiles[0].towerSprites[11];
				break;
			case 12://beam2 tower
				bonusInfo.text = "Beam Tower 2\nShoots all enemies in the direction its facing\nDamage: " + g.tiles[0].damages[12] + "\nCooldown: " + g.tiles[0].cooldowns[12] + "\nCost: " + g.tiles[0].towerCosts[12];
				image.sprite = g.tiles[0].towerSprites[12];
				break;
			case 13://coil2 tower
				bonusInfo.text = "Coil Tower 2\nShoots all enemies that leave its range based on how long they were in range\nBase Damage: = " + g.tiles[0].damages[13] + "\nRange: " + g.tiles[0].ranges[13] + "\nCost: " + g.tiles[0].towerCosts[13];
				image.sprite = g.tiles[0].towerSprites[13];
				break;
			case 14://tesla2 tower
				bonusInfo.text = "Tesla Tower 2\nSlows enemies that pass between it and another Tesla\nSlow Power: = " + g.tiles[0].damages[14] + "\nCost: " + g.tiles[0].towerCosts[14];
				image.sprite = g.tiles[0].towerSprites[14];
				break;
			case 15://bridge2 tower
				bonusInfo.text = "Bridge Tower 2\nSends enemies across itself, if the other side is earlier on their route" + "\nCooldown: " + g.tiles[0].cooldowns[15] + "\nCost: " + g.tiles[0].towerCosts[15];
				image.sprite = g.tiles[0].towerSprites[15];
				break;
			case 16://tag2 tower
				bonusInfo.text = "Tag Tower 2\nShoots and permanently slows an enemy\nDamage/Slow Power: = " + g.tiles[0].damages[16] + "\nRange: " + g.tiles[0].ranges[16] + "\nCooldown: " + g.tiles[0].cooldowns[16] + "\nCost: " + g.tiles[0].towerCosts[16];
				image.sprite = g.tiles[0].towerSprites[16];
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
				break;
			case 1://beam tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 4;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[4];
				g.sideMenu.buttonsUsed++;
				break;
			case 2://coil tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 5;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[5];
				g.sideMenu.buttonsUsed++;
				break;
			case 3://damage boost
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 17;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[17];
				g.sideMenu.buttonsUsed++;
				break;
			case 4://range boost
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 18;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[18];
				g.sideMenu.buttonsUsed++;
				break;
			case 5://speed boost
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 19;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[19];
				g.sideMenu.buttonsUsed++;
				break;
			case 6://missile2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 9;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[9];
				g.sideMenu.buttonsUsed++;
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
				break;
			case 9://bridge tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 7;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[7];
				g.sideMenu.buttonsUsed++;
				break;
			case 10://tag tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 8;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[8];
				g.sideMenu.buttonsUsed++;
				break;
			case 11://shock2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 11;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[11];
				g.sideMenu.buttonsUsed++;
				break;
			case 12://beam2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 12;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[12];
				g.sideMenu.buttonsUsed++;
				break;
			case 13://coil2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 13;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[13];
				g.sideMenu.buttonsUsed++;
				break;
			case 14://tesla2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 14;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[14];
				g.sideMenu.buttonsUsed++;
				break;
			case 15://bridge2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 15;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[15];
				g.sideMenu.buttonsUsed++;
				break;
			case 16://tag2 tower
				temp = g.sideMenu.buttons[g.sideMenu.buttonsUsed];
				temp.buttonType = 16;
				temp.GetComponent<SpriteRenderer>().sprite = temp.towerSprites[16];
				g.sideMenu.buttonsUsed++;
				break;
		}
	}
}

