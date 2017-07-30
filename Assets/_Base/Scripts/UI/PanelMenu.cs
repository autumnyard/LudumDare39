using UnityEngine;

public class PanelMenu : PanelBase
{

	public void ButtonPlay()
	{
		Director.Instance.GameBegin();
	}

	public void ButtonReset()
	{
		Director.Instance.DebugResetState();
	}

	public void ButtonExit()
	{
		Director.Instance.Exit();
	}
}
