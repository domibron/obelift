using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform Target;

    public float LerpAmount = 0.5f;

    void Update()
    {
        if (Target)
            transform.position = Vector3.Lerp(transform.position, Target.position, LerpAmount * Time.deltaTime);
    }
}
