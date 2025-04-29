using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FocusReactionTestManager : MonoBehaviour
{
    [SerializeField] private TestManager testManager;
    [SerializeField] private ShapeGenerator shapeGenerator;
    [SerializeField] private GameObject shapePrefab;
    [SerializeField] private GameObject countdownImage;
    [SerializeField] private Countdown countdown;


    private const int questionCount = 5;
    private int[] questions = new int[questionCount];
    private int currentQuestion = 0;

    private Shapes targetShape;
    private Colors targetColor;

    private bool isWaitingForReaction = false;
    private float reactionStartTime = 0f;


    private void Start()
    {
        StartCoroutine("StartCountDownThenNext");
    }

    private void SetRandomTarget()
    {
        targetShape = (Shapes)Random.Range(0, 3);
        targetColor = (Colors)Random.Range(0, 3);
    }

    private IEnumerator StartCountDownThenNext()
    {
        countdownImage.SetActive(true);
        yield return StartCoroutine(countdown.CountDown());
        countdownImage.SetActive(false);

        SetRandomTarget();
        UpdateImperativeText();

        StartCoroutine(ShowShapeSequence());
    }

    private void UpdateImperativeText()
    {
        string shapeName = targetShape switch
        {
            Shapes.Circle => "원",
            Shapes.Triangle => "삼각형",
            Shapes.Square => "사각형",
            _ => "도형"
        };

        string colorName = targetColor switch
        {
            Colors.Red => "빨간색",
            Colors.Green => "초록색",
            Colors.Blue => "파란색",
            _ => "색"
        };

        testManager.ChangeImperativeText($"{currentQuestion + 1}. {colorName} {shapeName}이(가) 나오면 빠르게 버튼을 누르세요.");
    }


    private IEnumerator ShowShapeSequence()
    {
        while (currentQuestion < questions.Length)
        {
            // 정답 등장 위치: 1~5번 중 하나
            int randomIndex = Random.Range(1, 6);

            for (int i = 1; i <= 5; i++)
            {
                // 정답 모형이 등장하는 경우 정해진 색깔과 형태로
                if (i == randomIndex)
                {
                    shapeGenerator.SetShape(targetShape);
                    shapeGenerator.SetColor(targetColor);

                    isWaitingForReaction = true;
                    reactionStartTime = Time.time;

                    yield return new WaitForSeconds(3f);

                    if (isWaitingForReaction)
                    {
                        Debug.Log("실패! 3초 안에 버튼 안 눌렀음");
                        isWaitingForReaction = false;
                        currentQuestion++;
                        StartCoroutine(StartCountDownThenNext());
                        yield break;
                    }
                }
                // 오답 모형이 등장하는 경우 랜덤 색깔과 형태로
                else
                {  
                    MakeShape();
                    PaintColor();
                    yield return new WaitForSeconds(3f);
                }
            }

            // 정답을 맞추든 틀리든 다음 문제는 카운트다운 이후 진행
            currentQuestion++;
            StartCoroutine(StartCountDownThenNext());
        }
    }

    private void MakeShape()
    {
        Shapes randomShape = (Shapes)Random.Range(0, 3);
        shapeGenerator.SetShape(randomShape);
    }

    private void PaintColor()
    {
        Colors randomColor = (Colors)Random.Range(0, 3);
        shapeGenerator.SetColor(randomColor);
    }

    public void OnclickButton()
    {
        if (isWaitingForReaction)
        {
            float reactionTime = Time.time - reactionStartTime;
            if (reactionTime <= 3f)
            {
                Debug.Log("성공! 반응 속도: " + reactionTime + "초");
            }
            else
            {
                Debug.Log("3초 넘어서 실패");
            }

            isWaitingForReaction = false;
            currentQuestion++;
        }
        else
        {
            Debug.Log("아직 눌러야 할 상황이 아님");
        }

        StartCoroutine(StartCountDownThenNext());
    }
}
