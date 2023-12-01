using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerTransform : MonoBehaviour
{
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        GameProcessController.OnChanged += NewState;
    }

    private void NewState(GameStates state)
    {
        if (state == GameStates.restart || state == GameStates.start)
        {
            transform.position = startPos;
            transform.rotation = Quaternion.identity;
        }
    }

    private void OnDestroy()
    {
        GameProcessController.OnChanged -= NewState;
    }

}
