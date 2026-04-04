using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Health health;

    [SerializeField]
    Image fillImage;

    void Start()
    {
        if (!health)
        {
            health = GetComponentInParent<Health>();
        }

        if (!health)
        {
            health = GetComponentInChildren<Health>();
        }

        if (!health)
        {
            Debug.LogError("Cannot get health component!", gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!health) return;

        fillImage.fillAmount = health.GetHealthNormalized();
    }
}
