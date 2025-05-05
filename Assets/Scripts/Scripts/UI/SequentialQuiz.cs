using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SequentialQuiz : MonoBehaviour
{
    [SerializeField] private List<Image> chosenObjectImage = new();

    [SerializeField] private List<Image> SpawnObjectImage = new();
    [SerializeField] private List<TMP_Text> SpawnObjectName = new();

    public int SlotCount => SpawnObjectName.Count;

    public void DisplayInformation(int slotIndex, Sprite sprite, string name)
    {
        if (slotIndex < 0 || slotIndex >= SpawnObjectName.Count)
        {
            return;
        }

        SpawnObjectImage[slotIndex].sprite = sprite;
        SpawnObjectName[slotIndex].text = name;
    }
}
