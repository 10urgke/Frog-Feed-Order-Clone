using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int moveLimit = 10;
    private int currentMoves = 0;
    private GridManager gridManager;
    public GameObject frogBluePrefab;
    public GameObject frogGreenPrefab;
    public GameObject frogPurplePrefab;
    public GameObject frogRedPrefab;
    public GameObject frogYellowPrefab;
    public TMPro.TextMeshProUGUI moveCounterText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    private void Update()
    {
       moveCounterText.text = $"{moveLimit-currentMoves} MOVES";
    }

    public void OnFrogInteracted(Frog frog)
    {
        if (currentMoves < moveLimit)
        {
            Vector3 targetPosition = DetermineTargetPosition(frog);
            frog.ExtendTongue(targetPosition);
            currentMoves++;
        }
        else
        {
            Debug.Log("Move limit reached. Game Over.");
        }
    }

    private Vector3 DetermineTargetPosition(Frog frog)
    {
        return frog.transform.position + new Vector3(0, 0, -5f);
    }

    public void ReplaceBerries(List<GameObject> collectedBerries, Frog frog)
    {
        foreach (var berry in collectedBerries)
        {
            Vector3 position = berry.transform.position;
            berry.SetActive(false);
            gridManager.ReplaceBerryAtPosition(position);
        }

        // Frog repositioning
        gridManager.PlaceFrogAtRandomPosition(GetFrogPrefab(frog));
    }

    private GameObject GetFrogPrefab(Frog frog)
    {
        if (frog.CompareTag("BlueFrog")) return frogBluePrefab;
        if (frog.CompareTag("GreenFrog")) return frogGreenPrefab;
        if (frog.CompareTag("PurpleFrog")) return frogPurplePrefab;
        if (frog.CompareTag("RedFrog")) return frogRedPrefab;
        if (frog.CompareTag("YellowFrog")) return frogYellowPrefab;
        return null;
    }
}
