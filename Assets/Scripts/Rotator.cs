using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float turningTime;

    private void Start()
    {
        PlayerCollision.OnCorner += StartRotate;
        GameProcessController.OnChanged += ChangeState;
        turningTime = Config.Instance.turningTime;
    }

    private void ChangeState(GameStates state)
    {
        if (state == GameStates.restart)
        {
            transform.rotation = Quaternion.identity;
        }
    }

    private void StartRotate(float angle)
    {
        StartCoroutine(RotateSmoothly(angle));
    }

    private IEnumerator RotateSmoothly(float rotAngle)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, rotAngle, 0f));

        while (elapsedTime < turningTime)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / turningTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    }

    private void OnDestroy()
    {
        PlayerCollision.OnCorner -= StartRotate;
        GameProcessController.OnChanged -= ChangeState;
    }
}
