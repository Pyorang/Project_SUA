using UnityEngine;
using TMPro;

public class RankingUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI[] rankingScore;

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
}
