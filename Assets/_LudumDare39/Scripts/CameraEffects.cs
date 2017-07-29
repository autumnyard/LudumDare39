using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
	public TweenRotation tweenRotationConstant1;

	// Shaking relative to health
	public TweenShake tweenShaking;
	private const float minShaking = 0f;
	private const float maxShaking = 1f;
	private float shakingToHealthRatio;
	[SerializeField] private float healthShakeCutoff1 = 7f;
	[SerializeField] private float healthShakeCutoff2 = 4f;
	[SerializeField] private float healthShakeCutoff3 = 2f;

	// Distance relative to health
	private const float minDistance = -11f; // Remember these values are negative!
	private const float maxDistance = -8f; // This is the closest
	private float distanceToHealthRatio;

	private void Start()
	{
		// If maxHealth is 20, and the distance is 4, ratio will be 5
		distanceToHealthRatio = GameManager.maxHealth / (minDistance - maxDistance);

		// If maxHealth is 20, and the shaking is 0.7, ratio will be 28.6
		shakingToHealthRatio = GameManager.maxHealth / (maxShaking - minShaking);

	}

	private void LateUpdate()
	{
		if( Director.Instance.gameRunning )
		{
			// Gonna use the health plenty
			float health = Director.Instance.gameManager.health;

			// The shakiest the camera gets the lowest the health is
			//float newStrenght = maxShaking - health / shakingToHealthRatio;
			//tweenShaking.strength = new Vector3( newStrenght, newStrenght );
			//tweenShaking.Update();
			if( health <= healthShakeCutoff2 )
			{
				//float newStrenght =  health / shakingToHealthRatio;
				//tweenShaking.strength = new Vector3( newStrenght, newStrenght );
				tweenShaking.strength = new Vector3( 0.1f, .1f );
				//tweenShaking.Play();
			}
			else
			{
				//tweenShaking.Stop();
			}

			// The camera getting farther the lowest the health is
			var newPos = transform.position;
			float newZ = Director.Instance.gameManager.health / distanceToHealthRatio;
			newZ += maxDistance;
			newPos.z = newZ;
			transform.position = newPos;
		}
	}


	public void Play()
	{
		tweenRotationConstant1.Play();
		//tweenShaking.Play();
	}

	public void Reset()
	{
		tweenRotationConstant1.Stop();
		//tweenShaking.Stop();
	}
}
