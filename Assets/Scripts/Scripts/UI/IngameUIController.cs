using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class IngameUIController : MonoBehaviour
{
    private int currentTime_centiSeconds = 0;
    private int currentTime_Second = 0;
    private int currentTime_Minute = 0;
    private float currentTime = 0;
    private float displayedSpeed = 5f;
    private bool isAlerting = false;

    float speed;

    [Header("Car Rigidbody")]
    [Space]

    [SerializeField] private Rigidbody carRigidbody; 

    [Header("Texts")]
    [Space]

    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Navigation")]
    [Space]

    [SerializeField] private Image navigationImage;
    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private Sprite[] alertImages;

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
        float targetSpeed = carRigidbody.linearVelocity.magnitude;
        displayedSpeed = Mathf.Lerp(displayedSpeed, targetSpeed, Time.deltaTime * 5f);

        speedText.text = ((int)(displayedSpeed * 3.6f)).ToString("000");
    }

    private void HandleInput()
    {
        AudioManager.Instance.Play(AudioType.SFX, "ButtonClick");

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

    public void SetAlertSignsOn()
    {
        navigationImage.sprite = alertImages[NavigationAlertManager.Instance.currentAlertType];
        navigationImage.gameObject.SetActive(true);
        alertText.gameObject.SetActive(true);

        if(!isAlerting)
        {
            isAlerting = true;
            StartCoroutine(Alert());
        }
    }

    IEnumerator Alert()
    {
        int blinkCount = 3;
        float duration = 2f;

        for (int i = 0; i < blinkCount; i++)
        {
            float time = 0f;
            while (time < duration)
            {
                float alpha = Mathf.Lerp(1f, 0f, time / duration);

                if (navigationImage != null)
                {
                    Color navColor = navigationImage.color;
                    navColor.a = alpha;
                    navigationImage.color = navColor;
                }

                if (alertText != null)
                {
                    Color textColor = alertText.color;
                    textColor.a = alpha;
                    alertText.color = textColor;
                }

                time += Time.deltaTime;
                yield return null;
            }

            if (navigationImage != null)
            {
                Color navColor = navigationImage.color;
                navColor.a = 1f;
                navigationImage.color = navColor;
            }

            if (alertText != null)
            {
                Color textColor = alertText.color;
                textColor.a = 1f;
                alertText.color = textColor;
            }
        }

        SetAlertSignsOff();
    }

    public void SetAlertSignsOff()
    {
        Color tempColor = navigationImage.color;
        Color tempColor2 = alertText.color;
        tempColor.a = 1f;
        tempColor2.a = 1f;

        navigationImage.color = tempColor;
        alertText.color = tempColor2;

        navigationImage.gameObject.SetActive(false);
        alertText.gameObject.SetActive(false);

        isAlerting = false;
    }

    public void ChangeNavigationImageState()
    {
        Debug.Log(NavigationAlertManager.Instance.currentAlertType);
        navigationImage.sprite = alertImages[NavigationAlertManager.Instance.currentAlertType];
        navigationImage.gameObject.SetActive(!navigationImage.gameObject.activeSelf);
        
        Color tempColor = navigationImage.color;
        tempColor.a = 1f;
        navigationImage.color = tempColor;
    }
    
    public void ChangeNavigtionTextState()
    {
        alertText.gameObject.SetActive(!alertText.gameObject.activeSelf);

        Color tempColor = alertText.color;
        tempColor.a = 1f;
        alertText.color = tempColor;
    }
}
