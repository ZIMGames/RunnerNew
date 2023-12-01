using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpline : MonoBehaviour
{
    public float xOffset;
    public float changeSplineDur = 0.2f;


    private void Start()
    {
        SwipeMovement.OnMove += UpdateSpline;
        xOffset = Config.Instance.splineOffset;
    }

    private void UpdateSpline(SwipeDirection newDir)
    {
        Vector3 targetOffset = Vector3.zero;
        transform.localPosition = new Vector3(Mathf.RoundToInt(transform.localPosition.x), transform.localPosition.y, transform.localPosition.z);

        if (newDir == SwipeDirection.left && transform.localPosition.x > -1)
        {
            targetOffset = new Vector3(-xOffset, 0, 0);
        }
        else if (newDir == SwipeDirection.right && transform.localPosition.x < 1)
        {
            targetOffset = new Vector3(xOffset, 0, 0);
        }
        
        if (targetOffset != Vector3.zero)
        {
            StartCoroutine(MoveSmoothly(targetOffset));
        }
    }

    private IEnumerator MoveSmoothly(Vector3 targetOffset)
    {
        Vector3 start = transform.localPosition;
        Vector3 target = start + targetOffset;
        float elapsedTime = 0f;

        while (elapsedTime < changeSplineDur)
        {
            Vector3 tempPosition = transform.localPosition;
            tempPosition.x = Mathf.Lerp(start.x, target.x, elapsedTime / changeSplineDur);
            transform.localPosition = tempPosition;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = target;
    }

    private void OnDestroy()
    {
        SwipeMovement.OnMove -= UpdateSpline;
    }
}
