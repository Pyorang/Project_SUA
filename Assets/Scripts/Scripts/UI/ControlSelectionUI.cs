using UnityEngine;
using UnityEngine.UI;

public class ControlSelectionUI : BaseUI
{
    public override void Init(Transform canvas)
    {
        base.Init(canvas);
    }

    public void OnClickKeyBoardMouseButton()
    {
        var loadingOperation = SceneLoader.Instance.LoadSceneAsync(ESceneType.Ingame);
        this.Close();
    }

    public void OnClickSteeringWheelButton()
    {
        var loadingOperation = SceneLoader.Instance.LoadSceneAsync(ESceneType.Ingame);
        this.Close();
    }
}