using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GridUISelector : MonoBehaviour
{
    [SerializeField] private List<Button> topRowButtons = new();
    [SerializeField] private List<Button> bottomRowButtons = new();

    [SerializeField] private Button confirmButton;

    [SerializeField] private RectTransform arrowImage = null;
    [SerializeField] private float offsetY = 140f;

    private Button[,] grid = new Button[2, 3];
    private int row = 1, col = 1;

    private void Awake()
    {
        for (int c = 0; c < 3; c++)
        {
            grid[0, c] = topRowButtons[c];
            grid[1, c] = bottomRowButtons[c];
        }
    }

    private void Start()
    {
        arrowImage.gameObject.SetActive(false);

        EventSystem.current.SetSelectedGameObject(grid[row, col].gameObject);
    }

    private void Update()
    {
        if (confirmButton.gameObject.activeInHierarchy)
        {
            ShowArrowOnConfirm();
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                confirmButton.onClick.Invoke();

            return;
        }

        bool moved = false;
        if (Input.GetKeyDown(KeyCode.LeftArrow) && col > 0) { col--; moved = true; }
        if (Input.GetKeyDown(KeyCode.RightArrow) && col < 2) { col++; moved = true; }
        if (Input.GetKeyDown(KeyCode.UpArrow) && row > 0) { row--; moved = true; }
        if (Input.GetKeyDown(KeyCode.DownArrow) && row < 1) { row++; moved = true; }

        if (moved)
            ShowArrowOnGrid();
    }

    private void ShowArrowOnGrid()
    {
        var btnGO = grid[row, col].gameObject;
        EventSystem.current.SetSelectedGameObject(btnGO);

        RectTransform targetRT = btnGO.transform as RectTransform;
        arrowImage.gameObject.SetActive(true);
        arrowImage.position = new Vector3(
            targetRT.position.x,
            targetRT.position.y + offsetY,
            targetRT.position.z
        );
    }

    private void ShowArrowOnConfirm()
    {
        EventSystem.current.SetSelectedGameObject(confirmButton.gameObject);

        RectTransform rt = confirmButton.transform as RectTransform;
        arrowImage.gameObject.SetActive(true);
        arrowImage.position = new Vector3(
            rt.position.x,
            rt.position.y + offsetY,
            rt.position.z
        );
    }
}
