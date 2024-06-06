using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject blueCellPrefab;
    public GameObject greenCellPrefab;
    public GameObject purpleCellPrefab;
    public GameObject redCellPrefab;
    public GameObject yellowCellPrefab;
    public int gridSizeX = 6;
    public int gridSizeY = 6;
    private GameObject[,] grid;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new GameObject[gridSizeX, gridSizeY];
        float cellSize = 1.1f;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                GameObject cellPrefab = GetCellPrefab(x, y);
                Vector3 position = new Vector3(x * cellSize, 0, y * cellSize);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);
                grid[x, y] = cell;
            }
        }
    }

    GameObject GetCellPrefab(int x, int y)
    {
        if (y == gridSizeY - 1)
        {
            return GetFrogCellPrefab(x);
        }
        else
        {
            int colorIndex = x % 5;
            switch (colorIndex)
            {
                case 0:
                    return blueCellPrefab;
                case 1:
                    return greenCellPrefab;
                case 2:
                    return purpleCellPrefab;
                case 3:
                    return redCellPrefab;
                case 4:
                    return yellowCellPrefab;
                default:
                    return blueCellPrefab;
            }
        }
    }

    GameObject GetFrogCellPrefab(int x)
    {
        switch (x)
        {
            case 0:
                return blueCellPrefab;
            case 1:
                return greenCellPrefab;
            case 2:
                return purpleCellPrefab;
            case 3:
                return redCellPrefab;
            case 4:
                return yellowCellPrefab;
            case 5:
                return blueCellPrefab;
            default:
                return blueCellPrefab;
        }
    }

    public GameObject[,] GetGrid()
    {
        return grid;
    }
}
