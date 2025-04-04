using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public enum EscapeButton
{
    Restart = 1,
    Lobby = 2
}

public class InGameSettingUI : BaseUI
{
    private int currentIndex = 1;
    [SerializeField] private GameObject[] arrowImages;

    #region InputSystems
    KeyBoardInputActions action;
    InputAction enterKeyBoard;
    InputAction enterSteeringWheel;
    InputAction leftRightKeyBoard;
    InputAction leftRightSteeringWheel;
    InputAction undoKeyBoard;
    InputAction undoSteeringWheel;
    #endregion

    private void Awake()
    {
        AllocateEscapeActions();
    }

    public override void Init(Transform Canvas)
    {
        base.Init(Canvas);
        Time.timeScale = 0f;
    }

    public override void Close(bool isCloseAll = false)
    {
        base.Close(isCloseAll);
        Time.timeScale = 1f;
    }

    public void OnEnable()
    {
        arrowImages[currentIndex - 1].SetActive(false);
        currentIndex = 1;
        EnableAllActions(UserDataManager.Instance.GetUserData<UserSettingData>().IsKeyBoard);
        arrowImages[currentIndex - 1].SetActive(true);
    }

    public void OnDisable()
    {
        DisableAllActions(UserDataManager.Instance.GetUserData<UserSettingData>().IsKeyBoard);
    }

    void EnableAllActions(bool isKeyBoard)
    {
        if (isKeyBoard)
        {
            enterKeyBoard.Enable();
            enterKeyBoard.performed += GetSettingButtonControl;
            leftRightKeyBoard.Enable();
            leftRightKeyBoard.performed += GetSettingButtonControl;
            undoKeyBoard.Enable();
            undoKeyBoard.performed += GetSettingButtonControl;
        }
        else
        {
            enterSteeringWheel.Enable();
            enterSteeringWheel.performed += GetSettingButtonControl;
            leftRightSteeringWheel.Enable();
            leftRightSteeringWheel.performed += GetSettingButtonControl;
            undoSteeringWheel.Enable();
            undoSteeringWheel.performed += GetSettingButtonControl;

        }
    }

    void DisableAllActions(bool isKeyBoard)
    {
        if (isKeyBoard)
        {
            enterKeyBoard.Disable();
            enterKeyBoard.performed -= GetSettingButtonControl;
            leftRightKeyBoard.Disable();
            leftRightKeyBoard.performed -= GetSettingButtonControl;
            undoKeyBoard.Disable();
            undoKeyBoard.performed -= GetSettingButtonControl;
        }
        else
        {
            enterSteeringWheel.Disable();
            enterSteeringWheel.performed -= GetSettingButtonControl;
            leftRightSteeringWheel.Disable();
            leftRightSteeringWheel.performed -= GetSettingButtonControl;
            undoSteeringWheel.Disable();
            undoSteeringWheel.performed -= GetSettingButtonControl;
        }
    }

    public void GetSettingButtonControl(InputAction.CallbackContext context)
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
                HandleInGameSettingButton();
                break;
            case "button3":
                HandleInGameSettingButton();
                break;
            case "b":
                Close(false);
                break;
            case "button2":
                Close(false);
                break;
            default:
                Debug.Log("Unknown input : " + control.name);
                break;
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
        SceneLoader.Instance.LoadSceneAsync(ESceneType.Lobby);
    }

    public void OnClickLeftMenuButton()
    {
        arrowImages[currentIndex - 1].SetActive(false);
        currentIndex--;
        if (currentIndex < (int)EscapeButton.Restart) currentIndex = (int)EscapeButton.Lobby;
        arrowImages[currentIndex - 1].SetActive(true);
    }

    public void OnClickRightMenuButton()
    {
        arrowImages[currentIndex - 1].SetActive(false);
        currentIndex++;
        if (currentIndex > (int)EscapeButton.Lobby) currentIndex = (int)EscapeButton.Restart;
        arrowImages[currentIndex - 1].SetActive(true);
    }

    public void HandleInGameSettingButton()
    {
        if (currentIndex == (int)EscapeButton.Lobby)
            OnClickGoToLobbyButton();
        else if (currentIndex == (int)EscapeButton.Restart)
            OnClickRestartButton();
    }

    private void AllocateEscapeActions()
    {
        action = new KeyBoardInputActions();
        enterKeyBoard = action.Car.SelectKeyBoard;
        enterSteeringWheel = action.Car.SelectSteeringWheel;
        leftRightKeyBoard = action.Car.LeftRightKeyBoard;
        leftRightSteeringWheel = action.Car.LeftRightSteeringWheel;
        undoKeyBoard = action.Car.UndoKeyBoard;
        undoSteeringWheel = action.Car.UndoSteeringWheel;
    }
}
