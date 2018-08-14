using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class menuButton : MonoBehaviour {

	public Camera c;
	public game g;
	public audioScript a;

	public GameObject overall;
	public Text overallInfo;

	public Sprite standard;
	public Sprite highlighted;
	public int menuButtonType;
	Vector3 pos;
	// Use this for initialization
	void Start () {
		c = FindObjectOfType<Camera>();
		if (GameObject.FindGameObjectWithTag("game") != null)
		{
			g = GameObject.FindGameObjectWithTag("game").GetComponent<game>();
		}
		a = GameObject.FindGameObjectWithTag("audio").GetComponent<audioScript>();


		pos = new Vector3((Screen.width * (transform.position.x - c.transform.position.x) / (Screen.width * 20f / 1280f)), ((Screen.height * (transform.position.y - c.transform.position.y)) / (Screen.height * 14f / 1280f)), 0);

		overall = new GameObject("overall");
		overall.transform.SetParent(FindObjectOfType<Canvas>().transform);
		overallInfo = overall.AddComponent<Text>();
		overallInfo.fontSize = 36;
		overallInfo.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
		overall.layer = 5;
		overallInfo.color = Color.black;
		overallInfo.alignment = TextAnchor.MiddleCenter;
		overallInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100);
		overallInfo.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 450);
		overallInfo.rectTransform.anchoredPosition = pos;
		if (menuButtonType == 0)
		{
			overallInfo.text = "New Game";
		}
		else if (menuButtonType == 1)
		{
			overallInfo.text = "Exit";
		}
		else if (menuButtonType == 2)
		{
			overallInfo.text = "Instructions";
		}
		else if (menuButtonType == 3)
		{
			overallInfo.text = "Main Menu";
		}
		else if (menuButtonType == 4)
		{
			overallInfo.text = "Resume";
		}
		else if (menuButtonType == 5)
		{
			overallInfo.text = "Next";
		}
		else if (menuButtonType == 6)
		{
			overallInfo.text = "Next";
		}
		else if (menuButtonType == 7)
		{
			overallInfo.text = "Back";
		}
		else if (menuButtonType == 8)
		{
			overallInfo.text = "Back";
		}
		else if (menuButtonType == 9)
		{
			overallInfo.text = "Back";
		}
		else if (menuButtonType == 10)
		{
			overallInfo.text = "Next";
		}
		else if (menuButtonType == 11)
		{
			overallInfo.text = "Mute/Unmute";
		}
		else if (menuButtonType == 12)
		{
			overallInfo.text = "Option 1(active songs)";
		}
		else if (menuButtonType == 13)
		{
			overallInfo.text = "Option 2(background)";
		}
		else if (menuButtonType == 14)
		{
			overallInfo.text = "Audio";
		}
		else if (menuButtonType == 15)
		{
			overallInfo.text = "Keep Playing";
		}
	}

	// Update is called once per frame
	void Update () {
		pos = new Vector3((Screen.width * (7f /5f) * (transform.position.x - c.transform.position.x) / (Screen.width * 20f / 1280f)), ((Screen.height * (transform.position.y - c.transform.position.y)) / (Screen.height * 14f / 1280f)), 0);
		overallInfo.rectTransform.anchoredPosition = pos;

	}

	private void OnMouseEnter()
	{
		this.GetComponent<SpriteRenderer>().sprite = highlighted;
	}

	private void OnMouseExit()
	{
		this.GetComponent<SpriteRenderer>().sprite = standard;
	}

	private void OnMouseDown()
	{
		if (menuButtonType == 0)
		{
			SceneManager.LoadScene("SampleScene");
		}
		else if (menuButtonType == 1)
		{
			Application.Quit();
		}
		else if (menuButtonType == 2)
		{
			SceneManager.LoadScene("instructions");
		}
		else if (menuButtonType == 3)
		{
			SceneManager.LoadScene("mainMenu");
		}
		else if (menuButtonType == 4)
		{
			g.paused = false;
			c.transform.position = new Vector3(2, 0, -10);
			c.orthographicSize = 7;
		}
		else if (menuButtonType == 5)
		{
			SceneManager.LoadScene("instructions2");
		}
		else if (menuButtonType == 6)
		{
			SceneManager.LoadScene("instructions3");
		}
		else if (menuButtonType == 7)
		{
			SceneManager.LoadScene("instructions2");
		}
		else if (menuButtonType == 8)
		{
			SceneManager.LoadScene("instructions");
		}
		else if (menuButtonType == 9)
		{
			SceneManager.LoadScene("instructions3");
		}
		else if (menuButtonType == 10)
		{
			SceneManager.LoadScene("instructions4");
		}
		else if (menuButtonType == 11)
		{
			a.manageMute();
		}
		else if (menuButtonType == 12)
		{
			a.setSongSet1();
		}
		else if (menuButtonType == 13)
		{
			a.setSongSet2();
		}
		else if (menuButtonType == 14)
		{
			SceneManager.LoadScene("audioControl");
		}
		else if (menuButtonType == 15)
		{
			g.paused = false;
			c.transform.position = new Vector3(2, 0, -10);
			c.orthographicSize = 7;
			g.endGameText.rectTransform.anchoredPosition = new Vector3(0, 10000, 0);
		}
	}
}
