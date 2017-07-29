using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenAlpha : MonoBehaviour {

    public bool playOnStart = true;
    public float time = 1f;
    public float alphaEnd = 0;
    public Ease ease;

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
        GetComponent<SpriteRenderer>().DOFade(alphaEnd, time).SetEase(ease);
    }
}
