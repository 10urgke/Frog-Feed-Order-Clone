using System.Collections;
using System.Collections.Generic;
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

        for (int i = 0; i < 6; i++)
        {
            PlaceObjectOnCell(grid[i, 5], GetFrogPrefabByIndex(i));
        }

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                PlaceObjectOnCell(grid[i, j], GetGrapePrefabByIndex(i));
            }
        }
    }

    GameObject GetFrogPrefabByIndex(int index)
    {
        switch (index)
        {
            case 0: return frogBluePrefab;
            case 1: return frogGreenPrefab;
            case 2: return frogPurplePrefab;
            case 3: return frogRedPrefab;
            case 4: return frogYellowPrefab;
            default: return frogBluePrefab;
        }
    }

    GameObject GetGrapePrefabByIndex(int index)
    {
        switch (index)
        {
            case 0: return grapeBluePrefab;
            case 1: return grapeGreenPrefab;
            case 2: return grapePurplePrefab;
            case 3: return grapeRedPrefab;
            case 4: return grapeYellowPrefab;
            default: return grapeBluePrefab;
        }
    }

    void PlaceObjectOnCell(GameObject cell, GameObject prefab)
    {
        if (prefab != null)
        {
            Instantiate(prefab, cell.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
        }
    }
}
