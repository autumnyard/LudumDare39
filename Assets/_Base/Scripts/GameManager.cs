using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Variables
	[SerializeField] private int maxHealth = 20;
	[SerializeField] private int health;
	[SerializeField] private int stars;

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

	public int HealthDecrease()
	{
		health--;

		if( health <= 0 )
		{
			if( OnPlayerDeath != null )
			{
				OnPlayerDeath();
			}
		}

		return health;
	}

	public int HealthIncrease( int howMuch )
	{
		health += howMuch;
		if( health > maxHealth )
		{
			health = maxHealth;
		}

		return health;
	}

	#endregion


	#region Star management 
	public void SetStars( int to )
	{
		stars = to;
	}

	public int StarsDecrease()
	{
		stars--;
		if( stars <= 0 )
		{
			if( OnNoStars != null )
			{
				OnNoStars();
			}
		}

		return stars;
	}
	#endregion
}
