using UnityEngine;

public class LockAxis : MonoBehaviour
{
    [SerializeField]
    bool x, y, z = false;

    [SerializeField]
    bool lockInWorld = true;

    Vector3 lockedRot;

    void Awake()
    {
        if (lockInWorld)
            lockedRot = transform.rotation.eulerAngles;
        else
            lockedRot = transform.localEulerAngles;
    }

    void Update()
    {
        if (lockInWorld)
        {
            if (x) transform.rotation = Quaternion.Euler(lockedRot.x, transform.eulerAngles.y, transform.eulerAngles.z);

            if (y) transform.rotation = Quaternion.Euler(transform.eulerAngles.x, lockedRot.y, transform.eulerAngles.z);

            if (z) transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, lockedRot.z);
        }
        else
        {
            if (x) transform.rotation = Quaternion.Euler(lockedRot.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

            if (y) transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, lockedRot.y, transform.localEulerAngles.z);

            if (z) transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, lockedRot.z);
        }
    }
}
