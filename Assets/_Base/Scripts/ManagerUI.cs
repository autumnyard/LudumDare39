using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
	[Header( "Components" ), SerializeField]
	private PanelBase panelMenu;
	[SerializeField]
	private PanelBase panelHUD;
	[SerializeField]
	private PanelBase panelLoading;
	[SerializeField]
	private PanelBase panelDebug;
	[SerializeField]
	private PanelBase panelScore;

	// Panel HUD
	[Header( "Ingame HUD" ), SerializeField] private UnityEngine.UI.Text health;
	[SerializeField] public Slider healthSlider;
	[SerializeField] private UnityEngine.UI.Text stars;
	//[SerializeField] private UnityEngine.UI.Text score;
	//[SerializeField] private UnityEngine.UI.Text enemycount;
	[SerializeField] private UnityEngine.UI.Text currentTime;
	[SerializeField] private UnityEngine.UI.Text bestTime;
	[SerializeField] private UnityEngine.UI.Text lastTime;

	private const string healthText = "Health: ";
	private const string starsText = "V H S > ";
	private const string bestTimeText = "Best time: ";
	private const string lastTimeText = "Last time: ";
	private const string currentTimeText = "time:";

	void Awake()
	{
		Director.Instance.managerUI = this;
	}

	private void Start()
	{
		healthSlider.minValue = 0;
		healthSlider.maxValue = GameManager.maxHealth;
	}

	//private void Update()
	//{
	//	if( Director.Instance.currentScene == Structs.GameScene.Ingame )
	//	{
	//	}
	//}

	#region Panel management
	public void SetPanels()
	{
		switch( Director.Instance.currentScene )
		{
			case Structs.GameScene.Menu:
				panelMenu.Show();
				panelHUD.Hide();
				panelLoading.Hide();
				panelDebug.Hide();
				panelScore.Hide();
				break;

			case Structs.GameScene.Ingame:
				panelMenu.Hide();
				panelHUD.Show();
				panelLoading.Hide();
				//panelDebug.Show(); // For the final version, no debug menu!
				panelScore.Hide();
				break;

			case Structs.GameScene.LoadingGame:
				panelMenu.Hide();
				panelHUD.Hide();
				panelLoading.Show();
				panelDebug.Hide();
				panelScore.Hide();
				break;

			case Structs.GameScene.Score:
				panelMenu.Hide();
				panelHUD.Hide();
				panelLoading.Hide();
				panelDebug.Hide();
				panelScore.Show();
				break;

			default:
				panelMenu.Hide();
				panelHUD.Hide();
				panelLoading.Hide();
				panelDebug.Hide();
				panelScore.Hide();
				break;
		}
	}
	#endregion

	#region Here lies the HUD management
	public void SetHealth( float newHealth )
	{
		if( newHealth < 0 )
		{
			health.text = healthText + "--";
		}
		else
		{
			health.text = healthText + newHealth.ToString( "00" );
		}

		healthSlider.value = newHealth;
	}


	public void SetStars( int to )
	{
		if( to < 0 )
		{
			stars.text = starsText + "--";
		}
		else
		{
			stars.text = starsText + to.ToString( "0" );
		}
	}

	public void SetScore( int best, int last )
	{
		/*
		Best time: -- 
		Last time: --
		*/
		if( best < 0 )
		{
			bestTime.text = bestTimeText + "-- seconds";
		}
		else
		{
			bestTime.text = bestTimeText + best.ToString( /*"000"*/ ) + " seconds";
		}

		if( last < 0 )
		{
			lastTime.text = lastTimeText + "-- seconds";
		}
		else
		{
			lastTime.text = lastTimeText + last.ToString( /*"000"*/ ) + " seconds";
		}
	}

	public void SetCurrentTime( int to )
	{
		if( to < 0 )
		{
			currentTime.text = currentTimeText + "--";
		}
		else
		{
			currentTime.text = currentTimeText + to.ToString( /*"000"*/ ) + "";
		}
	}
	#endregion
}
