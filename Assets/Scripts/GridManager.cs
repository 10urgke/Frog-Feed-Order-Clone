using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 4;
    public int columns = 4;
    public float cellSize = 1.0f;

    public GameObject cellBluePrefab;
    public GameObject cellGreenPrefab;
    public GameObject cellPurplePrefab;
    public GameObject cellRedPrefab;
    public GameObject cellYellowPrefab;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Vector3 cellPosition = new Vector3(i * cellSize, 0, j * cellSize);
                GameObject cellPrefab = GetCellPrefab(i, j);
                Instantiate(cellPrefab, cellPosition, Quaternion.identity, transform);
            }
        }
    }

    GameObject GetCellPrefab(int row, int column)
    {
        if ((row + column) % 5 == 0)
            return cellBluePrefab;
        if ((row + column) % 5 == 1)
            return cellGreenPrefab;
        if ((row + column) % 5 == 2)
            return cellPurplePrefab;
        if ((row + column) % 5 == 3)
            return cellRedPrefab;
        return cellYellowPrefab;
    }
}
