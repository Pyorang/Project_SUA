using UnityEngine;

public class LobbyUIController : MonoBehaviour
{
    public void Init()
    {
        //
    }

    public void OnClickOptionButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<OptionUI>(uiData);
    }

    public void OnClickPlayButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<MapSelectionUI>(uiData);
    }

    public void GuideButton()
    {
        // �� ����
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
