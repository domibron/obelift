using UnityEngine;
using UnityEngine.UI;

public class LevelSrage : MonoBehaviour
{
    [SerializeField]
    LevelSelectBut[] buttons;

    [SerializeField]
    LevelSelectStage levelStageSelect;


    public void SelectButton(int column, int sceneIndex)
    {
        levelStageSelect.SelectStage(column, sceneIndex);
    }

    public void SetButtonsActive(bool active = false)
    {
        foreach (var but in buttons)
        {
            but.GetComponent<Button>().interactable = active;
        }
    }
}
