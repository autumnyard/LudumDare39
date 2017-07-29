using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Variables
	[SerializeField] private int maxHealth = 20;
	[SerializeField] private int health;
	//public int health;

	// Finish game condition
	public delegate void Delegate();
	public Delegate OnPlayerDeath;
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


	#region Game management!
	public void ResetHealth()
	{
		health = maxHealth;
	}

	public int HealthDecrease()
	{
		health--;

		if( health == 0 )
		{
			if( OnPlayerDeath != null )
			{
				OnPlayerDeath();
			}
		}

		return health;
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
}
