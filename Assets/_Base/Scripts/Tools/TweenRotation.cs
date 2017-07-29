using UnityEngine;
using DG.Tweening;

public class TweenRotation : MonoBehaviour
{
    public bool playOnStart = true;
    public float rotationInit = -20f;
    public float rotationFinish = 20f;
    public float time = 1f;
    public Ease ease;
    public bool loop = false;
    public LoopType loopType;

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
        // if (rotationInit != -1)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, rotationInit);
        }
        transform.DORotate(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, rotationFinish), time)
                      .SetEase(ease)
                      .SetLoops(loops, loopType);
    }
}
