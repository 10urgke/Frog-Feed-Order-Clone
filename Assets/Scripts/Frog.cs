using System.Collections;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public Color frogColor;
    public GameObject tonguePrefab;
    public float tongueSpeed = 5f;

    private bool isInteracted = false;
    private GameObject tongueInstance;

    void OnMouseDown()
    {
        if (!isInteracted)
        {
            GameManager.Instance.OnFrogInteracted(this);
        }
    }

    public void ExtendTongue(Vector3 targetPosition)
    {
        isInteracted = true;
        tongueInstance = Instantiate(tonguePrefab, transform.position, Quaternion.identity);
        StartCoroutine(MoveTongue(targetPosition));
    }

    private IEnumerator MoveTongue(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * tongueSpeed;
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            tongueInstance.GetComponent<LineRenderer>().SetPosition(1, newPosition - startPosition);
            yield return null;
        }

        // Dilin meyveyi topladıktan sonra geri dönmesi
        StartCoroutine(RetractTongue(startPosition));
    }

    private IEnumerator RetractTongue(Vector3 startPosition)
    {
        Vector3 targetPosition = tongueInstance.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * tongueSpeed;
            Vector3 newPosition = Vector3.Lerp(targetPosition, startPosition, elapsedTime);
            tongueInstance.GetComponent<LineRenderer>().SetPosition(1, newPosition - startPosition);
            yield return null;
        }

        Destroy(tongueInstance);
        isInteracted = false;
    }
}
