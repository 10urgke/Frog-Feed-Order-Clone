using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject blueCellPrefab;
    public GameObject greenCellPrefab;
    public GameObject purpleCellPrefab;
    public GameObject redCellPrefab;
    public GameObject yellowCellPrefab;
    public int gridSize = 5;
    private GameObject[,] grid;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new GameObject[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GameObject cellPrefab = GetRandomCellPrefab();
                GameObject cell = Instantiate(cellPrefab, new Vector3(x, 0, y), Quaternion.identity);
                grid[x, y] = cell;
            }
        }
    }

    GameObject GetRandomCellPrefab()
    {
        GameObject[] cellPrefabs = new GameObject[] { blueCellPrefab, greenCellPrefab, purpleCellPrefab, redCellPrefab, yellowCellPrefab };
        return cellPrefabs[Random.Range(0, cellPrefabs.Length)];
    }

    public GameObject[,] GetGrid()
    {
        return grid;
    }
}
