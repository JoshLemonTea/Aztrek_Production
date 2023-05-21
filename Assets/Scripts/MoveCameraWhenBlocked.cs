using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraWhenBlocked : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float smoothing;
    [SerializeField] private float blockingRadius;
    [SerializeField] private float closeEnough;
    private GameObject childCamera;
    private Vector3 normalCameraPos;
    private Vector3 targetCameraPos;
    private Vector3 offset;
    private float normalDistance;

    // Start is called before the first frame update
    void Start()
    {
        childCamera = transform.GetChild(0).gameObject;
        normalDistance = Vector3.Distance(childCamera.transform.position, transform.position);
        offset = childCamera.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        normalCameraPos = transform.position + ((childCamera.transform.position - transform.position).normalized * normalDistance);
        CheckIfCameraIsBlocked();
        MoveCameraIfBlocked();
    }

    private void CheckIfCameraIsBlocked()
    {
        float safeDistance = 0;
        Vector3 directionTowardsPlayer = ((transform.position) - childCamera.transform.position).normalized;
        List<Collider> hits = new List<Collider>();
        hits.AddRange(Physics.OverlapSphere(normalCameraPos, blockingRadius));
        hits = clearList(hits);
        if (hits.Count > 0)
        {
            targetCameraPos = normalCameraPos + (directionTowardsPlayer * safeDistance);

            List<Collider> newHits = new List<Collider>();
            newHits.AddRange(Physics.OverlapSphere(targetCameraPos, blockingRadius));
            newHits = clearList(newHits);
            bool tooClose = false;

            while (newHits.Count > 0 && !tooClose)
            {
                safeDistance += 0.1f;
                if (safeDistance > (normalDistance - closeEnough))
                {
                    tooClose = true;
                }
                targetCameraPos = normalCameraPos + (directionTowardsPlayer * safeDistance);
                newHits.Clear();
                newHits.AddRange(Physics.OverlapSphere(targetCameraPos, blockingRadius));
                newHits = clearList(newHits);
            }
            Debug.Log(safeDistance);
        }
        else
        {
            targetCameraPos = normalCameraPos;
        }
    }

    private void MoveCameraIfBlocked()
    {
        childCamera.transform.position = Vector3.Lerp(childCamera.transform.position, targetCameraPos, smoothing);
    }

    private List<Collider> clearList(List<Collider> originalList)
    {
        for (int i = 0; i < originalList.Count; i++)
        {
            if (originalList[i].transform.gameObject.GetComponent<Player>() != null || originalList[i].isTrigger == true)
            {
                originalList.RemoveAt(i);
                i--;
            }
        }
        return originalList;
    }
}
