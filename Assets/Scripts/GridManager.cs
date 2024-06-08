using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject blueCellPrefab;
    public GameObject greenCellPrefab;
    public GameObject purpleCellPrefab;
    public GameObject redCellPrefab;
    public GameObject yellowCellPrefab;
    public GameObject grapeBluePrefab;
    public GameObject grapeGreenPrefab;
    public GameObject grapePurplePrefab;
    public GameObject grapeRedPrefab;
    public GameObject grapeYellowPrefab;
    public GameObject frogBluePrefab;
    public GameObject frogGreenPrefab;
    public GameObject frogPurplePrefab;
    public GameObject frogRedPrefab;
    public GameObject frogYellowPrefab;
    public int gridSize = 6;
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

    public void ReplaceBerryAtPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.z);

        if (x >= 0 && x < gridSize && y >= 0 && y < gridSize)
        {
            GameObject oldCell = grid[x, y];
            string cellName = oldCell.name;
            GameObject newBerry = Instantiate(GetRandomBerryPrefab(), position, Quaternion.identity);

            GameObject newCell = Instantiate(GetCellPrefabFromBerry(newBerry), oldCell.transform.position, Quaternion.identity);
            Destroy(oldCell);
            grid[x, y] = newCell;
        }
    }

    GameObject GetRandomBerryPrefab()
    {
        GameObject[] berryPrefabs = new GameObject[] { grapeBluePrefab, grapeGreenPrefab, grapePurplePrefab, grapeRedPrefab, grapeYellowPrefab };
        return berryPrefabs[Random.Range(0, berryPrefabs.Length)];
    }

    GameObject GetCellPrefabFromBerry(GameObject berry)
    {
        if (berry.CompareTag("BlueBerry")) return blueCellPrefab;
        if (berry.CompareTag("GreenBerry")) return greenCellPrefab;
        if (berry.CompareTag("PurpleBerry")) return purpleCellPrefab;
        if (berry.CompareTag("RedBerry")) return redCellPrefab;
        if (berry.CompareTag("YellowBerry")) return yellowCellPrefab;
        return null;
    }

    public void PlaceFrogAtRandomPosition(GameObject frogPrefab)
    {
        List<Vector3> edgePositions = new List<Vector3>();

        for (int x = 0; x < gridSize; x++)
        {
            edgePositions.Add(new Vector3(x, 0, 0));
            edgePositions.Add(new Vector3(x, 0, gridSize - 1));
        }

        for (int y = 1; y < gridSize - 1; y++)
        {
            edgePositions.Add(new Vector3(0, 0, y));
            edgePositions.Add(new Vector3(gridSize - 1, 0, y));
        }

        Vector3 randomPosition = edgePositions[Random.Range(0, edgePositions.Count)];
        Instantiate(frogPrefab, randomPosition, Quaternion.identity);
    }
}
