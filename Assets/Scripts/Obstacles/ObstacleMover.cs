using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed;
    public float Offset;

    public bool zAxis;
    private float localPos;
    private float posVal;
    private Vector3 dir;

    private void Start()
    {
        SetDir();
    }
    private void FixedUpdate()
    {
        transform.localPosition += dir * speed * Time.fixedDeltaTime;

        if (zAxis)
        {
            posVal = transform.localPosition.z;
        }
        else
        {
            posVal = transform.localPosition.x;
        }

        float delta = Mathf.Abs(Mathf.Abs(posVal) - Mathf.Abs(localPos));

        if (delta >= Offset)
        {
            dir *= -1;
        }
    }

    private void SetDir()
    {
        if (zAxis)
        {
            dir = new Vector3(0, 0, -Offset);
            localPos = transform.localPosition.z;
        }
        else
        {
            dir = new Vector3(-Offset, 0, 0);
            localPos = transform.localPosition.x;
        }
    }
}
