using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;

    void Awake()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
    }

    public void WinContinue()
    {
        if (Storage.Instance != null)
            Storage.Instance.lastColumn = Storage.Instance.selectedColumn;

        SceneManager.LoadScene(1);
    }

    public void LoseContinue()
    {
        if (Storage.Instance != null && Storage.Instance.currentStage > 0)
        {
            Storage.Instance.currentStage--;
        }

        SceneManager.LoadScene(1);

    }
}
