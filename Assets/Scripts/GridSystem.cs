using System;
using System.Collections.Generic;
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

    [SerializeField]
    GameObject seletor;

    [SerializeField]
    GameObject selectableTilePrefab;

    GameObject lastSelected = null;

    private List<GameObject> selectableTiles = new List<GameObject>();



    public Vector3? SelectedWorldPos
    {
        get
        {
            if (!selectedGridPos.HasValue) return null;

            return GetGridPosAsWrold(selectedGridPos.Value);
        }
    }

    public Vector3? CurrentSelectedTile
    {
        get
        {
            if (!lastSelected) return null;

            return GetPosAsGridWorld(lastSelected.transform.position);
        }
    }

    void Start()
    {
        mainCam = Camera.main;

        // GenerateSelectableGrid(Vector3.zero, 5);
    }

    void Update()
    {
        UpdateSelectedGridPos();

        if (selectedGridPos.HasValue)
        {
            seletor.transform.position = GetGridPosAsWrold(selectedGridPos.Value);
            seletor.SetActive(true);

            lastSelected = null; // Set to null, then set to val if valid.

            foreach (var tile in selectableTiles)
            {
                // if (lastSelected != null)
                // {
                //     lastSelected.GetComponent<Selectable>().Deselect();
                // }
                if (!tile) continue; // skip.



                if (GetGridPosRound(tile.transform.position) == selectedGridPos.Value)
                {
                    tile.GetComponent<Selectable>().Select();
                    // OnSelectedTile?.Invoke(GetGridPosAsWrold(selectedGridPos.Value));
                    lastSelected = tile;
                }
                else
                {
                    tile.GetComponent<Selectable>().Deselect();
                }
            }
        }
        else
        {
            seletor.SetActive(false);
        }

        // if (InputSystem.actions.FindAction("Attack").IsPressed() && selectedGridPos.HasValue)
        // {
        //     Debug.DrawLine(GetGridPosAsWrold(selectedGridPos.Value), GetGridPosAsWrold(selectedGridPos.Value) + Vector3.up, Color.cyan, 1f);

        //     FindFirstObjectByType<PlayerMovement>()?.MoveTo(GetGridPosAsWrold(selectedGridPos.Value));

        // }

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

    public Vector2Int GetGridPosRound(Vector3 pos)
    {
        Vector3 localPos = pos - transform.position;

        localPos /= gridSize;

        localPos.x = Mathf.Round(localPos.x);
        localPos.z = Mathf.Round(localPos.z);

        // localPos *= gridSize;

        return new Vector2Int(Mathf.RoundToInt(localPos.x), Mathf.RoundToInt(localPos.z));
    }


    // This is cool, will remove later just want it in history.
    // Vector3 GetGridPosAsWorld(Vector3 pos, ConvertToGrid gridFunc)
    // {
    //     return transform.position + ConvertVec2IntoToVec3(gridFunc.Invoke(pos));
    // }

    public Vector3 GetGridPosAsWrold(Vector2Int pos)
    {
        return transform.position + (ConvertVec2IntoToVec3(pos) * gridSize) + (Vector3.up * (gridSize / 2f));

    }

    public Vector3 GetPosAsGridWorld(Vector3 pos)
    {
        return transform.position + (ConvertVec2IntoToVec3(GetGridPosRound(pos)) * gridSize) + (Vector3.up * (gridSize / 2f));

    }

    public Vector3 ConvertVec2IntoToVec3(Vector2Int vec, float y = 0)
    {
        return new Vector3(vec.x, y, vec.y);
    }

    public void ClearSelectableGrid()
    {
        foreach (var selectableTile in selectableTiles)
        {
            Destroy(selectableTile);
        }

        // lastSelected = null;
        selectableTiles.Clear();
    }

    public void GenerateSelectableGrid(Vector3 worldPos, int range)
    {
        ClearSelectableGrid();

        Vector2Int gridPos = GetGridPosRound(worldPos);

        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                if (Mathf.Abs(x) + Mathf.Abs(y) <= range)
                {
                    GameObject tile = Instantiate(selectableTilePrefab, GetGridPosAsWrold(new Vector2Int(gridPos.x + x, gridPos.y + y)), Quaternion.identity);
                    selectableTiles.Add(tile);
                }
            }
        }
    }

    public int GetDistanceOnGrid(Vector3 worldPosA, Vector3 worldPosB)
    {
        Vector2Int a = GetGridPosRound(worldPosA);
        Vector2Int b = GetGridPosRound(worldPosB);

        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    public bool CheckGridIsEmpty(Vector3 worldPos, int layerMask = Physics.AllLayers, QueryTriggerInteraction interaction = QueryTriggerInteraction.UseGlobal)
    {
        Vector2Int world = GetGridPosRound(worldPos);

        return CheckGridIsEmpty(world, layerMask, interaction);
    }

    public bool CheckGridIsEmpty(Vector2Int gridPos, int layerMask = Physics.AllLayers, QueryTriggerInteraction interaction = QueryTriggerInteraction.UseGlobal)
    {
        Vector3 world = GetGridPosAsWrold(gridPos);

        float halfGrid = gridSize * 0.5f;

        Vector3 cent = world + (Vector3.up * halfGrid);
        Vector3 extent = new Vector3(halfGrid - (halfGrid * 0.1f), halfGrid - (halfGrid * 0.1f), halfGrid - (halfGrid * 0.1f));

        Debug.DrawLine(cent, cent + Vector3.forward * extent.z, Color.wheat, 10f);
        Debug.DrawLine(cent, cent + Vector3.back * extent.z, Color.wheat, 10f);

        Debug.DrawLine(cent, cent + Vector3.up * extent.y, Color.wheat, 10f);
        Debug.DrawLine(cent, cent + Vector3.down * extent.y, Color.wheat, 10f);

        Debug.DrawLine(cent, cent + Vector3.right * extent.x, Color.wheat, 10f);
        Debug.DrawLine(cent, cent + Vector3.left * extent.x, Color.wheat, 10f);


        return !Physics.CheckBox(world + (Vector3.up * halfGrid), new Vector3(halfGrid - (halfGrid * 0.1f), halfGrid - (halfGrid * 0.1f), halfGrid - (halfGrid * 0.1f)), transform.rotation, layerMask, interaction);
    }

    public Collider[] GetCollidersOnGrid(Vector3 pos, int range, int layerMask = Physics.AllLayers, QueryTriggerInteraction interaction = QueryTriggerInteraction.UseGlobal)
    {
        Vector2Int world = GetGridPosRound(pos);

        return GetCollidersOnGrid(world, range, layerMask, interaction);
    }

    public Collider[] GetCollidersOnGrid(Vector2Int pos, int range, int layerMask = Physics.AllLayers, QueryTriggerInteraction interaction = QueryTriggerInteraction.UseGlobal)
    {
        Vector3 world = GetGridPosAsWrold(pos);

        float halfGrid = gridSize * 0.5f;


        List<Collider> allCols = new List<Collider>();

        for (int x = -range; x <= range; x++)
        {
            for (int y = -range; y <= range; y++)
            {
                if (Mathf.Abs(x) + Mathf.Abs(y) <= range)
                {
                    Collider[] cols = Physics.OverlapBox(GetGridPosAsWrold(pos + new Vector2Int(x, y)) + (Vector3.up * halfGrid), new Vector3(halfGrid - (halfGrid * 0.1f), halfGrid - (halfGrid * 0.1f), halfGrid - (halfGrid * 0.1f)), transform.rotation);

                    foreach (Collider col in cols)
                    {
                        if (allCols.Contains(col)) continue;
                        allCols.Add(col);
                    }
                }
            }
        }

        return allCols.ToArray();
    }

    public Collider[] GetCollidersInRange(Vector3 pos, int radius, int layerMask = Physics.AllLayers, QueryTriggerInteraction interaction = QueryTriggerInteraction.UseGlobal)
    {
        Vector2Int world = GetGridPosRound(pos);

        return GetCollidersInRange(world, radius, layerMask, interaction);
    }

    public Collider[] GetCollidersInRange(Vector2Int pos, int radius, int layerMask = Physics.AllLayers, QueryTriggerInteraction interaction = QueryTriggerInteraction.UseGlobal)
    {
        Vector3 world = GetGridPosAsWrold(pos);
        // print(world);
        float halfGrid = gridSize * 0.5f;

        List<Collider> allCols = new List<Collider>();

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                if (Mathf.FloorToInt(Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2))) <= radius)
                {
                    Collider[] cols = Physics.OverlapBox(GetGridPosAsWrold(pos + new Vector2Int(x, y)) + (Vector3.up * halfGrid), new Vector3(halfGrid - (halfGrid * 0.1f), halfGrid - (halfGrid * 0.1f), halfGrid - (halfGrid * 0.1f)), transform.rotation);

                    // Debug.DrawLine(GetGridPosAsWrold(pos + new Vector2Int(x, y)), GetGridPosAsWrold(pos + new Vector2Int(x, y)) + Vector3.up, Color.pink, 60f);

                    foreach (Collider col in cols)
                    {
                        if (allCols.Contains(col)) continue;
                        allCols.Add(col);
                    }
                }
            }
        }

        return allCols.ToArray();
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
