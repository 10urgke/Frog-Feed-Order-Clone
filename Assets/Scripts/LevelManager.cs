using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject frogBluePrefab;
    public GameObject frogGreenPrefab;
    public GameObject frogPurplePrefab;
    public GameObject frogRedPrefab;
    public GameObject frogYellowPrefab;

    public GameObject grapeBluePrefab;
    public GameObject grapeGreenPrefab;
    public GameObject grapePurplePrefab;
    public GameObject grapeRedPrefab;
    public GameObject grapeYellowPrefab;

    private GridManager gridManager;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        PlaceFrogsAndGrapes();
    }

    void PlaceFrogsAndGrapes()
    {
        GameObject[,] grid = gridManager.GetGrid();

        for (int x = 0; x < gridManager.gridSize; x++)
        {
            for (int y = 0; y < gridManager.gridSize; y++)
            {
                GameObject cell = grid[x, y];
                string cellName = cell.name;

                if (Random.value > 0.7f)
                {
                    if (Random.value > 0.5f)
                    {
                        PlaceObjectOnCell(cell, GetFrogPrefab(cellName));
                    }
                    else
                    {
                        PlaceObjectOnCell(cell, GetGrapePrefab(cellName));
                    }
                }
            }
        }
    }

    GameObject GetFrogPrefab(string cellName)
    {
        if (cellName.Contains("Blue")) return frogBluePrefab;
        if (cellName.Contains("Green")) return frogGreenPrefab;
        if (cellName.Contains("Purple")) return frogPurplePrefab;
        if (cellName.Contains("Red")) return frogRedPrefab;
        if (cellName.Contains("Yellow")) return frogYellowPrefab;
        return null;
    }

    GameObject GetGrapePrefab(string cellName)
    {
        if (cellName.Contains("Blue")) return grapeBluePrefab;
        if (cellName.Contains("Green")) return grapeGreenPrefab;
        if (cellName.Contains("Purple")) return grapePurplePrefab;
        if (cellName.Contains("Red")) return grapeRedPrefab;
        if (cellName.Contains("Yellow")) return grapeYellowPrefab;
        return null;
    }

    void PlaceObjectOnCell(GameObject cell, GameObject prefab)
    {
        if (prefab != null)
        {
            Instantiate(prefab, cell.transform.position, Quaternion.identity);
        }
    }
}
