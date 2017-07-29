using UnityEngine;

public class CursorLocking : MonoBehaviour
{
	public static bool isLocked;

	private void Update()
	{
		isLocked = Cursor.lockState == CursorLockMode.Locked;

        // Behaviour during gameplay
		if( Director.Instance.currentScene == Structs.GameScene.Ingame )
		{
            // Lock cursor after clicking on the game
			if( Input.GetMouseButtonDown( 0 ) )
			{
				Lock();
			}

            // Unlock cursor after pressing escape
			if( Input.GetKeyDown( KeyCode.Escape ) )
			{
				Unlock();
			}
		}
        // Automatically lock cursor when initializing game
		else if( Director.Instance.currentScene == Structs.GameScene.Initialization )
		{
			Lock();
		}
        // Automatically unlock cursor anytime else
        else
        {
			Unlock();
		}
	}

	public void Lock()
	{
		//Debug.Log( "Lock cursor" );
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void Unlock()
	{
		//Debug.Log( "Unlock cursor" );
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
