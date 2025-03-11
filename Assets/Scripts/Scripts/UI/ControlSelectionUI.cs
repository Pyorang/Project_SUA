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
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<MapSelectionUI>(uiData);
    }
}