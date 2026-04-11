using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectStage : MonoBehaviour
{
    [SerializeField]
    int currentStage = 0;

    [SerializeField]
    int lastIndex = 0;

    [SerializeField]
    int selectedIndex = 0;

    [SerializeField]
    LevelSrage[] rows;

    void Start()
    {
        if (Storage.Instance != null)
        {
            if (Storage.Instance.lastColumn == -1 || Storage.Instance.currentStage == -1)
            {
                ActivateRow(0);
                return;
            }

            currentStage = Storage.Instance.currentStage;
            lastIndex = Storage.Instance.lastColumn;
            selectedIndex = Storage.Instance.selectedColumn;
        }

        ActivateRow(currentStage);
    }

    public void SelectStage(int column, int sceneIndex)
    {
        currentStage++;
        lastIndex = column;

        if (Storage.Instance != null)
        {
            Storage.Instance.currentStage = currentStage;
            Storage.Instance.lastColumn = lastIndex;
            Storage.Instance.selectedColumn = selectedIndex;
        }

        SceneManager.LoadScene(sceneIndex);
    }

    public void ActivateRow(int row)
    {
        for (int i = 0; i < rows.Length; i++)
        {
            rows[i].SetButtonsActive(i == row);
        }
    }
}
