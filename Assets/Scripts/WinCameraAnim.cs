using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCameraAnim : MonoBehaviour
{
    [SerializeField] private Transform endPos;
    private float changeDuration = 1.3f;

    private void Start()
    {
        GameProcessController.OnChanged += ChangePos;
    }

    private void ChangePos(GameStates state)
    {
        if (state == GameStates.win)
        {
            StartCoroutine(ChangeTransformCoroutine());
        }
    }

    private IEnumerator ChangeTransformCoroutine()
    {
        Vector3 startPos = transform.position;
        Quaternion startRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < changeDuration)
        {
            transform.position = Vector3.Lerp(startPos, endPos.position, elapsedTime / changeDuration);
            transform.rotation = Quaternion.Lerp(startRotation, endPos.rotation, elapsedTime / changeDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos.position;
        transform.rotation = endPos.rotation;
    }

    private void OnDestroy()
    {
        GameProcessController.OnChanged -= ChangePos;
    }
}
