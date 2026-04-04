using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    [SerializeField]
    bool lookAway = false;

    Transform camTrans;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camTrans = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!lookAway)
        {
            transform.LookAt(camTrans);
        }
        else
        {
            transform.LookAt(transform.position - (camTrans.position - transform.position));
        }
    }
}
