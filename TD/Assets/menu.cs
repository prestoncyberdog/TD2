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
	public GameObject tower;
	public Text towerInfo;

	public Transform Button;
	public button[] buttons;
	public int buttonsUsed;


	// Use this for initialization
	void Start () {
		g = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
		Vector3 pos = new Vector3((Screen.width * (transform.position.x - 2) / (Screen.width * 20f / 1280f)), (Screen.height * transform.position.y / (Screen.height * 14f / 1280f)), 0);

		overall = new GameObject("overall");
		overall.transform.SetParent(FindObjectOfType<Canvas>().transform);
		overallInfo = overall.AddComponent<Text>();
		overallInfo.fontSize = 30;
		overallInfo.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		overall.layer = 5;
		overallInfo.color = Color.black;
		overallInfo.alignment = TextAnchor.MiddleCenter;
		overallInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200);
		overallInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
		overallInfo.rectTransform.anchoredPosition = pos + new Vector3(0, Screen.height * 330f / 894f, 0);

		wave = new GameObject("waveinfo");
		wave.transform.SetParent(FindObjectOfType<Canvas>().transform);
		waveInfo = wave.AddComponent<Text>();
		waveInfo.fontSize = 20;
		waveInfo.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		wave.layer = 5;
		waveInfo.color = Color.black;
		waveInfo.alignment = TextAnchor.MiddleCenter;
		waveInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200);
		waveInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
		waveInfo.rectTransform.anchoredPosition = pos + new Vector3(0, Screen.height * 210f / 894f, 0);

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
		towerInfo.rectTransform.anchoredPosition = pos + new Vector3(0, -Screen.height * 310f/894f, 0);

		buttons = new button[12];
		for(int i = 0;i<buttons.Length;i++)
		{
			buttons[i] = Instantiate(Button, new Vector3(7.8f + (i%3)*1.2f, 1.5f - (i/3)*1.5f, -1), Quaternion.identity).gameObject.GetComponent<button>();
			if (g.gameMode == 0)
			{
				buttons[i].buttonType = i;
			}
			else if (g.gameMode == 1)
			{
				buttons[i].buttonType = 0;
			}
		}
		

		if (g.gameMode == 0)
		{
			buttons[9].buttonType = 17;
			buttons[10].buttonType = 18;
			buttons[11].buttonType = 19;
		}
		else if (g.gameMode == 1)
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
		overallInfo.text = "Wave : " + (g.waveIndex + 1) + "/" + g.numWaves + "\nLives: " + g.lives + "\nGold: " + g.gold;
		if (g.waveIndex < g.numWaves - 1)
		{
			waveInfo.text = "Next: Wave " + (g.waveIndex + 2) + "\n# of creeps: " + g.waves[g.waveIndex + 1][0] + "\nHealth: " + g.waves[g.waveIndex + 1][2] + "\nSpacing: " + g.waves[g.waveIndex + 1][1] + "\nSpeed: " + Mathf.Round(100 * (10.0f / (g.waves[g.waveIndex + 1][3] + 0.0f))) / 100f;

		}
		else
		{
			waveInfo.text = "Next: Wave " + (g.waveIndex + 1) + "\n# of creeps: " + g.waves[g.waveIndex][0] + "\nHealth: " + g.waves[g.waveIndex][2] + "\nSpacing: " + g.waves[g.waveIndex][1] + "\nSpeed: " + Mathf.Round(100 * (10.0f / (g.waves[g.waveIndex][3] + 0.0f))) / 100f;
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
				towerInfo.text = "Splash Tower\nShoots enemy and splashes onto other nearby enemies\nDamage: = " + g.damages[2] + "\nRange/Radius: " + g.ranges[2] + "\nCooldown: " + g.cooldowns[2] + "\nCost: " + g.towerCosts[2];
				break;
			case 3://shock
				towerInfo.text = "Shock Tower\nShoots all enemies within its range\nDamage: = " + g.damages[3] + "\nRange: " + g.ranges[3] + "\nCooldown: " + g.cooldowns[3] + "\nCost: " + g.towerCosts[3];
				break;
			case 4://beam
				towerInfo.text = "Beam Tower\nShoots all enemies in the direction its facing\nDamage: " + g.damages[4] + "\nCooldown: " + g.cooldowns[4] + "\nCost: " + g.towerCosts[4];
				break;
			case 5://coil
				towerInfo.text = "Coil Tower\nShoots all enemies that leave its range based on how long they were in range\nBase Damage: = " + g.damages[5] + "\nRange: " + g.ranges[5]  + "\nCost: " + g.towerCosts[5];
				break;
			case 6://tesla
				towerInfo.text = "Tesla Tower\nSlows enemies that pass between it and another Tesla\nSlow Power: = " + g.damages[6]  + "\nCost: " + g.towerCosts[6];
				break;
			case 7://bridge
				towerInfo.text = "Bridge Tower\nSends enemies across itself, if the other side is earlier on their route" + "\nCooldown: " + g.cooldowns[7] + "\nCost: " + g.towerCosts[7];
				break;
			case 8://tag
				towerInfo.text = "Tag Tower\nShoots and permanently slows an enemy\nDamage/Slow Power: = " + g.damages[8] + "\nRange: " + g.ranges[8] + "\nCooldown: " + g.cooldowns[8] + "\nCost: " + g.towerCosts[8];
				break;
			case 9://missile2
				towerInfo.text = "Missile Tower 2\nShoots damaging missiles\nDamage: " + g.damages[9] + "\nRange: " + g.ranges[9] + "\nCooldown: " + g.cooldowns[9] + "\nCost: " + g.towerCosts[9];
				break;
			case 10://splash2
				towerInfo.text = "Splash Tower 2\nShoots enemy and splashes onto other nearby enemies\nDamage: = " + g.damages[10] + "\nRange/Radius: " + g.ranges[10] + "\nCooldown: " + g.cooldowns[10] + "\nCost: " + g.towerCosts[10];
				break;
			case 11://shock2
				towerInfo.text = "Shock Tower 2\nShoots all enemies within its range\nDamage: = " + g.damages[11] + "\nRange: " + g.ranges[11] + "\nCooldown: " + g.cooldowns[11] + "\nCost: " + g.towerCosts[11];
				break;
			case 12://beam2
				towerInfo.text = "Beam Tower 2\nShoots all enemies in the direction its facing\nDamage: " + g.damages[12] + "\nCooldown: " + g.cooldowns[12] + "\nCost: " + g.towerCosts[12];
				break;
			case 13://coil2
				towerInfo.text = "Coil Tower 2\nShoots all enemies that leave its range based on how long they were in range\nBase Damage: = " + g.damages[13] + "\nRange: " + g.ranges[13] + "\nCost: " + g.towerCosts[13];
				break;
			case 14://tesla2
				towerInfo.text = "Tesla Tower 2\nSlows enemies that pass between it and another Tesla\nSlow Power: = " + g.damages[14] + "\nCost: " + g.towerCosts[14];
				break;
			case 15://bridge
				towerInfo.text = "Bridge Tower 2\nSends enemies across itself, if the other side is earlier on their route" + "\nCooldown: " + g.cooldowns[15] + "\nCost: " + g.towerCosts[15];
				break;
			case 16://tag
				towerInfo.text = "Tag Tower 2\nShoots and permanently slows an enemy\nDamage/Slow Power: = " + g.damages[16] + "\nRange: " + g.ranges[16] + "\nCooldown: " + g.cooldowns[16] + "\nCost: " + g.towerCosts[16];
				break;
			case 17://damage boost
				towerInfo.text = "Damage Boost\nIncreases damage of a single tower" + "\nDamage Multiplier: " + g.damageBoost + "\nUpgrade amount: " + g.dBoostGain + "\nCost to upgrade: " + g.damageCost;
				break;
			case 18://range boost
				towerInfo.text = "Range Boost\nIncreases range of a single tower" + "\nBonus Range: " + g.rangeBoost + "\nUpgrade amount: " + g.rBoostGain + "\nCost to upgrade: " + g.rangeCost;
				break;
			case 19://speed boost
				towerInfo.text = "Speed Boost\nDecreases cooldown of a single tower" + "\nCooldown Multiplier: " + g.speedBoost + "\nUpgrade amount(multiplies): " + g.sBoostGain + "\nCost to upgrade: " + g.speedCost;
				break;
		}
	}
}
