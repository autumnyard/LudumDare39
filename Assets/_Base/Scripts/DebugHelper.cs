using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHelper : MonoBehaviour
{

	ManagerCamera managerCamera;
	ManagerEntity managerEntity;
	ManagerMap managerMap;


	void Start()
	{
		managerCamera = Director.Instance.managerCamera;
		managerEntity = Director.Instance.managerEntity;
		managerMap = Director.Instance.managerMap;
	}


	#region Lets just make this easy
	public void SetCameraToFollow()
	{
		Director.Instance.managerCamera.cameras[0].SetFollow( managerEntity.playersScript[0].transform );
	}

	public void SetCameraToFixedAxisHorizontal()
	{
		Director.Instance.managerCamera.cameras[0].SetFixedAxis( managerEntity.playersScript[0].transform, true );
	}

	public void SetCameraToFixedAxisVertical()
	{
		Director.Instance.managerCamera.cameras[0].SetFixedAxis( managerEntity.playersScript[0].transform, false );
	}

	public void SetCameraToFixedPoint()
	{
		Director.Instance.managerCamera.cameras[0].SetFixedPoint( managerMap.mapScript.cameraGrabs[0] );
	}

	public void SetCameraToSnapToCameraGrabs()
	{
		Director.Instance.managerCamera.cameras[0].SetSnapToCameraGrab();
	}
	#endregion

	#region Lets make this rock
	public void SwitchToLevel( int number )
	{
		//managerMap.LoadMap( 0 );
		Director.Instance.DebugLoadLevel( number );
	}

	public void SwitchToLevel1()
	{
		//managerMap.LoadMap( 0 );
		Director.Instance.DebugLoadLevel( 0 );
	}

	public void SwitchToLevel2()
	{
		//managerMap.LoadMap( 1 );
		Director.Instance.DebugLoadLevel( 1 );
	}

	public void SwitchToLevelNext()
	{
		Director.Instance.DebugLevelNext();
	}

	public void SwitchToLevelPrevious()
	{
		Director.Instance.DebugLevelPrevious();
	}
	#endregion
}
