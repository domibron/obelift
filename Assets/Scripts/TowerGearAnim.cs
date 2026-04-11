using UnityEngine;

public class TowerGearAnim : MonoBehaviour
{
    [SerializeField] Transform outerGear;
    Vector3 g1dir = Vector3.forward;
    [SerializeField] float g1rotSpeed = 1f;
    [SerializeField] float g1spin = 50f;

    [SerializeField] Transform innerGear;
    Vector3 g2dir = Vector3.forward;
    [SerializeField] float g2rotSpeed = 1f;
    [SerializeField] float g2spin = 50f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        g1dir = new Vector3(Mathf.Sign(Time.time), Mathf.Sign(Mathf.PI + Time.time), Mathf.Cos(Time.time));
        outerGear.Rotate(g1dir.normalized * Time.deltaTime * g1rotSpeed, Space.World);
        // outerGear.Rotate(Vector3.forward * Time.deltaTime * g1spin, Space.Self);
        outerGear.Rotate(outerGear.transform.forward * Time.deltaTime * g1spin, Space.World);
        // float g1z = outerGear.localRotation.eulerAngles.z + (1 * Time.deltaTime * g1spin);
        // if (g1z > 360) g1z = 0;
        // else if (g1z < 0) g1z = 360;
        // outerGear.localRotation = Quaternion.Euler(outerGear.localRotation.eulerAngles.x, outerGear.localRotation.eulerAngles.x, g1z);

        g2dir = new Vector3(-Mathf.Sign(Time.time), -Mathf.Sign(Mathf.PI + Time.time), -Mathf.Cos(Time.time));
        innerGear.Rotate(g2dir.normalized * Time.deltaTime * g2rotSpeed, Space.World);
        // innerGear.Rotate(Vector3.forward * Time.deltaTime * g2spin, Space.Self);
        innerGear.Rotate(innerGear.transform.forward * Time.deltaTime * g2spin, Space.World);
        // float g2z = innerGear.localRotation.eulerAngles.z + (1 * Time.deltaTime * g2spin);
        // if (g2z > 360) g2z = 0;
        // else if (g2z < 0) g2z = 360;

        // innerGear.localRotation = Quaternion.Euler(innerGear.localRotation.eulerAngles.x, innerGear.localRotation.eulerAngles.y, g2z);


    }
}
