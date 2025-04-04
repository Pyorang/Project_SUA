using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class RankingUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI[] rankingScore;

    #region InputSystems
    KeyBoardInputActions action;
    InputAction undoKeyBoard;
    InputAction undoSteeringWheel;
    #endregion

    private void Awake()
    {
        AllocateEscapeActions();
    }

    public void OnEnable()
    {
        EnableAllActions();
    }

    public void OnDisable()
    {
        DisableAllActions();
    }

    void EnableAllActions()
    {
        undoKeyBoard.Enable();
        undoKeyBoard.performed += GetSettingButtonControl;
        undoSteeringWheel.Enable();
        undoSteeringWheel.performed += GetSettingButtonControl;
    }

    void DisableAllActions()
    {
        undoKeyBoard.Disable();
        undoKeyBoard.performed -= GetSettingButtonControl;
        undoSteeringWheel.Disable();
        undoSteeringWheel.performed -= GetSettingButtonControl;
    }

    public override void Init(Transform canvas)
    {
        base.Init(canvas);
        UpdateRankingText();
    }

    public void UpdateRankingText()
    {
        int survivedTime, survivedTime_Second, survivedTime_Minute, survivedTime_centiSecond;

        for (int i = 0; i < rankingScore.Length; i++)
        {
            survivedTime = UserDataManager.Instance.GetUserData<UserRankingData>().GetRankScore(i);

            survivedTime_centiSecond = (int)((survivedTime * 100) % 100);
            survivedTime_Second = (int)survivedTime % 60;
            survivedTime_Minute = ((int)survivedTime / 60) % 100;

            if (survivedTime_Minute > 99)
            {
                survivedTime_Minute = 99;
                survivedTime_Second = 59;
                survivedTime_centiSecond = 99;
            }

            rankingScore[i].text = $"{survivedTime_Minute:D2}:{survivedTime_Second:D2}:{survivedTime_centiSecond:D2}";
        }
    }

    public void GetSettingButtonControl(InputAction.CallbackContext context)
    {
        var control = context.control;

        switch (control.name)
        {
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

    private void AllocateEscapeActions()
    {
        action = new KeyBoardInputActions();
        undoKeyBoard = action.Car.UndoKeyBoard;
        undoSteeringWheel = action.Car.UndoSteeringWheel;
    }
}
