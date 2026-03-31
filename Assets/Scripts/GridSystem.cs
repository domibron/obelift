using System;
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

    delegate Vector2Int ConvertToGrid(Vector3 pos);

    Vector2Int? selectedGridPos = null;

    public event Action<Vector3> OnGridSelected;
    public event Action<Vector3> OnGridHoverChanged;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        UpdateSelectedGridPos();

        if (InputSystem.actions.FindAction("Attack").IsPressed())
        {
            if (selectedGridPos.HasValue)
            {
                Debug.DrawLine(GetGridPosAsWrold(selectedGridPos.Value), GetGridPosAsWrold(selectedGridPos.Value) + Vector3.up, Color.cyan, 1f);
            }
        }
    }

    void UpdateSelectedGridPos()
    {
        if (Physics.Raycast(GetMouseAsRay(), out RaycastHit hit, maxDist, gridLayerMask, QueryTriggerInteraction.Ignore))
        {
            selectedGridPos = GetGridPosRound(hit.point);
        }
        else
        {
            selectedGridPos = null;
        }
    }

    Ray GetMouseAsRay()
    {
        return mainCam.ScreenPointToRay(Mouse.current.position.value);
    }

    Vector2Int GetGridPosRound(Vector3 pos)
    {
        Vector3 localPos = pos - transform.position;

        return new Vector2Int(Mathf.RoundToInt(localPos.x), Mathf.RoundToInt(localPos.z));
    }

    // This is cool, will remove later just want it in history.
    // Vector3 GetGridPosAsWorld(Vector3 pos, ConvertToGrid gridFunc)
    // {
    //     return transform.position + ConvertVec2IntoToVec3(gridFunc.Invoke(pos));
    // }

    Vector3 GetGridPosAsWrold(Vector2Int pos)
    {
        return transform.position + ConvertVec2IntoToVec3(pos);

    }

    Vector3 GetPosAsGridWorld(Vector3 pos)
    {
        return transform.position + ConvertVec2IntoToVec3(GetGridPosRound(pos));

    }

    Vector3 ConvertVec2IntoToVec3(Vector2Int vec, float y = 0)
    {
        return new Vector3(vec.x, y, vec.y);
    }

    void OnDrawGizmos()
    {
        Vector3 size = new Vector3(gridSize, gridSize, gridSize);

        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, size);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3.right * gridSize), size);
        Gizmos.DrawWireCube(transform.position + (Vector3.forward * gridSize), size);
        Gizmos.DrawWireCube(transform.position + (Vector3.left * gridSize), size);
        Gizmos.DrawWireCube(transform.position + (Vector3.back * gridSize), size);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + (Vector3.right * gridSize) + (Vector3.forward * gridSize), size);
        Gizmos.DrawWireCube(transform.position + (Vector3.left * gridSize) + (Vector3.forward * gridSize), size);
        Gizmos.DrawWireCube(transform.position + (Vector3.right * gridSize) + (Vector3.back * gridSize), size);
        Gizmos.DrawWireCube(transform.position + (Vector3.left * gridSize) + (Vector3.back * gridSize), size);

        Gizmos.color = Color.white;
    }
}
