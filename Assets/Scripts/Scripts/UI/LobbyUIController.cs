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
    InputAction selectMenuKeyBoard;
    InputAction selectMenuSteeringWheel;
    InputAction enterMenuKeyBoard;
    InputAction enterMenuSteeringWheel;
    #endregion

    public void Awake()
    {
        AllocateEscapeActions();
    }

    private void OnEnable()
    {
        selectMenuKeyBoard.Enable();
        selectMenuKeyBoard.performed += GetMenuButtonControl;
        selectMenuSteeringWheel.Enable();
        selectMenuSteeringWheel.performed += GetMenuButtonControl;
        enterMenuKeyBoard.Enable() ;
        enterMenuKeyBoard.performed += GetMenuButtonControl;
        enterMenuSteeringWheel.Enable();
        enterMenuSteeringWheel.performed += GetMenuButtonControl;
    }

    private void OnDisable()
    {
        selectMenuKeyBoard.Disable();
        selectMenuKeyBoard.performed -= GetMenuButtonControl;
        selectMenuSteeringWheel.Disable();
        selectMenuSteeringWheel.performed -= GetMenuButtonControl;
        enterMenuKeyBoard.Disable();
        enterMenuKeyBoard.performed -= GetMenuButtonControl;
        enterMenuSteeringWheel.Disable();
        enterMenuSteeringWheel.performed -= GetMenuButtonControl;
    }

    public void Init()
    {
        //
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

    public void GetMenuButtonControl(InputAction.CallbackContext context)
    {
        if(!UIManager.Instance.ExistAnyOpenUI())
        {
            var control = context.control;

            switch (control.name)
            {
                case "leftArrow":
                    OnClickLeftMenuButton();
                    break;
                case "left":
                    OnClickLeftMenuButton();
                    break;
                case "rightArrow":
                    OnClickRightMenuButton();
                    break;
                case "right":
                    OnClickRightMenuButton();
                    break;
                case "enter":
                    HandleMenuButton();
                    break;
                case "button3":
                    HandleMenuButton();
                    break;
                default:
                    Debug.Log("Unknown input : " + control.name);
                    break;
            }
        }
    }

    private void AllocateEscapeActions()
    {
        action = new KeyBoardInputActions();
        selectMenuKeyBoard = action.Car.LeftRightKeyBoard;
        selectMenuSteeringWheel = action.Car.LeftRightSteeringWheel;
        enterMenuKeyBoard = action.Car.SelectKeyBoard;
        enterMenuSteeringWheel = action.Car.SelectSteeringWheel;
    }
}
