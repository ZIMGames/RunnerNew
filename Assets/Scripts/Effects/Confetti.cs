using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    [SerializeField] private GameObject EffectObj;

    private void Start()
    {
        GameProcessController.OnChanged += NewState;
    }

    private void NewState(GameStates state)
    {
        if (state == GameStates.win)
        {
            EffectObj.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        GameProcessController.OnChanged -= NewState;
    }
}
