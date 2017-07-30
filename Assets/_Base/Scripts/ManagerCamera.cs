using UnityEngine;


public class ManagerCamera : MonoBehaviour
{
    #region Variables
    public CameraHelper[] cameras;



	// LDJAM39
	//[SerializeField] private CameraEffects cameraEffects;
	public CameraEffects cameraEffects;
	#endregion


	#region Monobehaviour

	void Awake()
    {
        Director.Instance.managerCamera = this;
    }

    private void Start()
    {
        // This makes a pointer to the array in Camera
        //Camera.GetAllCameras( cameras ) ;

        // This copies the array
        cameras = new CameraHelper[Camera.allCamerasCount];
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i] = Camera.allCameras[i].GetComponent<CameraHelper>();
        }
#if DEBUG
        //Debug.Log( "ManagerCamera: We have " + cameras.Length + " cameras, of which the main camera is: " + Camera.main.name );
#endif
    }

    #endregion

	// TODO: Holy shit man, everything 
	
}
