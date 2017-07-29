using UnityEngine;
using DG.Tweening;

public class TweenPosition : MonoBehaviour {

    public bool playOnStart = true;
    public Vector3 finalPosition;
    public float time = 1f;
    public Ease ease;

    Tweener tween;

    void Start()
    {
        if (playOnStart)
        {
            Play();
        }
    }


    public void Play()
    {
        tween = transform.DOMove(finalPosition, time).SetEase(ease);
    }

    public void Stop()
    {
        tween.Kill();
    }
}
