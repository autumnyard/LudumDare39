using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMotion : MonoBehaviour
{

    [SerializeField] private float speed = 0.3f;
    [SerializeField] private float radius = 5f;
    //private Vector2 center;
    public Vector2 center = Vector2.zero;

    private float _angle;

    private void Start()
    {
        // _centre = transform.position;
        //center = Vector2.zero;
        //radius = 5f;
        //speed = 0.4f;
    }

    private void Update()
    {

        _angle += speed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * radius;
        transform.position = center + offset;
    }
}
