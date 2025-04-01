using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameSettingUI : BaseUI
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text SFXButtonText;

    private bool isSFXOn = true;

    public override void Init(Transform Canvas)
    {
        base.Init(Canvas);
        Time.timeScale = 0f;
        slider.value = AudioManager.Instance.GetAllVolume();
    }

    public override void Close(bool isCloseAll = false)
    {
        base.Close(isCloseAll);
        Time.timeScale = 1f;
    }

    public void OnSoundSliderChanged()
    {
        AudioManager.Instance.SetAllVolume(slider.value);
    }

    public void OnClickSFXOnOffBtn()
    {
        AudioManager.Instance.ChangeSFXState();
        isSFXOn = !isSFXOn;

        if (isSFXOn)
        {
            SFXButtonText.text = "SFX ON";
        }
        else
        {
            SFXButtonText.text = "SFX OFF";
        }
    }
    public void OnClickRestartButton()
    {
        Close(false);
        SceneLoader.Instance.LoadScene(ESceneType.Ingame);
    }

    public void OnClickGoToLobbyButton()
    {
        Close(false);
        SceneLoader.Instance.LoadScene(ESceneType.Lobby);
    }
}
