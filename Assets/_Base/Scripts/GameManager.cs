using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Variables
	public const float maxHealth = 20;

	[SerializeField] public float health { private set; get; }
	[SerializeField] public int stars { private set; get; }

	//public int health;

	// Finish game condition
	public delegate void Delegate();
	public Delegate OnPlayerDeath;
	public Delegate OnNoStars;
	#endregion

	#region Monobehaviour
	void Awake()
	{
		Director.Instance.gameManager = this;
	}

	private void Start()
	{
		Director.Instance.EverythingBeginsHere();
	}
	#endregion


	#region Health management
	public void ResetHealth()
	{
		health = maxHealth;
	}

	public void HealthDecrease()
	{
		health--;

		if( health <= 0 )
		{
			if( OnPlayerDeath != null )
			{
				OnPlayerDeath();
			}
		}
	}

	public void HealthDecreaseFloat( float quant )
	{
		health -= quant;

		if( health <= 0 )
		{
			if( OnPlayerDeath != null )
			{
				OnPlayerDeath();
			}
		}

	}

	public void HealthIncrease( int howMuch )
	{
		health += howMuch;
		if( health > maxHealth )
		{
			health = maxHealth;
		}
	}

	#endregion


	#region Star management 
	public void SetStars( int to )
	{
		stars = to;
	}

	public void StarsDecrease()
	{
		stars--;
		if( stars <= 0 )
		{
			stars = 0;
			// No need for this, endgame will be triggered by EntityPlayer 
			// when it enters an End trigger
			//	if( OnNoStars != null )
			//	{
			//		OnNoStars();
			//	}
		}
	}

	#endregion
}
