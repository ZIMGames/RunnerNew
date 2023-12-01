using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        GameProcessController.OnChanged += ChangeAnim;
    }

    private void ChangeAnim(GameStates state)
    {
        switch (state)
        {
            case GameStates.lose:
                Debug.Log("DieAnim");
                _animator.SetTrigger("Die");
                break;
            case GameStates.start:
                _animator.SetTrigger("Run");
                Debug.Log("RunAnim");
                break;
            case GameStates.win:
                _animator.SetTrigger("Dance");
                Debug.Log("DanceAnim");
                break;
            case GameStates.restart:
                _animator.Rebind();
                Debug.Log("RestartAnimator");
                break;
        }
    }

    private void OnDestroy()
    {
        GameProcessController.OnChanged -= ChangeAnim;
    }
}
