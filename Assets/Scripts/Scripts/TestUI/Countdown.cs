using UnityEngine;
using System.Collections;
using TMPro;

public class Countdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    public IEnumerator CountDown()
    {
        Debug.Log("ī��Ʈ�ٿ� ����");
        int currentTime = 3;

        while (currentTime > 0)
        {
            countdownText.text = currentTime.ToString();
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        countdownText.text = " ";
    }
}
