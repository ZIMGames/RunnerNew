using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCollision : MonoBehaviour
{
    public static Action<float> OnCorner;
    public static Action<bool> OnEndRound;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            OnEndRound?.Invoke(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Corner"))
        {
            float rotAngle = other.gameObject.GetComponent<CornerInfo>().CornerDegrees;
            OnCorner?.Invoke(rotAngle);
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            OnEndRound?.Invoke(true);
        }
    }
}
