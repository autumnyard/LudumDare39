using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHelper : MonoBehaviour
{

	public enum Type : int
	{
		Unsetted, // If this is true, then set manually to default
		FixedPoint, // This can be done following a non-moving GameObject
		SnapToCameraGrabs, // This has different CameraGrabs, and snaps to the one closer to the player
		FixedAxis,
		Follow
	}

	#region Variables
	// Main settings
	[Header( "Main settings" )/*, SerializeField*/] public Type type; // I make it public for Director.Update
	new private Transform camera;
	public Transform target;// { private set; get; }

	// Fixed point specific variables
	[Header( "Fixed point" )]
	public Vector2 targetPoint = Vector2.zero;

	// Fixed axis specific variables
	[Header( "Fixed axis" )]
	public bool isHorizontal = true; // True = horizontal. False = vertical
	public float fixedValue = 0f; // If the axis is horizontal, this is the height. If the axis is vertical, this is the X value

	// Snap to CameraGrabs specific variables
	[Header( "Snap to CameraGrabs" )]
	public float distanceToSnap = 100f;
	public delegate void Delegate( float x, float y );
	public Delegate OnNewCameraGrab;

	// Follow type specific variables
	[Header( "Follow" )]
	public float maxSpeed = 20f; // Should be used to clamp the speed, NOT YET IMPLEMENTED TODO

	// Cursor locking
	/*
    [SerializeField]
    private bool cursorLocking = true;
    private CursorLocking cursorLock;
    */

	// Movement settings
	/*
    [SerializeField,Header("Fidgeting")]
    private float damping = 5.0f;
    private bool smoothRotation = false;
    private float rotationDamping = 10.0f;
    */

	#endregion

	#region Monobehaviour
	private void Awake()
	{
		camera = transform;

	}

	void FixedUpdate()
	{
		if( camera != null )
		{
			if( target != null )
			{
				switch( type )
				{
					case Type.FixedPoint:
						// Doesn't need to be updated, damn it that's the point
						//CameraFixedPoint();
						break;

					case Type.FixedAxis:
						CameraFixedAxis();
						break;

					case Type.Follow:
						CameraFollow();
						break;

				}
			}
		}
		else
		{
			//Debug.LogError( "There's no Camera component" + name );
		}
	}
	#endregion


	#region Options
	private void CameraFixedPoint()
	{
		Vector3 newPos = Vector3.zero;
		if( targetPoint != null )
		{
			newPos = new Vector3( targetPoint.x, targetPoint.y, -10 );
			camera.localPosition = newPos;
		}
		else
		{
			//Debug.LogError( "There's no target in camera " + name );
		}
	}

	private void CameraFixedAxis()
	{
		Vector3 newPos = Vector3.zero;
		if( target != null )
		{
			float newPosX = (isHorizontal) ? target.localPosition.x : fixedValue;
			float newPosY = (isHorizontal) ? fixedValue : target.localPosition.y;
			newPos = new Vector3( newPosX, newPosY, -10 );
			camera.localPosition = newPos;
		}
		else
		{
			//Debug.LogError( "There's no target in camera " + name );
		}
	}

	private void CameraSnapToCameraGrabs( float x = 0f, float y = 0f )
	{
		Vector3 newPos = Vector3.zero;
		newPos = new Vector3( x, y, -10 );
		camera.localPosition = newPos;
	}

	private void CameraFollow()
	{
		Vector3 newPos = Vector3.zero;
		if( target != null )
		{
			newPos = new Vector3( target.localPosition.x, target.localPosition.y, -10 );
			camera.localPosition = newPos;
		}
		else
		{
			//Debug.LogError( "There's no target in camera " + name );
		}
	}
	#endregion


	#region Public
	public void SetFixedPoint( Vector2 targetPointP = default( Vector2 ) )
	{
		type = Type.FixedPoint;
		targetPoint = targetPointP;

		// Just set it once
		CameraFixedPoint();
	}

	public void SetFollow( Transform targetP = null )
	{
		type = Type.Follow;
		target = targetP;
		//maxSpeed = maxSpeedP;
	}

	public void SetSnapToCameraGrab()
	{
		type = Type.SnapToCameraGrabs;
		//targetPoint = targetPointP;

		// This should work with the delegate
		OnNewCameraGrab += CameraSnapToCameraGrabs; 
		// DEBUG: This callback will be called from the.. Director?
		// 
		// TODO: I must unlink this somewhere, sometime
		// TODO: Most likely with a general reset each time we reset the CameraManager

		// Just set it once
		CameraFixedPoint();
	}

	public void SetFixedAxis( Transform targetP = null, bool isHorizontalP = false, float fixedValueP = 0f )
	{
		type = Type.FixedAxis;
		target = targetP;
		isHorizontal = isHorizontalP;
		fixedValue = fixedValueP;
	}
	#endregion
}
