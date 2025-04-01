using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LobbyUIController : MonoBehaviour
{
    #region InputSystems
    KeyBoardInputActions action;
    InputAction escapeKeyAction;
    #endregion

    public void Awake()
    {
        AllocateEscapeActions();
    }

    private void OnEnable()
    {
        escapeKeyAction.Enable();
        escapeKeyAction.performed += GetSettingButtonControl;
    }

    private void OnDisable()
    {
        escapeKeyAction.Disable();
        escapeKeyAction.performed -= GetSettingButtonControl;
    }

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

    private void HandleInput()
    {
        //AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");

        var frontUI = UIManager.Instance.GetFrontUI();
        if (frontUI != null)
        {
            frontUI.Close();
            Time.timeScale = 1f;
        }
        else
        {
            OnClickSettingButton();
        }
    }

    public void GetSettingButtonControl(InputAction.CallbackContext context)
    {
        var control = context.control;

        switch (control.name)
        {
            case "escape":
                HandleInput();
                break;
            case "button10":
                HandleInput();
                break;
            default:
                Debug.Log("Unknown input : " + control.name);
                break;
        }
    }

    private void AllocateEscapeActions()
    {
        action = new KeyBoardInputActions();
        escapeKeyAction = action.Car.Settings;
    }
}
