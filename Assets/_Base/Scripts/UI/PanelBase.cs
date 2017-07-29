using UnityEngine;


public class PanelBase : MonoBehaviour
{
    private CanvasRenderer canvas;

    private void Awake()
    {
        canvas = GetComponent<CanvasRenderer>();

        if (canvas == null)
        {
            Debug.LogWarning("UI menu without Unity Canvas: " + name);
        }
    }

    public void Show()
    {
        //Debug.Log("Show UI menu: " + name);
        gameObject.SetActive(true);
        //canvas.SetAlpha( 1f );
    }

    public void Hide()
    {
        //canvas.SetAlpha( 0f );
        gameObject.SetActive(false);
        //Debug.Log("Hide UI menu: " + name);
    }
}
