using UnityEditor;
using UnityEngine;


public class CustomHotkeys : MonoBehaviour
{
	[MenuItem( "Pablo/Toggle Selected GameObject %g" )]
	static void ToggleGameObject()
	{
		//var selected = Selection.transforms;
		if( Selection.transforms != null )
		{
			foreach( Transform t in Selection.transforms )
				t.gameObject.SetActive( !t.gameObject.activeInHierarchy );
		}
	}


	[MenuItem( "Pablo/DEBUG Copy size from BoxCollider2D to Sliced SpriteRenderer %k" )]
	static void DEBUGCopySizeFromColliderToSprite()
	{
		if( Selection.transforms != null )
		{
			var collider = Selection.transforms[0].GetComponent<BoxCollider2D>();
			var renderer = Selection.transforms[0].GetComponent<SpriteRenderer>();
			if( collider != null && renderer != null )
			{
				renderer.drawMode = SpriteDrawMode.Sliced;
				renderer.size = collider.size;
			}
		}
	}

	[MenuItem( "Pablo/DEBUG Copy size from Sliced SpriteRenderer to BoxCollider2D %l" )]
	static void DEBUGCopySizeFromSpriteToCollider()
	{
		if( Selection.transforms != null )
		{
			var collider = Selection.transforms[0].GetComponent<BoxCollider2D>();
			var renderer = Selection.transforms[0].GetComponent<SpriteRenderer>();
			if( collider != null && renderer != null && renderer.drawMode == SpriteDrawMode.Sliced )
			{
				collider.size = renderer.size;
			}
		}
	}

	[MenuItem( "Pablo/Clear Console %t" )]
	static void ClearConsole()
	{
		// This simply does "LogEntries.Clear()" the long way:
		var logEntries = System.Type.GetType( "UnityEditorInternal.LogEntries,UnityEditor.dll" );
		var clearMethod = logEntries.GetMethod( "Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public );
		clearMethod.Invoke( null, null );
	}

	[MenuItem( "Pablo/Apply Prefab Changes %h" )]
	static void ApplyPrefabChanges()
	{
		var obj = Selection.activeGameObject;
		if( obj != null )
		{
			var prefab_root = PrefabUtility.FindPrefabRoot( obj );
			var prefab_src = PrefabUtility.GetPrefabParent( prefab_root );
			if( prefab_src != null )
			{
				PrefabUtility.ReplacePrefab( prefab_root, prefab_src, ReplacePrefabOptions.ConnectToPrefab );
				Debug.Log( "Updating prefab : " + AssetDatabase.GetAssetPath( prefab_src ) );
			}
			else
			{
				Debug.Log( "Selected has no prefab" );
			}
		}
		else
		{
			Debug.Log( "Nothing selected" );
		}
	}

	/*
	[MenuItem( "GameObject/Thingie with prefab %a" )]
	static void PrefabThingie()
	{
		//var selected = Selection.transforms;
		if( Selection.transforms != null )
		{
			foreach( Transform t in Selection.transforms )
				//t.gameObject.SetActive( !t.gameObject.activeInHierarchy );
				UnityEditor.PrefabUtility.
		}
	}
	*/
}
