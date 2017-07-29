﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class Director : MonoBehaviour
{

	#region Variables
	public GameManager gameManager;
	public ManagerCamera managerCamera;
	//public Player player;
	public ManagerMap managerMap;
	public ManagerEntity managerEntity;
	//public WavesManager waveManager;
	public ManagerInput managerInput;
	public ManagerUI managerUI;
	//public ScoreManager scoreManager;
	public ManagerAudio managerAudio;


	public Structs.GameMode currentGameMode { private set; get; }
	public Structs.GameDifficulty currentGameDifficulty { private set; get; }
	public Structs.GameView currentGameView { private set; get; }
	public Structs.GameScene currentScene;

	private int maxLevelNumber = 2;
	public int currentLevel = 0;

	public bool isPaused;

	// Constant health decrease variables
	public bool gameRunning { private set; get; }
	public float healthDecreaseTime = 1; // 1 second / health point
	private float healthDecreaseCounter;
	private bool movingPlayer = false;
	#endregion


	#region Singleton
	private static Director instance;

	public static Director Instance
	{
		get { return instance; }
	}

	static Director()
	{
		GameObject obj = GameObject.Find( "Director" );

		if( obj == null )
		{
			obj = new GameObject( "Director", typeof( Director ) );
		}

		instance = obj.GetComponent<Director>();
	}
	#endregion


	#region Monobehaviour
	private void Awake()
	{
		DontDestroyOnLoad( this.gameObject );
	}

	private void Update()
	{
		// Constant health decrease and death check
		// Important: Health is only depleted by input
		if( gameRunning && movingPlayer )
		{
			// This for float health, UI with slider
			gameManager.HealthDecreaseFloat( Time.deltaTime );
			managerUI.SetHealth( gameManager.health );

			// This is when health is an int, UI with string
			//healthDecreaseCounter -= Time.deltaTime;
			////Debug.Log( healthDecreaseCounter );
			//if( healthDecreaseCounter < 0 )
			//{
			//	gameManager.HealthDecrease();
			//	managerUI.SetHealth( gameManager.health );
			//	healthDecreaseCounter = healthDecreaseTime;
			//}
		}
	}

	private void LateUpdate()
	{
		// Camera type: Snap to camera grabs
		if( managerCamera.cameras[0].type == CameraHelper.Type.SnapToCameraGrabs )
		{
			CheckForNewCameraGrab();
		}
	}
	#endregion


	#region Scene management
	private void ChangeScene( Structs.GameScene to )
	{
		currentScene = to;

		//Debug.Log("Change scene to: " + currentScene);

		switch( currentScene )
		{
			case Structs.GameScene.Initialization:
				SwitchToMenu();
				break;

			case Structs.GameScene.Menu:
				managerInput.SetEvents();
				managerUI.SetPanels();
				SetGameRunning( false );
				break;

			case Structs.GameScene.LoadingGame:
				//InitMap();
				//entityManager.Init();
				//GameInitialize();
				//InitPlayer();
				//InitCamera();
				//SetCameraOnPlayer();
				//GameStart();
				managerUI.SetPanels();
				SetGameRunning( false );
				SwitchToIngame();
				break;

			case Structs.GameScene.Ingame:
				//inputManager.SetEvents();
				//uiManager.UpdateUI();
				// This loads the map, sets the player and the camera
				// Using the number of the level
				LoadLevel();

				if( managerEntity.playersScript[0] != null )
				{
					managerEntity.playersScript[0].OnDie += GameEnd;
				}

				managerInput.SetEvents();
				managerUI.SetPanels();

				SetGameRunning( true );

				break;

			case Structs.GameScene.GameReset:
				SetGameRunning( false );

				managerEntity.playersScript[0].OnDie -= GameEnd;
				managerEntity.Reset();
				managerMap.Reset();
				GameBegin();
				break;

			case Structs.GameScene.GameEnd:
				SetGameRunning( false );

				managerEntity.playersScript[0].OnDie -= GameEnd;
				managerEntity.Reset();
				managerMap.Reset();
				managerInput.SetEvents();
				managerUI.SetPanels();
				SwitchToMenu();
				break;

			case Structs.GameScene.Exit:
				Application.Quit();
				break;
		}

	}
	#endregion


	#region Game settings & management
	public void SetGameSettings( Structs.GameMode gameMode, Structs.GameDifficulty gameDifficulty, Structs.GameView viewMode )
	{
		currentGameMode = gameMode;
		currentGameDifficulty = gameDifficulty;
		currentGameView = viewMode;
	}

	private void LoadLevel()
	{
		// Reset entities and map
		managerEntity.Reset();
		managerMap.Reset();

		// Load the current level
		managerMap.LoadMap( currentLevel );
		var newMap = managerMap.mapScript;
		//Debug.Log( "Loading level number: " + (currentLevel + 1).ToString() );

		// Load player init position from map
		// If there is a player init pos in the map data
		Vector2 playerInitPos;
		if( newMap.players.Count > 0 )
		{
			playerInitPos = managerMap.mapScript.players[0];
		}
		else
		{
			playerInitPos = Vector2.zero;
		}
		managerEntity.SummonPlayer( 0, playerInitPos );

		// Set camera
		Vector2 cameraInitPos = Vector2.zero;

		switch( newMap.cameraType )
		{
			case CameraHelper.Type.FixedPoint:
				if( newMap.cameraGrabs.Count > 0 )
				{
					// If there is camera data, init camera to first cameragrab
					cameraInitPos = managerMap.mapScript.cameraGrabs[0];
				}
				managerCamera.cameras[0].SetFixedPoint( cameraInitPos );
				break;

			case CameraHelper.Type.FixedAxis:
				break;

			case CameraHelper.Type.SnapToCameraGrabs:
				//Debug.Log( "CameraGrabs on new map: " + newMap.cameraGrabs.Count );
				if( newMap.cameraGrabs.Count > 0 )
				{
					managerCamera.cameras[0].SetSnapToCameraGrab();
				}
				else
				{
					// If there are no camera grabs, just fall back to default follow player
					managerCamera.cameras[0].SetFollow( managerEntity.playersScript[0].transform );
				}
				break;

			default: // By default, just follow the first player
			case CameraHelper.Type.Follow:
				managerCamera.cameras[0].SetFollow( managerEntity.playersScript[0].transform );
				break;
		}

		// Set stars
		int starNumber = newMap.stars.Count;
		gameManager.SetStars( starNumber );
		managerUI.SetStars( starNumber );
	}

	private void LoadNumberLevel( int levelNumber )
	{
		currentLevel = levelNumber;
	}

	private void LoadNextLevel()
	{
		currentLevel++;
		if( currentLevel >= maxLevelNumber )
		{
			currentLevel = maxLevelNumber - 1;
		}
	}

	private void LoadPreviousLevel()
	{
		currentLevel--;
		if( currentLevel < maxLevelNumber )
		{
			currentLevel = 0;
		}
	}

	private void SetGameRunning( bool to )
	{
		gameRunning = to;
		healthDecreaseCounter = healthDecreaseTime;
		gameManager.ResetHealth();

		if( to )
		{
			gameManager.OnPlayerDeath += GameEnd;
			//gameManager.OnNoStars += GameEnd;
		}
		else
		{
			if( gameManager.OnPlayerDeath != null )
			{
				gameManager.OnPlayerDeath -= GameEnd;
			}
		}
	}

	#endregion


	#region Game cycle
	// This is the first thing that begins the whole game
	public void EverythingBeginsHere()
	{
		ChangeScene( Structs.GameScene.Initialization );
	}

	// This is automatic
	private void SwitchToMenu()
	{
		ChangeScene( Structs.GameScene.Menu );
	}

	public void GameBegin()
	{
		ChangeScene( Structs.GameScene.LoadingGame );
	}

	// This is automatic
	private void SwitchToIngame()
	{
		ChangeScene( Structs.GameScene.Ingame );
	}

	private void GameReset()
	{
		ChangeScene( Structs.GameScene.GameReset );
	}

	public void GameEnd()
	{
		ChangeScene( Structs.GameScene.GameEnd );
	}

	public void Exit()
	{
		Debug.Log( "Exit game!" );
		ChangeScene( Structs.GameScene.Exit );
	}
	#endregion


	#region DEBUG
	//public void DebugHurtPlayer()
	//{
	//    managerEntity.playersScript[0].Hurt();
	//}

	public void DebugLoadLevel( int numb )
	{
		LoadNumberLevel( numb );
		GameReset();
	}

	public void DebugLevelNext()
	{
		LoadNextLevel();
		GameReset();
	}

	public void DebugLevelPrevious()
	{
		LoadPreviousLevel();
		GameReset();
	}

	private void CheckForNewCameraGrab()
	{
		// I'm calling this from Director.Update

		Vector2 player = managerEntity.playersScript[0].transform.position;
		List<Vector2> cameraGrabs = managerMap.mapScript.cameraGrabs;
		float min = 999;
		Vector2 correctCameraGrab = Vector2.zero;

		// Constantly checking for distance between player and ALL camera grabs?
		// Extremely inefficient
		for( int i = 0; i < cameraGrabs.Count; i++ )
		{
			float checking = Vector2.Distance( player, cameraGrabs[i] );
			if( checking < min )
			{
				min = checking;
				correctCameraGrab = cameraGrabs[i];
			}
		}

		// When the result of the check is different, then call CameraHelper.OnNewCameraGrab(x,y)
		managerCamera.cameras[0].OnNewCameraGrab( correctCameraGrab.x, correctCameraGrab.y );
	}

	public void DebugHealthIncrease( int howMuch )
	{
		// Modify the health
		gameManager.HealthIncrease( howMuch );

		// Show the change in the UI
		managerUI.SetHealth( (int)gameManager.health );

		// Also reset healthDecreaser counter to 1 second
		healthDecreaseCounter = healthDecreaseTime;
	}

	public void DebugHealthDecrease( float howMuch = 0.1f )
	{
		gameManager.HealthDecreaseFloat( howMuch );
		managerUI.SetHealth( gameManager.health );
	}

	public void DebugStarTaken()
	{
		gameManager.StarsDecrease();
		managerUI.SetStars( gameManager.stars );
	}

	public void CheckEndGameCondition()
	{
		if( gameManager.stars <= 0 )
		{
			// Finish the fucking level
			//Debug.Log("FINISHE THE FUCKINGE LEVEL");
			DebugLevelNext();
		}
	}

	public void DebugPlayerMoving( bool setTo )
	{
		movingPlayer = setTo;
	}
	#endregion
}
