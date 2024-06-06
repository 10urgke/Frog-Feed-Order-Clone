using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int moveLimit = 10;
    private int currentMoves = 0;

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
        return frog.transform.position + new Vector3(0, 0, -6f); // Yönünü ayarlamak için
    }
}
