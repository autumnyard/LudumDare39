using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMap : MonoBehaviour
{

	new private SpriteRenderer renderer;

	public bool switchColours;
	public Color color1;
	public Color color2;
	public float timeMultiplier = 0.5f;
	private float color1Hue;
	private float color2Hue;
	private float hueLerped;
	private float s = 1f;
	private float v = 1f;


	void Start()
	{
		renderer = GetComponent<SpriteRenderer>();

		if( switchColours )
		{
			// Get the hues to the inputted colors
			Color.RGBToHSV( color1, out color1Hue, out s, out v );
			Color.RGBToHSV( color2, out color2Hue, out s, out v );
		}
		else
		{
			renderer.color = color1;
		}

	}


	void Update()
	{

		if( switchColours )
		{
			//lerpedColor = Color.Lerp( Color.cyan, Color.yellow, Mathf.PingPong( Time.time, 1 ) );
			hueLerped = Mathf.Lerp( color1Hue, color2Hue, Mathf.PingPong( Time.time * timeMultiplier, 1 ) );
			renderer.color = Color.HSVToRGB( hueLerped, s, v );
		}
	}
}
