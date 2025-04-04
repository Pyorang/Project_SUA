using UnityEngine;
using UnityEngine.InputSystem;

public enum ChooseInput
{
    KeyBoard = 1,
    SteeringWheel = 2
}
public class ControlSelectionUI : BaseUI
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

    public void OnEnable()
    {
        arrowImages[currentIndex - 1].SetActive(false);
        currentIndex = 1;
        arrowImages[currentIndex - 1].SetActive(true);
        EnableAllActions();
    }

    public void OnDisable()
    {
        DisableAllActions();
    }

    void EnableAllActions()
    {
        enterKeyBoard.Enable();
        enterKeyBoard.performed += GetSettingButtonControl;
        leftRightKeyBoard.Enable();
        leftRightKeyBoard.performed += GetSettingButtonControl;
        undoKeyBoard.Enable();
        undoKeyBoard.performed += GetSettingButtonControl;
        enterSteeringWheel.Enable();
        enterSteeringWheel.performed += GetSettingButtonControl;
        leftRightSteeringWheel.Enable();
        leftRightSteeringWheel.performed += GetSettingButtonControl;
        undoSteeringWheel.Enable();
        undoSteeringWheel.performed += GetSettingButtonControl;
    }

    void DisableAllActions()
    {
        enterKeyBoard.Disable();
        enterKeyBoard.performed -= GetSettingButtonControl;
        leftRightKeyBoard.Disable();
        leftRightKeyBoard.performed -= GetSettingButtonControl;
        undoKeyBoard.Disable();
        undoKeyBoard.performed -= GetSettingButtonControl;
        enterSteeringWheel.Disable();
        enterSteeringWheel.performed -= GetSettingButtonControl;
        leftRightSteeringWheel.Disable();
        leftRightSteeringWheel.performed -= GetSettingButtonControl;
        undoSteeringWheel.Disable();
        undoSteeringWheel.performed -= GetSettingButtonControl;
    }

    public override void Init(Transform canvas)
    {
        base.Init(canvas);
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

    public void OnClickLeftMenuButton()
    {
        arrowImages[currentIndex - 1].SetActive(false);
        currentIndex--;
        if (currentIndex < (int)ChooseInput.KeyBoard) currentIndex = (int)ChooseInput.SteeringWheel;
        arrowImages[currentIndex - 1].SetActive(true);
    }

    public void OnClickRightMenuButton()
    {
        arrowImages[currentIndex - 1].SetActive(false);
        currentIndex++;
        if (currentIndex > (int)ChooseInput.SteeringWheel) currentIndex = (int)ChooseInput.KeyBoard;
        arrowImages[currentIndex - 1].SetActive(true);
    }

    public void HandleInGameSettingButton()
    {
        if (currentIndex == (int)ChooseInput.KeyBoard)
            OnClickKeyBoardMouseButton();
        else if (currentIndex == (int)ChooseInput.SteeringWheel)
            OnClickSteeringWheelButton();
    }

    public void OnClickKeyBoardMouseButton()
    {
        UserDataManager.Instance.GetUserData<UserSettingData>().IsKeyBoard = true;
        var loadingOperation = SceneLoader.Instance.LoadSceneAsync(ESceneType.Ingame);
        this.Close();
    }

    public void OnClickSteeringWheelButton()
    {
        UserDataManager.Instance.GetUserData<UserSettingData>().IsKeyBoard = false;
        var loadingOperation = SceneLoader.Instance.LoadSceneAsync(ESceneType.Ingame);
        this.Close();
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