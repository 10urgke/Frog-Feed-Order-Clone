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
        int gridSizeX = gridManager.gridSizeX;
        int gridSizeY = gridManager.gridSizeY;

        int[,] frogPositions = new int[,] {
            {0, 5}, {1, 5}, {2, 5}, {3, 5}, {4, 5}, {5, 5}
        };

        PlaceObjects(frogPositions, GetFrogPrefab, gridSizeX, gridSizeY);

        int[,] grapePositions = new int[gridSizeX * (gridSizeY - 1), 2];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY - 1; y++)
            {
                grapePositions[x * (gridSizeY - 1) + y, 0] = x;
                grapePositions[x * (gridSizeY - 1) + y, 1] = y;
            }
        }

        PlaceObjects(grapePositions, GetGrapePrefab, gridSizeX, gridSizeY);
    }

    void PlaceObjects(int[,] positions, System.Func<string, GameObject> getPrefab, int gridSizeX, int gridSizeY)
    {
        GameObject[,] grid = gridManager.GetGrid();
        for (int i = 0; i < positions.GetLength(0); i++)
        {
            int x = positions[i, 0];
            int y = positions[i, 1];
            if (x < gridSizeX && y < gridSizeY)
            {
                GameObject cell = grid[x, y];
                GameObject prefab = getPrefab(cell.name);
                if (prefab != null)
                {
                    Instantiate(prefab, cell.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
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
}
