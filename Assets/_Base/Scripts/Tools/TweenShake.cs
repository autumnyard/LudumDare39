using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenShake : MonoBehaviour {

    public bool playOnStart = true;
    public float time = 1f;
    public Vector3 strength = Vector3.zero;
    public int vibrato = 100;

    // Use this for initialization
    void Start()
    {
        if (playOnStart)
        {
            Play();
        }
    }


    public void Play()
    {
        transform.DOShakePosition(time, strength, vibrato);
    }
}
