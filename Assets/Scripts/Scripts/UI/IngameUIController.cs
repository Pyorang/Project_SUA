using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IngameUIController : MonoBehaviour
{
    private int currentTime_centiSeconds = 0;
    private int currentTime_Second = 0;
    private int currentTime_Minute = 0;
    private float currentTime = 0;

    [SerializeField] private TextMeshProUGUI timerText;

    public void Init()
    {
        //
    }

    public void Update()
    {
        UpdateTimerTextUI();
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

        timerText.text = $"{currentTime_Minute:D2} : {currentTime_Second:D2} : {currentTime_centiSeconds:D2}";
    }
}
