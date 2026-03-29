using UnityEngine;
using UnityEngine.InputSystem;

public class GridSystem : MonoBehaviour
{
    [SerializeField]
    float gridSize = 1f;

    Camera mainCam;

    [SerializeField]
    float maxDist = 100f;

    [SerializeField]
    LayerMask gridLayerMask = Physics.AllLayers;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        Ray ray = mainCam.ScreenPointToRay(Mouse.current.position.value);


        if (Physics.Raycast(ray, out RaycastHit hit, maxDist, gridLayerMask, QueryTriggerInteraction.Ignore))
        {
            print("HITTING");

            Debug.DrawLine(mainCam.transform.position, hit.point, Color.green, 1f);

            print(GetGridPos(hit.collider.transform.position));
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * maxDist, Color.red, 1f);
        }

    }

    Vector2Int GetGridPos(Vector3 pos)
    {
        Vector3 localPos = pos - transform.position;

        return new Vector2Int(Mathf.FloorToInt(localPos.x), Mathf.FloorToInt(localPos.z));
    }
}
