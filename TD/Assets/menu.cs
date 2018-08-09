using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class menu : MonoBehaviour {

	public game g;
	public GameObject overall;
	public Text overallInfo;
	public GameObject wave;
	public Text waveInfo;
	public GameObject nextWave;
	public Text nextWaveInfo;
	public GameObject tower;
	public Text towerInfo;

	public Transform Button;
	public button[] buttons;
	public int buttonsUsed;

	public string towerInfoString;

	public GameObject imageObject;
	public SpriteRenderer image;
	public GameObject imageObject2;
	public SpriteRenderer image2;


	// Use this for initialization
	void Start () {
		g = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
		Vector3 pos = new Vector3((Screen.width * (transform.position.x - 2) / (Screen.width * 20f / 1280f)), (Screen.height * transform.position.y / (Screen.height * 14f / 1280f)), 0);

		imageObject = new GameObject();
		image = imageObject.AddComponent<SpriteRenderer>();
		image.transform.position = transform.position + new Vector3(-1f, 2.4f, -0.1f);
		image.transform.localScale = new Vector3(.15f, .15f, 1);
		image.transform.rotation = Quaternion.Euler(0, 0, -90);
		imageObject2 = new GameObject();
		image2 = imageObject2.AddComponent<SpriteRenderer>();
		image2.transform.position = transform.position + new Vector3(1f, 2.4f, -0.1f);
		image2.transform.localScale = new Vector3(.15f, .15f, 1);
		image2.transform.rotation = Quaternion.Euler(0, 0, -90); 


		transform.position = transform.position + new Vector3(0, 0, -.1f);

		overall = new GameObject("overall");
		overall.transform.SetParent(FindObjectOfType<Canvas>().transform);
		overallInfo = overall.AddComponent<Text>();
		overallInfo.fontSize = 24;
		overallInfo.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		overall.layer = 5;
		overallInfo.color = Color.black;
		overallInfo.alignment = TextAnchor.MiddleCenter;
		overallInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200);
		overallInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
		overallInfo.rectTransform.anchoredPosition = pos + new Vector3(0, Screen.height * 320f / 894f, 0);

		wave = new GameObject("waveinfo");
		wave.transform.SetParent(FindObjectOfType<Canvas>().transform);
		waveInfo = wave.AddComponent<Text>();
		waveInfo.fontSize = 16;
		waveInfo.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		wave.layer = 5;
		waveInfo.color = Color.black;
		waveInfo.alignment = TextAnchor.MiddleCenter;
		waveInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200);
		waveInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
		waveInfo.rectTransform.anchoredPosition = pos + new Vector3(Screen.width * -65f / 1280f, Screen.height * 210f / 894f, 0);

		nextWave = new GameObject("nextwaveinfo");
		nextWave.transform.SetParent(FindObjectOfType<Canvas>().transform);
		nextWaveInfo = nextWave.AddComponent<Text>();
		nextWaveInfo.fontSize = 16;
		nextWaveInfo.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		nextWave.layer = 5;
		nextWaveInfo.color = Color.black;
		nextWaveInfo.alignment = TextAnchor.MiddleCenter;
		nextWaveInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200);
		nextWaveInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
		nextWaveInfo.rectTransform.anchoredPosition = pos + new Vector3(Screen.width * 65f / 1280f, Screen.height * 210f / 894f, 0);

		tower = new GameObject("towerinfo");
		tower.transform.SetParent(FindObjectOfType<Canvas>().transform);
		towerInfo = tower.AddComponent<Text>();
		towerInfo.fontSize = 16;
		towerInfo.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		tower.layer = 5;
		towerInfo.color = Color.black;
		towerInfo.alignment = TextAnchor.MiddleCenter;
		towerInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200);
		towerInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 220);
		towerInfo.rectTransform.anchoredPosition = pos + new Vector3(0, -Screen.height * 315f/894f, 0);

		towerInfoString = "";

		buttons = new button[12];
		for(int i = 0;i<buttons.Length;i++)
		{
			buttons[i] = Instantiate(Button, new Vector3(7.8f + (i%3)*1.2f, 1.5f - (i/3)*1.5f, -1), Quaternion.identity).gameObject.GetComponent<button>();
			if (g.gameMode == 0)
			{
				buttons[i].buttonType = i;
			}
			else if (g.gameMode >= 1)
			{
				buttons[i].buttonType = 0;
			}
		}
		

		if (g.gameMode == 0)
		{
			buttons[9].buttonType = 47;
			buttons[10].buttonType = 48;
			buttons[11].buttonType = 49;
		}
		else if (g.gameMode >= 1)
		{
			buttons[1].buttonType = 1;
			buttons[2].buttonType = 2;
			buttonsUsed = 3;
		}
		

	}

	// Update is called once per frame
	void Update () {
		SetText();
	}
	
	void SetText()
	{
		overallInfo.text = "Wave : " + (g.waveIndex + 1) + "/" + g.numWaves + "\nLives: " + g.lives + "\nGold: " + g.gold + "\nTime Factor: " + g.timeFactor/2;

		if (g.waveIndex >= 0)
		{
			waveInfo.text = "Wave " + (g.waveIndex + 1) + "\n#: " + g.waves[g.waveIndex][0] + "\nHealth: " + g.waves[g.waveIndex][2];// + "\nSpacing: " + g.waves[g.waveIndex][1] + "\nPace: " + g.waves[g.waveIndex][3];
			image.sprite = ant.setSprite(g.waves[g.waveIndex][3], g.waves[g.waveIndex][1], g.lowSpeed, g.highSpeed, g.lowSpace, g.highSpace, g.enemySprites, g.timeFactor);
			image.transform.position = transform.position + new Vector3(-1f, 2.4f, -0.1f);
		}
		if (g.waveIndex < g.numWaves - 1)
		{
			nextWaveInfo.text = "Wave " + (g.waveIndex + 2) + "\n#: " + g.waves[g.waveIndex + 1][0] + "\nHealth: " + g.waves[g.waveIndex + 1][2];// + "\nSpacing: " + g.waves[g.waveIndex + 1][1] + "\nPace: " + g.waves[g.waveIndex+1][3];
			image2.sprite = ant.setSprite(g.waves[g.waveIndex + 1][3], g.waves[g.waveIndex + 1][1], g.lowSpeed, g.highSpeed, g.lowSpace, g.highSpace, g.enemySprites, g.timeFactor);
			image2.transform.position = transform.position + new Vector3(1f, 2.4f, -0.1f);
		}
		else
		{
			nextWaveInfo.text = "Wave " + (g.waveIndex + 1) + "\n#: " + g.waves[g.waveIndex][0] + "\nHealth: " + 2 * g.waves[g.waveIndex][2];// + "\nSpacing: " + g.waves[g.waveIndex][1] + "\nPace: " + g.waves[g.waveIndex][3];
			image2.sprite = ant.setSprite(g.waves[g.waveIndex][3], g.waves[g.waveIndex][1], g.lowSpeed, g.highSpeed, g.lowSpace, g.highSpace, g.enemySprites, g.timeFactor);
			image2.transform.position = transform.position + new Vector3(1f, 2.4f, -0.1f);
		}

		switch (g.currButtonActive)
		{
			case 0://blocker
				towerInfo.text = "Blocker\nBlockades enemies" + "\nCost: " + g.towerCosts[0];
				break;
			case 1://missile
				towerInfo.text = "Missile Tower\nShoots damaging missiles\nDamage: " + g.damages[1] + "\nRange: " + g.ranges[1] + "\nCooldown: " + g.cooldowns[1] + "\nCost: " + g.towerCosts[1];
				break;
			case 2://splash
				towerInfo.text = "Splash Tower\nShoots enemy and splashes onto other nearby enemies\nDamage: " + g.damages[2] + "\nRange: " + g.ranges[2] + "\nSplash radius: " + (g.ranges[2] + (g.defaultEffects[2] - 1.0) * 0.5) + "\nCooldown: " + g.cooldowns[2] + "\nCost: " + g.towerCosts[2];
				break;
			case 3://shock
				towerInfo.text = "Shock Tower\nShoots all enemies within its range\nDamage: " + g.damages[3] + "\nRange: " + g.ranges[3] + "\nCooldown: " + g.cooldowns[3] + "\nCost: " + g.towerCosts[3];
				break;
			case 4://beam
				towerInfo.text = "Beam Tower\nShoots all enemies in the direction its facing\nDamage: " + g.damages[4] + "\nCooldown: " + g.cooldowns[4] + "\nCost: " + g.towerCosts[4];
				break;
			case 5://coil
				towerInfo.text = "Coil Tower\nDamages all enemies that leave its range based on how long they were in range\nBase Damage: " + g.damages[5] + "\nRange: " + g.ranges[5]  + "\nCost: " + g.towerCosts[5];
				break;
			case 6://tesla
				towerInfo.text = "Tesla Tower\nSlows enemies that pass between it and another Tesla\nSlow Power: " + g.effects[6]  + "\nCost: " + g.towerCosts[6];
				break;
			case 7://bridge
				towerInfo.text = "Bridge Tower\nSends enemies across itself, if the other side is earlier on their route" + "\nCooldown: " + g.cooldowns[7] + "\nMax sendbacks per enemy: " + g.defaultEffects[7] + "\nCost: " + g.towerCosts[7];
				break;
			case 8://tag
				towerInfo.text = "Tag Tower\nShoots and permanently slows an enemy\nDamage: " + g.damages[8] + "\nSlow Power: " + g.effects[8] + "\nRange: " + g.ranges[8] + "\nCooldown: " + g.cooldowns[8] + "\nCost: " + g.towerCosts[8];
				break;
			case 9://missile2
				towerInfo.text = "Missile Tower 2\nShoots damaging missiles\nDamage: " + g.damages[9] + "\nRange: " + g.ranges[9] + "\nCooldown: " + g.cooldowns[9] + "\nCost: " + g.towerCosts[9];
				break;
			case 10://splash2
				towerInfo.text = "Splash Tower 2\nShoots enemy and splashes onto other nearby enemies\nDamage: " + g.damages[10] + "\nRange: " + g.ranges[10] + "\nSplash radius: " + (g.ranges[10] + (g.defaultEffects[10] - 1.0) * 0.5) + "\nCooldown: " + g.cooldowns[10] + "\nCost: " + g.towerCosts[10];
				break;
			case 11://shock2
				towerInfo.text = "Shock Tower 2\nShoots all enemies within its range\nDamage: " + g.damages[11] + "\nRange: " + g.ranges[11] + "\nCooldown: " + g.cooldowns[11] + "\nCost: " + g.towerCosts[11];
				break;
			case 12://beam2
				towerInfo.text = "Beam Tower 2\nShoots all enemies in the direction its facing\nDamage: " + g.damages[12] + "\nCooldown: " + g.cooldowns[12] + "\nCost: " + g.towerCosts[12];
				break;
			case 13://coil2
				towerInfo.text = "Coil Tower 2\nDamages all enemies that leave its range based on how long they were in range\nBase Damage: " + g.damages[13] + "\nRange: " + g.ranges[13] + "\nCost: " + g.towerCosts[13];
				break;
			case 14://tesla2
				towerInfo.text = "Tesla Tower 2\nSlows enemies that pass between it and another Tesla\nSlow Power: " + g.effects[14] + "\nCost: " + g.towerCosts[14];
				break;
			case 15://bridge2
				towerInfo.text = "Bridge Tower 2\nSends enemies across itself, if the other side is earlier on their route" + "\nCooldown: " + g.cooldowns[15] + "\nMax sendbacks per enemy: " + g.defaultEffects[15] + "\nCost: " + g.towerCosts[15];
				break;
			case 16://tag2
				towerInfo.text = "Tag Tower 2\nShoots and permanently slows an enemy\nDamage: " + g.damages[16] + "\nSlow Power: " + g.effects[16] + "\nRange: " + g.ranges[16] + "\nCooldown: " + g.cooldowns[16] + "\nCost: " + g.towerCosts[16];
				break;
			case 17://missile3
				towerInfo.text = "Missile Tower 3\nShoots damaging missiles\nDamage: " + g.damages[17] + "\nRange: " + g.ranges[17] + "\nCooldown: " + g.cooldowns[17] + "\nCost: " + g.towerCosts[17];
				break;
			case 18://splash3
				towerInfo.text = "Splash Tower 3\nShoots enemy and splashes onto other nearby enemies\nDamage: " + g.damages[18] + "\nRange: " + g.ranges[18] + "\nSplash radius: " + (g.ranges[18] + (g.defaultEffects[18] - 1.0) * 0.5) + "\nCooldown: " + g.cooldowns[18] + "\nCost: " + g.towerCosts[18];
				break;
			case 19://shock3
				towerInfo.text = "Shock Tower 3\nShoots all enemies within its range\nDamage: " + g.damages[19] + "\nRange: " + g.ranges[19] + "\nCooldown: " + g.cooldowns[19] + "\nCost: " + g.towerCosts[19];
				break;
			case 20://beam3
				towerInfo.text = "Beam Tower 3\nShoots all enemies in the direction its facing\nDamage: " + g.damages[20] + "\nCooldown: " + g.cooldowns[20] + "\nCost: " + g.towerCosts[20];
				break;
			case 21://coil3
				towerInfo.text = "Coil Tower 3\nDamages all enemies that leave its range based on how long they were in range\nBase Damage: " + g.damages[21] + "\nRange: " + g.ranges[21] + "\nCost: " + g.towerCosts[21];
				break;
			case 22://tesla3
				towerInfo.text = "Tesla Tower 3\nSlows enemies that pass between it and another Tesla\nSlow Power: " + g.effects[22] + "\nCost: " + g.towerCosts[22];
				break;
			case 23://bridge3
				towerInfo.text = "Bridge Tower 3\nSends enemies across itself, if the other side is earlier on their route" + "\nCooldown: " + g.cooldowns[23] + "\nMax sendbacks per enemy: " + g.defaultEffects[23] + "\nCost: " + g.towerCosts[23];
				break;
			case 24://tag3
				towerInfo.text = "Tag Tower 3\nShoots and permanently slows an enemy\nDamage: " + g.damages[24] + "\nSlow Power: " + g.effects[24] + "\nRange: " + g.ranges[24] + "\nCooldown: " + g.cooldowns[24] + "\nCost: " + g.towerCosts[24];
				break;
			case 25://missile4
				towerInfo.text = "Missile Tower 4\nShoots damaging missiles\nDamage: " + g.damages[25] + "\nRange: " + g.ranges[25] + "\nCooldown: " + g.cooldowns[25] + "\nCost: " + g.towerCosts[25];
				break;
			case 26://shocksplash
				towerInfo.text = "Shocksplash Tower\nShoots and splashes on all enemies in range\nDamage: " + g.damages[26] + "\nRange: " + g.ranges[26] + "\nSplash radius: " + (g.ranges[26] + (g.defaultEffects[26] - 1.0) * 0.5) + "\nCooldown: " + g.cooldowns[26] + "\nCost: " + g.towerCosts[26];
				break;
			case 27://tagbeam
				towerInfo.text = "Tagbeam Tower\nShoots and permanently slows all enemies in the direction its facing\nDamage: " + g.damages[27] + "\nSlow Power: " + g.effects[27] + "\nCooldown: " + g.cooldowns[27] + "\nCost: " + g.towerCosts[27];
				break;
			case 28://teslacoil
				towerInfo.text = "Teslacoil Tower\nActs as both a powerful Coil and Tesla tower\nBase Damage: " + g.damages[28] + "\nSlow Power: " + g.effects[28] + "\nRange: " + g.ranges[28] + "\nCost: " + g.towerCosts[28];
				break;
			case 46://effect boost
				towerInfo.text = "Effect Boost\nIncreases special effect of a single tower" + "\nAdded Effect Multiplier: " + g.effectBoost + "\nUpgrade amount: " + g.eBoostGain + "\nCost to upgrade: " + g.effectCost;
				break;
			case 47://damage boost
				towerInfo.text = "Damage Boost\nIncreases damage of a single tower" + "\nDamage Multiplier: " + g.damageBoost + "\nUpgrade amount: " + g.dBoostGain + "\nCost to upgrade: " + g.damageCost;
				break;
			case 48://range boost
				towerInfo.text = "Range Boost\nIncreases range of a single tower" + "\nBonus Range: " + g.rangeBoost + "\nUpgrade amount: " + g.rBoostGain + "\nCost to upgrade: " + g.rangeCost;
				break;
			case 49://speed boost
				towerInfo.text = "Speed Boost\nDecreases cooldown of a single tower" + "\nCooldown Multiplier: " + g.speedBoost + "\nUpgrade amount(multiplies): " + g.sBoostGain + "\nCost to upgrade: " + g.speedCost;
				break;
		}

		g.towerRange.transform.position = new Vector3(100, 100, 0);
		if (g.towerSelected && g.lastTowerSelected.status == g.lastTowerSelected.FILLED)
		{
			towerInfo.text = g.towerNames[g.lastTowerSelected.towerType] + "\nDamage: " + g.lastTowerSelected.damage + "\nRange: " + g.lastTowerSelected.range + "\nCooldown: " + g.lastTowerSelected.maxCooldown + "\nSlow Power: " + g.lastTowerSelected.effect * g.effects[g.lastTowerSelected.towerType] + "\nBonus Effect: " + (g.lastTowerSelected.effect - 1) +"\nSell cost: " + g.sellCosts[g.lastTowerSelected.towerType];
			g.towerRange.transform.position = g.lastTowerSelected.transform.position + new Vector3(0, 0, -.01f);
			float scaleFactor = (float)(g.lastTowerSelected.range / 2.5);
			g.towerRange.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
		}
	}
}
