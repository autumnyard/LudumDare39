using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Panel HUD
	[Header( "Ingame HUD" ), SerializeField] private UnityEngine.UI.Text health;
	[SerializeField] private UnityEngine.UI.Text stars;
	//[SerializeField] private UnityEngine.UI.Text score;
	//[SerializeField] private UnityEngine.UI.Text enemycount;

	private const string healthText = "Health: ";
	private const string starsText = "Remaining stars: \n";

	void Awake()
	{
		Director.Instance.managerUI = this;
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
				break;

			case Structs.GameScene.Ingame:
				panelMenu.Hide();
				panelHUD.Show();
				panelLoading.Hide();
				//panelDebug.Show();
				break;

			case Structs.GameScene.LoadingGame:
				panelMenu.Hide();
				panelHUD.Hide();
				panelLoading.Show();
				panelDebug.Hide();
				break;

			default:
				panelMenu.Hide();
				panelHUD.Hide();
				panelLoading.Hide();
				panelDebug.Hide();
				break;
		}
	}
	#endregion

	#region Inagem HUD management
	public void SetHealth( int newHealth )
	{
		if( newHealth < 0 )
		{
			health.text = healthText + "--";
		}
		else
		{
			health.text = healthText + newHealth.ToString( "00" );
		}
	}

	public void SetStars( int to)
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
	#endregion
}
