using UnityEngine;

public class RankingUI : BaseUI
{
    public override void Init(Transform canvas)
    {
        base.Init(canvas);
    }

    public void OnClickSettingButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<SettingUI>(uiData);
    }
}
