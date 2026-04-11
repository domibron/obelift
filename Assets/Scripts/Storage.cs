using UnityEngine;

public class Storage : MonoBehaviour
{
    public static Storage Instance { get; private set; }

    public int currentStage = -1;
    public int selectedColumn = -1;
    public int lastColumn = -1;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            // do nothing.
        }
        else if (Instance != null && Instance == this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
}
