using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerMap : MonoBehaviour
{

	[SerializeField] private GameObject[] maps;
	private GameObject map;
	public Map mapScript;

	void Awake()
	{
		Director.Instance.managerMap = this;
	}


	#region Private methods

	private void SummonMap( int which )
	{
		map = Instantiate( maps[which], this.transform ) as GameObject;
		mapScript = map.GetComponent<Map>();
	}

	private void RemoveCurrentMap()
	{
		if( map != null )
		{
			Destroy( map );
			mapScript = null;
			map = null;
		}
	}

	#endregion


	#region Public methods

	public void LoadMap( int which = 0 )
	{
		//RemoveCurrentMap();
		SummonMap( which );
	}

	public void Reset()
	{
		RemoveCurrentMap();
	}

	#endregion
}
