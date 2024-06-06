using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public Color frogColor;
    public GameObject tonguePrefab;
    public float tongueSpeed = 5f;
    public string berryTag;

    private bool isInteracted = false;
    private GameObject tongueInstance;
    private List<GameObject> collectedBerries = new List<GameObject>();

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
        LineRenderer lineRenderer = tongueInstance.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.startColor = frogColor;
        lineRenderer.endColor = frogColor;
        StartCoroutine(MoveTongue(targetPosition));
    }

    private IEnumerator MoveTongue(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;
        LineRenderer lineRenderer = tongueInstance.GetComponent<LineRenderer>();

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * tongueSpeed;
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime);
            lineRenderer.SetPosition(1, newPosition);

            RaycastHit hit;
            if (Physics.Raycast(startPosition, newPosition - startPosition, out hit, (newPosition - startPosition).magnitude))
            {
                if (hit.collider.CompareTag(berryTag))
                {
                    collectedBerries.Add(hit.collider.gameObject);
                    hit.collider.gameObject.SetActive(false);
                }
                else if (!hit.collider.CompareTag(berryTag))
                {
                    StartCoroutine(RetractTongue(startPosition));
                    yield break;
                }
            }

            yield return null;
        }

        StartCoroutine(RetractTongue(startPosition));
    }

    private IEnumerator RetractTongue(Vector3 startPosition)
    {
        Vector3 targetPosition = tongueInstance.GetComponent<LineRenderer>().GetPosition(1);
        float elapsedTime = 0f;
        LineRenderer lineRenderer = tongueInstance.GetComponent<LineRenderer>();

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * tongueSpeed;
            Vector3 newPosition = Vector3.Lerp(targetPosition, startPosition, elapsedTime);
            lineRenderer.SetPosition(1, newPosition);
            yield return null;
        }

        Destroy(tongueInstance);
        isInteracted = false;
        collectedBerries.Clear();
    }
}
