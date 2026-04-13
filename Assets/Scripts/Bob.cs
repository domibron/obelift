using UnityEngine;
using UnityEngine.AI;

public class Bob : MonoBehaviour
{
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Transform targetBobTrans;

    [SerializeField] float bobAplitude = 1;
    [SerializeField] float bobFreq = 1;

    Vector3 defaultPos;

    float localTime;

    void Start()
    {
        defaultPos = targetBobTrans.localPosition;
    }

    void Update()
    {
        if (navMeshAgent.velocity.magnitude > 0.05f)
        {
            targetBobTrans.localPosition = defaultPos + (Vector3.up * ((Mathf.Sin((localTime * Mathf.PI) * bobFreq) + 1f) / 2f) * bobAplitude);
            localTime += Time.deltaTime;
        }
        else
        {
            targetBobTrans.localPosition = defaultPos;
            localTime = 0f;
        }
    }

}
