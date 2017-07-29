using UnityEngine;
using DG.Tweening;

public class TweenScale : MonoBehaviour
{
    public bool playOnStart = true;
    public float scaleInit = 0.1f;
    public float scaleFinish = 2f;
    public float time = 1f;
    public Ease ease;
    public bool loop = false;

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
        int loops = (loop) ? -1 : 0;
        if (scaleFinish >= 0)
        {
            transform.localScale.Set(scaleInit, scaleInit, transform.localScale.z);
        }
        transform.DOScale(new Vector3(scaleFinish, scaleFinish, transform.localScale.z), time)
                      .SetEase(ease)
                      .SetLoops(loops);
    }
}
