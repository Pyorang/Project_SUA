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
            Shapes.Circle => "��",
            Shapes.Triangle => "�ﰢ��",
            Shapes.Square => "�簢��",
            _ => "����"
        };

        string colorName = targetColor switch
        {
            Colors.Red => "������",
            Colors.Green => "�ʷϻ�",
            Colors.Blue => "�Ķ���",
            _ => "��"
        };

        testManager.ChangeImperativeText($"{currentQuestion + 1}. {colorName} {shapeName}��(��) ������ ������ ��ư�� ��������.");
    }


    private IEnumerator ShowShapeSequence()
    {
        while (currentQuestion < questions.Length)
        {
            // ���� ���� ��ġ: 1~5�� �� �ϳ�
            int randomIndex = Random.Range(1, 6);

            for (int i = 1; i <= 5; i++)
            {
                // ���� ������ �����ϴ� ��� ������ ����� ���·�
                if (i == randomIndex)
                {
                    shapeGenerator.SetShape(targetShape);
                    shapeGenerator.SetColor(targetColor);

                    isWaitingForReaction = true;
                    reactionStartTime = Time.time;

                    yield return new WaitForSeconds(3f);

                    if (isWaitingForReaction)
                    {
                        Debug.Log("����! 3�� �ȿ� ��ư �� ������");
                        isWaitingForReaction = false;
                        currentQuestion++;
                        StartCoroutine(StartCountDownThenNext());
                        yield break;
                    }
                }
                // ���� ������ �����ϴ� ��� ���� ����� ���·�
                else
                {  
                    MakeShape();
                    PaintColor();
                    yield return new WaitForSeconds(3f);
                }
            }

            // ������ ���ߵ� Ʋ���� ���� ������ ī��Ʈ�ٿ� ���� ����
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
                Debug.Log("����! ���� �ӵ�: " + reactionTime + "��");
            }
            else
            {
                Debug.Log("3�� �Ѿ ����");
            }

            isWaitingForReaction = false;
            currentQuestion++;
        }
        else
        {
            Debug.Log("���� ������ �� ��Ȳ�� �ƴ�");
        }

        StartCoroutine(StartCountDownThenNext());
    }
}
