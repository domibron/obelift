using UnityEngine;

public class LevelSelectBut : MonoBehaviour
{
    [SerializeField]
    int columnIndex = 0;

    [SerializeField]
    int sceneIndex = 0;

    [SerializeField]
    LevelSrage levelSrage;


    public void SelectButton()
    {
        levelSrage.SelectButton(columnIndex, sceneIndex);
    }
}
