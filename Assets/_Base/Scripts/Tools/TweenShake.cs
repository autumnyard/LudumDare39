using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenShake : MonoBehaviour
{

	private Tweener thisTween;
	public bool playOnStart = true;
	public float time = 1f;
	public Vector3 strength = Vector3.zero;
	public int vibrato = 100;
	public bool loop = false;

	// Use this for initialization
	void Start()
	{
		if( playOnStart )
		{
			Play();
		}
	}


	public void Play()
	{
        int loops = (loop) ? -1 : 0;
		thisTween = transform.DOShakePosition( time, strength, vibrato )
					  .SetLoops( loops );
	}

	public void Stop()
	{
		if( thisTween.IsPlaying() )
		{
			thisTween.Pause();
		}
	}

	public void Update()
	{

		if( thisTween.IsPlaying() )
		{
			thisTween.Restart();
			thisTween.Play();
		}
	}
}
