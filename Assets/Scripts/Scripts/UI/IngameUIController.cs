using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class IngameUIController : MonoBehaviour
{
    private int currentTime_centiSeconds = 0;
    private int currentTime_Second = 0;
    private int currentTime_Minute = 0;
    private float currentTime = 0;

    float speed;

    [Header("Car Rigidbody")]

    [Space]

    [SerializeField] private Rigidbody carRigidbody; 

    [Header("Texts")]

    [Space]

    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI timerText;

    #region InputSystems
    KeyBoardInputActions action;
    InputAction escapeKeyBoard;
    InputAction escapeSteeringWheel;
    #endregion


    public void Awake()
    {
        AllocateEscapeActions();
    }

    private void OnEnable()
    {
        EnableAllActions(UserDataManager.Instance.GetUserData<UserSettingData>().IsKeyBoard);
    }

    private void OnDisable()
    {
        DisableAllActions(UserDataManager.Instance.GetUserData<UserSettingData>().IsKeyBoard);
    }

    void EnableAllActions(bool isKeyBoard)
    {
        if (isKeyBoard)
        {
            escapeKeyBoard.Enable();
            escapeKeyBoard.performed += GetSettingButtonControl;
        }
        else
        {
            escapeSteeringWheel.Enable();
            escapeSteeringWheel.performed += GetSettingButtonControl;
        }
    }

    void DisableAllActions(bool isKeyBoard)
    {
        if (isKeyBoard)
        {
            escapeKeyBoard.Disable();
            escapeKeyBoard.performed -= GetSettingButtonControl;
        }
        else
        {
            escapeSteeringWheel.Disable();
            escapeSteeringWheel.performed -= GetSettingButtonControl;
        }
    }

    public void Init()
    {
        //
    }

    public void Update()
    {
        UpdateTimerTextUI();
    }

    public void FixedUpdate()
    {
        speed = carRigidbody.linearVelocity.magnitude;
        speedText.text = ((int)speed * 3.6f).ToString("000");
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
           OnClickInGameSettingButton();
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

    public void UpdateTimerTextUI()
    {
        currentTime += Time.deltaTime;
        currentTime_centiSeconds = (int)((currentTime * 100) % 100);
        currentTime_Second = (int)currentTime % 60;
        currentTime_Minute = ((int)currentTime / 60) % 100;

        if (currentTime_Minute > 99)
        {
            currentTime_Minute = 99;
            currentTime_Second = 59;
            currentTime_centiSeconds = 99;
        }

        timerText.text = $"{currentTime_Minute:D2}:{currentTime_Second:D2}:{currentTime_centiSeconds:D2}";
    }

    public void OnClickInGameSettingButton()
    {
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<InGameSettingUI>(uiData);
    }

    private void AllocateEscapeActions()
    {
        action = new KeyBoardInputActions();
        escapeKeyBoard = action.Car.SettingsKeyBoard;
        escapeSteeringWheel = action.Car.SettingsSteeringWheel;
    }
}
