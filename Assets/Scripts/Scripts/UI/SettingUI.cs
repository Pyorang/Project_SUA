using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingUI : BaseUI
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text SFXButtonText;

    private bool isSFXOn = true;

    public override void Init(Transform Canvas)
    {
        base.Init(Canvas);
        slider.value = AudioManager.Instance.GetAllVolume();
    }

    public void OnSoundSliderChanged()
    {
        AudioManager.Instance.SetAllVolume(slider.value);
    }

    /*
    public void OnClickBGMOnOffBtn()
    {
        AudioManager.Instance.ChangeBGMState();
    }
    */

    public void OnClickSFXOnOffBtn()
    {
        AudioManager.Instance.ChangeSFXState();
        isSFXOn = !isSFXOn;

        if (isSFXOn)
        {
            SFXButtonText.text = "ON";
        }
        else
        {
            SFXButtonText.text = "OFF";
        }
    }
}
