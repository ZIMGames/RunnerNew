using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMover : MonoBehaviour
{
    private float speed;
    private float turningTime;

    private GameStates state;
    private Vector3 moveDirection;

    private void Start()
    {
        GameProcessController.OnChanged += UpdateState;
        PlayerCollision.OnCorner += CalcDirection;

        turningTime = Config.Instance.turningTime;
        speed = Config.Instance.roadSpeed;

        moveDirection = new Vector3(0f, 0f, -speed);
    }

    private void FixedUpdate()
    {
        if (state == GameStates.start)
        {
            gameObject.transform.position += moveDirection * speed * Time.fixedDeltaTime;
        }
    }

    private void CalcDirection(float angle)
    {
        Vector3 newDir = Vector3.zero;

        if (angle > 0)
        {
            newDir = new Vector3(-speed, 0, 0);
        }
        else
        {
            newDir = new Vector3(0, 0, -speed);
        }

        StartCoroutine(ChangeDirection(turningTime, newDir));
    }

    private IEnumerator ChangeDirection(float changeTime, Vector3 newDir)
    {
        Vector3 startDir = moveDirection;
        float elapsedTime = 0f;

        while (elapsedTime < changeTime)
        {
            float t = Mathf.Sin((elapsedTime / changeTime) * Mathf.PI * 0.5f);
            moveDirection = Vector3.Slerp(startDir, newDir, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        moveDirection = newDir;
    }

    private void UpdateState(GameStates newState)
    {
        state = newState;
        if (state == GameStates.restart)
        {
            moveDirection = new Vector3(0f, 0f, -speed);
        }
    }

    private void OnDestroy()
    {
        GameProcessController.OnChanged -= UpdateState;
        PlayerCollision.OnCorner -= CalcDirection;
    }
}
