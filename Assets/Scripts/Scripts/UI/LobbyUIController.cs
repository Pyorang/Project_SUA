using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public enum MenuButton
{
    Play = 1,
    Guide = 2,
    Ranking = 3,
    Exit = 4
}

public class LobbyUIController : MonoBehaviour
{
    private int currentIndex = 1;
    [SerializeField] TextMeshProUGUI MenuButtonText;

    #region InputSystems
    KeyBoardInputActions action;
    InputAction selectMenu;
    InputAction escapeKeyAction;
    #endregion

    public void Awake()
    {
        AllocateEscapeActions();
    }

    private void OnEnable()
    {
        selectMenu.Enable();
        selectMenu.performed += GetMenuButtonControl;
        escapeKeyAction.Enable();
        escapeKeyAction.performed += GetSettingButtonControl;
    }

    private void OnDisable()
    {
        selectMenu.Enable();
        selectMenu.performed -= GetMenuButtonControl;
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

    private void OnClickPlayButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<ControlSelectionUI>(uiData);
    }

    private void OnClickGuideButton()
    {
        // ¾À º¯°æ
    }

    private void OnClickRankingButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<RankingUI>(uiData);
    }

    private void OnClickExitButton()
    {
        Application.Quit();
    }

    public void OnClickLeftMenuButton()
    {
        currentIndex--;
        if (currentIndex < (int)MenuButton.Play) currentIndex = (int)MenuButton.Exit;
        MenuButtonText.text = ((MenuButton)currentIndex).ToString();
    }

    public void OnClickRightMenuButton()
    {
        currentIndex++;
        if (currentIndex > (int)MenuButton.Exit) currentIndex = (int)MenuButton.Play;
        MenuButtonText.text = ((MenuButton)currentIndex).ToString();
    }

    public void HandleMenuButton()
    {
        switch (currentIndex)
        {
            case (int)MenuButton.Play:
                OnClickPlayButton();
                break;
            case (int)MenuButton.Guide:
                OnClickGuideButton();
                break;
            case (int)MenuButton.Ranking:
                OnClickRankingButton();
                break;
            case (int)MenuButton.Exit:
                break;
        }
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
    public void GetMenuButtonControl(InputAction.CallbackContext context)
    {
        var control = context.control;

        switch (control.name)
        {
            case "leftArrow":
                OnClickLeftMenuButton();
                break;
            case "hat/left":
                OnClickLeftMenuButton();
                break;
            case "rightArrow":
                OnClickRightMenuButton();
                break;
            case "hat/right":
                OnClickRightMenuButton();
                break;
            default:
                Debug.Log("Unknown input : " + control.name);
                break;
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
        selectMenu = action.Car.LookLeftRight;
        escapeKeyAction = action.Car.Settings;
    }
}
