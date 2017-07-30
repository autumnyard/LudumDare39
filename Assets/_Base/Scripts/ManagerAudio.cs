using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAudio : MonoBehaviour
{

	[SerializeField] private AudioSource songGame;
	[SerializeField] private AudioSource songMenu;
	[SerializeField] private AudioSource sfx01;
	[SerializeField] private AudioSource sfx02;
	[SerializeField] private AudioSource sfx03;

	void Awake()
	{
		Director.Instance.managerAudio = this;
	}


	public void PlaySongGame()
	{
		if( songGame.isPlaying )
		{
			songGame.UnPause();
		}
		else
		{
			songGame.Play();
		}
	}

	public void StopSongGame()
	{
		songGame.Pause();
	}

	public void PlaySongMenu()
	{
		songMenu.time = 0;
		songMenu.Play();
	}

	public void StopSongMenu()
	{
		songMenu.Stop();
	}

	public void PlaySfx1()
	{
		sfx01.Play();
	}
	public void PlaySfx2()
	{
		sfx02.Play();
	}
	public void PlaySfx3()
	{
		sfx03.Play();
	}
}
