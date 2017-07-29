using UnityEngine;

public class EntityPlayer : EntityBase
{

	void OnTriggerEnter2D( Collider2D other )
	{
		if( other.CompareTag( Structs.Elements.Capsule.ToString() ) )
		{
			// Recover the corresponding health
			int recover = other.GetComponent<Capsule>().healthRecover;
			//Debug.Log( other.tag + ": Recovering health " + recover );
			Director.Instance.DebugHealthIncrease( recover );

			// And destroy the capsule
			Destroy( other.gameObject );
		}
		else if( other.CompareTag( Structs.Elements.Star.ToString() ) )
		{
			// Remove the star from the remaining counter
			//Debug.Log( other.tag + ": Taking a star!" );
			Director.Instance.DebugStarTaken();

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

}
