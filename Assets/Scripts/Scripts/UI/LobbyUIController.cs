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
        // �� ����
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
