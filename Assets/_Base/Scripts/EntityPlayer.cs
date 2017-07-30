using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EntityPlayer : EntityBase
{
	private void Start()
	{
		canDash = false;
		dashTimerCoroutine = StartCoroutine( DashTimer() );
	}

	void OnTriggerEnter2D( Collider2D other )
	{
		if( other.CompareTag( Structs.Elements.Capsule.ToString() ) )
		{
			// Recover the corresponding health
			int recover = other.GetComponent<Capsule>().healthRecover;
			//Debug.Log( other.tag + ": Recovering health " + recover );
			Director.Instance.DebugHealthIncrease( recover );

			// Play sound!
			Director.Instance.managerAudio.PlaySfx1();

			// And destroy the capsule
			Destroy( other.gameObject );
		}
		else if( other.CompareTag( Structs.Elements.Star.ToString() ) )
		{
			// Remove the star from the remaining counter
			//Debug.Log( other.tag + ": Taking a star!" );
			Director.Instance.DebugStarTaken();

			// Play sound!
			Director.Instance.managerAudio.PlaySfx2();

			// And destroy the star
			Destroy( other.gameObject );
		}
		else if( other.CompareTag( Structs.Elements.End.ToString() ) )
		{
			// Check whether enough stars or not
			//Debug.Log( other.tag + ": Checking if level shall be finished." );
			Director.Instance.CheckEndGameCondition();
		}

	}


	public float dashTime = 0.5f;
	public float dashAgainTime = 0.5f;
	public float dashAgainScale = 1.3f;
	public float timeAnimationDashAgain = 0.1f;
	[SerializeField, Range( 0.1f, 10f )] private float dashSpeed = 6f;
	Coroutine dashTimerCoroutine;

	public void Dash()
	{
		// Espera a que pueda hacer dash
		if( canDash == false )
		{
			return;
		}

		canDash = false;
		ChangeState( States.Dashing );
		dashTimerCoroutine = StartCoroutine( DashTimer() );

		// TRAIL
		////trail.time = 0.5f;

		/*
        // GHOST
        Debug.Log(" + Start dash");
        GetComponent<GhostSprites>().enabled = true;
        */
		////if( sfxDash != null )
		////{
		////	sfxDash.Play();
		////}

		Vector2 direction = Vector2.zero;

		if( Input.GetKey( KeyCode.W ) )
		{
			direction.y = 1;
		}
		else if( Input.GetKey( KeyCode.S ) )
		{
			direction.y = -1;
		}

		if( Input.GetKey( KeyCode.A ) )
		{
			direction.x = -1;
		}
		else if( Input.GetKey( KeyCode.D ) )
		{
			direction.x = 1;
		}

		// If we are not pressing anything, dash on the current direction
		if( direction == Vector2.zero )
		{
			rigidbody.AddForce( rigidbody.velocity.normalized * dashSpeed, ForceMode2D.Impulse );
		}
		else // If we are pressing on a certain direction, dash over there
		{
			rigidbody.velocity = Vector2.zero;
			rigidbody.AddForce( direction.normalized * dashSpeed, ForceMode2D.Impulse );
		}
	}


	IEnumerator DashTimer()
	{
		yield return new WaitForSeconds( dashTime );
		if( currentState == States.Dashing )
		{
			ChangeState( States.Normal );
			//trail.time = 0.1f;
		}

		yield return new WaitForSeconds( dashAgainTime );
		canDash = true;
		//transform.GetChild( 0 ).DOScale( new Vector3( transform.localScale.x * dashAgainScale, transform.localScale.y * dashAgainScale, transform.localScale.z ), timeAnimationDashAgain )
		//			  .SetEase( Ease.InOutCubic )
		//		  .SetLoops( 2, LoopType.Yoyo );
	}

}
