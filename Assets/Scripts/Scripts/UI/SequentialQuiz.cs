using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

public class SequentialQuiz : MonoBehaviour
{
    [SerializeField] private List<Button> slotButtons = new();
    [SerializeField] private List<TMP_Text> slotTexts = new();

    [SerializeField] private List<Button> choosenButtons = new();
    [SerializeField] private List<TMP_Text> choosenTexts = new();

    [SerializeField] private Sprite defaultSlotSprite = null;
    [SerializeField] private Button confirmButton = null;

    public List<SpawnObject> slotList { get; private set; }
    private List<SpawnObject> choosenAnswer;

    private void Awake()
    {
        slotList = Enumerable
            .Repeat<SpawnObject>(null, slotButtons.Count)
            .ToList();

        choosenAnswer = Enumerable
            .Repeat<SpawnObject>(null, choosenButtons.Count)
            .ToList();
    }

    private void OnEnable()
    {
        for (int i = 0; i < slotButtons.Count; i++)
        {
            slotButtons[i].image.sprite = defaultSlotSprite;
            slotTexts[i].text = "";
            slotList[i] = null;
        }

        var spawned = SelectivePointSpawner.Instance.spawnedInfos;
        var slotIndices = Enumerable.Range(0, slotButtons.Count)
                                    .OrderBy(_ => Random.value)
                                    .Take(spawned.Count)
                                    .ToList();
        
        for (int i = 0; i < spawned.Count; i++)
        {
            int randomSlot = slotIndices[i];
            DisplayInformation(randomSlot, spawned[i]);
        }

        for (int i = 0; i < choosenButtons.Count; i++)
        {
            choosenButtons[i].image.sprite = defaultSlotSprite;
            choosenTexts[i].text = "";
            choosenAnswer[i] = null;
        }

        confirmButton.gameObject.SetActive(false);
    }

    public void DisplayInformation(int slotIndex, SpawnObject so)
    {
        if (slotIndex < 0 || slotIndex >= slotButtons.Count || so == null) return;

        slotButtons[slotIndex].image.sprite = so.sprite;
        slotTexts[slotIndex].text = so.name;
        slotList[slotIndex] = so;
    }

    public void OnClickSlotButton()
    {
        var go = EventSystem.current.currentSelectedGameObject;
        var btn = go?.GetComponent<Button>();
        if (btn == null) return;

        int slotIndex = slotButtons.IndexOf(btn);
        if (slotIndex < 0) return;

        var so = slotList[slotIndex];
        if (so == null) return;

        for (int i = 0; i < choosenAnswer.Count; i++)
        {
            if (choosenAnswer[i] == null)
            {
                choosenButtons[i].image.sprite = so.sprite;
                choosenTexts[i].text = so.name;
                choosenAnswer[i] = so;
                break;
            }
        }

        UpdateConfirmButtonState();
    }

    public void OnClickChoosenButton()
    {
        var go = EventSystem.current.currentSelectedGameObject;
        if (go == null) return;
        var btn = go.GetComponent<Button>();
        if (btn == null) return;

        int idx = choosenButtons.IndexOf(btn);
        if (idx < 0) return;

        choosenButtons[idx].image.sprite = defaultSlotSprite;
        choosenTexts[idx].text = "";
        choosenAnswer[idx] = null;

        UpdateConfirmButtonState();
    }

    private void UpdateConfirmButtonState()
    {
        bool allFilled = choosenAnswer.All(x => x != null);
        confirmButton.gameObject.SetActive(allFilled);
    }

    public void OnClickConfirmButton()
    {
        var spawned = SelectivePointSpawner.Instance.spawnedInfos;
        int wrongCount = 0;
        int totalCount = spawned.Count;

        for (int i = 0; i < totalCount; i++)
        {
            if (choosenAnswer[i] != spawned[i])
            {
                wrongCount++;
            }
        }

        if(wrongCount == 0)
        {
            Debug.Log("다 맞았습니다.");
        }
        else
        {
            Debug.Log($"{wrongCount}개 틀렸습니다.");
        }

    }
}
