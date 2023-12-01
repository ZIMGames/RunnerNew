using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    public static Config Instance;

    public float turningTime = 1.2f;
    public float roadSpeed = 2f;
    public float splineOffset = 1f;

    private void Awake()
    {
        Instance = this;
    }
}
