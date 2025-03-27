using UnityEngine;
using UnityEngine.UI;

public class ControlSelectionUI : BaseUI
{
    public override void Init(Transform canvas)
    {
        base.Init(canvas);
    }

    public void OnClickControlSelectionButton()
    {
        var loadingOperation = SceneLoader.Instance.LoadSceneAsync(ESceneType.Ingame);
        this.Close();
    }
}