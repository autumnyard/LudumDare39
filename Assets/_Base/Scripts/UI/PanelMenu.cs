
public class PanelMenu : PanelBase
{

    public void ButtonPlay()
    {
        Director.Instance.GameBegin();
    }

    public void ButtonExit()
    {
        Director.Instance.Exit();
    }
}
