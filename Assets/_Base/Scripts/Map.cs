using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
	public CameraHelper.Type cameraType = CameraHelper.Type.Unsetted;
	public List<Vector2> players;
	public List<Vector2> cameraGrabs;
	public List<Vector2> stars;

	private void Awake()
	{
		//First, get all the thingies
		MapThingie[] temp = GetComponentsInChildren<MapThingie>();

		// Populate the players
		// Populate the camera grabs
		foreach( MapThingie item in temp )
		{
			switch( item.type )
			{

				case MapThingie.Type.CameraGrab:
					cameraGrabs.Add( item.transform.position );
					//Debug.Log("Adding camera grab: "+ item.transform.position .x);
					break;

				case MapThingie.Type.Player:
					players.Add( item.transform.position );
					break;

				case MapThingie.Type.Star:
					stars.Add( item.transform.position );
					break;

				default:
					break;

			}
		}

		// Populate the obstacles, enemies, items, whatever
	}
}
