using UnityEngine;
using UnityEngine.UI;
using TMPro;


[System.Serializable]
public struct TestStep
{
    public GameObject panel;
    [TextArea(2, 4)] public string explainText;
}

public class TestManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI imperativeText;
    [SerializeField] private GameObject explain;
    [SerializeField] private TextMeshProUGUI explainText;
    [SerializeField] private TestStep[] testSteps;

    public static int currentPanel = 0;

    private void Start()
    {
        HideAllPanels();
        ChangeExplainText();
    }

    private void HideAllPanels()
    {
        foreach (var step in testSteps)
        {
            if (step.panel != null)
                step.panel.SetActive(false);
        }
    }

    public void ChangeImperativeText(string text)
    {
        imperativeText.text = text;
    }

    public void ChangeExplainText()
    {
        explainText.text = testSteps[currentPanel].explainText;
    }

    public void ShowExplainText(int index)
    {
        explainText.text = testSteps[index].explainText;
        explain.gameObject.SetActive(true);
    }

    public void CloseExplainText(int index)
    {
        explain.gameObject.SetActive(false);
    }


    public void ShowTestPanel(int index)
    {
        HideAllPanels();
        imperativeText.gameObject.SetActive(true);
        testSteps[index].panel.SetActive(true);
        currentPanel = index;
        ChangeExplainText();
    }

    public void CloseTestPanel(int index)
    {
        imperativeText.gameObject.SetActive(false);
        testSteps[index].panel.SetActive(false);
    }
}
