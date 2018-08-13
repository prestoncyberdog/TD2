using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioScript : MonoBehaviour {

	public AudioClip[] songs;
	public AudioClip[] songSet1 = new AudioClip[12];
	public AudioClip[] songSet2 = new AudioClip[12];

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
		songs = songSet1;
		scrambleSongs();
		musicSource.clip = songs[0];
		musicSource.Play();
		Invoke("playNextSong", musicSource.clip.length);
	}

	public void setSongSet2()
	{
		musicSource.Stop();
		CancelInvoke();
		songs = songSet2;
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
		int choice;
		for (int i = 0;i<songs.Length;i++)
		{
			choice = Random.Range(0, songs.Length);
			swapSongs(i, choice);
		}
	}

	public void swapSongs (int a, int b)
	{
		AudioClip temp = songs[a];
		songs[a] = songs[b];
		songs[b] = temp;
	}
}
