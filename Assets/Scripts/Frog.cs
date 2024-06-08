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
    private bool hasHitWrongBerry = false;

    void Update()
    {
        if (!isInteracted && Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    GameManager.Instance.OnFrogInteracted(this);
                }
            }
        }
    }

    public void ExtendTongue(Vector3 targetPosition)
    {
        isInteracted = true;
        hasHitWrongBerry = false;
        collectedBerries.Clear();
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

            Vector3 direction = newPosition - startPosition;
            float distance = direction.magnitude;
            direction.Normalize();

            RaycastHit[] hits = Physics.RaycastAll(startPosition, direction, distance);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag(berryTag) && !collectedBerries.Contains(hit.collider.gameObject))
                    {
                        collectedBerries.Add(hit.collider.gameObject);
                        Debug.Log("Berry collected!");
                    }
                    else if (!hit.collider.CompareTag(berryTag))
                    {
                        hasHitWrongBerry = true;
                        break;
                    }
                }
            }

            if (hasHitWrongBerry)
            {
                break;
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

        if (!hasHitWrongBerry)
        {
            GameManager.Instance.ReplaceBerries(collectedBerries, this);
        }

        Destroy(tongueInstance);
        isInteracted = false;
        collectedBerries.Clear();
    }
}
