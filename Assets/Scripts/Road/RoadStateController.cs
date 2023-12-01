using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadStateController : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;

    private void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
        GameProcessController.OnChanged += UpdateState;
    }

    private void UpdateState(GameStates newState)
    {
        if (newState == GameStates.restart)
        {
            transform.position = startPos;
            transform.rotation = startRot;
        }
    }

    private void OnDestroy()
    {
        GameProcessController.OnChanged -= UpdateState;
    }
}
