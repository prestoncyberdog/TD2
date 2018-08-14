using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioScript : MonoBehaviour {

	public AudioClip[] songs;
	public AudioClip[] songSet1 = new AudioClip[12];
	public AudioClip[] songSet2 = new AudioClip[12];
	public AudioClip[] starters1 = new AudioClip[2];

	public int songIndex;
	public AudioSource musicSource;

	// Use this for initialization
	void Start () {
		if (GameObject.FindGameObjectsWithTag("audio").Length != 1)
		{
			Destroy(gameObject);
			return;
		}

		//ideally should scramble songs list
		songs = songSet1;
		musicSource.volume = .6f;
		songIndex = 0;
		scrambleSongs();
		musicSource.clip = songs[0];
		DontDestroyOnLoad(this);
		musicSource.Play();
		Invoke("playNextSong", musicSource.clip.length);

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			manageMute();
		}
		/*
		if (Input.GetKeyDown(KeyCode.Q))
		{
			manageMute();
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			setSongSet1();
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			setSongSet2();
		}*/
	}

	public void setSongSet1 ()
	{
		musicSource.Stop();
		CancelInvoke();
		songs = new AudioClip[songSet1.Length];
		for (int i=0;i<songSet1.Length;i++)
		{
			songs[i] = songSet1[i];
		}
		//songs = songSet1;
		scrambleSongs();
		musicSource.clip = songs[0];
		musicSource.Play();
		Invoke("playNextSong", musicSource.clip.length);
	}

	public void setSongSet2()
	{
		musicSource.Stop();
		CancelInvoke();
		songs = new AudioClip[songSet2.Length];
		for (int i = 0; i < songSet2.Length; i++)
		{
			songs[i] = songSet2[i];
		}
		//songs = songSet2;
		musicSource.clip = songs[0];
		musicSource.Play();
		Invoke("playNextSong", musicSource.clip.length);
	}

	public void manageMute()
	{
		musicSource.mute = !musicSource.mute;
	}

	void playNextSong()
	{
		songIndex++;
		if (songIndex >= songs.Length)
		{
			songIndex = 0;
		}
		musicSource.Stop();
		musicSource.clip = songs[songIndex];
		musicSource.Play();
		Invoke("playNextSong", musicSource.clip.length);
	}

	public void scrambleSongs ()
	{
		int choice = Random.Range(0, starters1.Length);
		//swapSongs(0, choice);
		int choice2;
		for (int i = 1;i<songs.Length;i++)//dont scramble first song
		{
			choice2 = Random.Range(1, songs.Length);
			swapSongs(i, choice2);
		}
		for (int i=0;i<songs.Length;i++)
		{
			if (songs[i] == starters1[choice])
			{
				swapSongs(0, i);
				break;
			}
		}
	}

	public void swapSongs (int a, int b)
	{
		AudioClip temp = songs[a];
		songs[a] = songs[b];
		songs[b] = temp;
	}
}
