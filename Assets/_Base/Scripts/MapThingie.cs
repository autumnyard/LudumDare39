
using UnityEngine;

public class MapThingie : MonoBehaviour
{

	public enum Type
	{
		Unsetted,
		Player,
		CameraGrab,
		Star,
		MaxValues
	}
	public Type type = Type.Unsetted;

	public int id = -1; // Still not being used, but I want that to order them
}
