using UnityEngine;

public class LobbyUIController : MonoBehaviour
{
    public void Init()
    {
        //
    }

    public void OnClickSettingButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<SettingUI>(uiData);
    }

    public void OnClickPlayButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<ControlSelectionUI>(uiData);
    }

    public void OnClickGuideButton()
    {
        // ¾À º¯°æ
    }

    public void OnClickRankingButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<RankingUI>(uiData);
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
