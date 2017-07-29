using UnityEditor;
using UnityEngine;

public class CustomUtils : EditorWindow
{

	[MenuItem("Pablo/Reset Playerprefs")]

	public static void DeletePlayerPrefs()
	{
		PlayerPrefs.DeleteAll();
	}

}