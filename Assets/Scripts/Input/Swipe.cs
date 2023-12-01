using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class SwipeMovement : MonoBehaviour
{
    public delegate void MovePlayer(SwipeDirection newDir);
    public static event MovePlayer OnMove;

    private Vector3 startTouchPosition;
    public float swipeThreshold = 50f;
    private bool isSwiping;
    private bool isPaused;

    private void Start()
    {
        GameProcessController.OnChanged += NewState;
    }

    private void NewState(GameStates state)
    {
        if (state == GameStates.lose || state == GameStates.win)
        {
            isPaused = true;
        }
        else
        {
            isPaused = false;
        }
    }

    private void Update()
    {
        CheckSwipe();
    }

    private void CheckSwipe()
    {
        if (Input.GetMouseButtonDown(0) && !IsMouseOverUI())
        {
            isSwiping = true;
            startTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            if (isSwiping && !isPaused)
            {
                Vector2 currentTouchPosition = Input.mousePosition;
                float swipeDistance = currentTouchPosition.x - startTouchPosition.x;

                if (Mathf.Abs(swipeDistance) >= swipeThreshold)
                {
                    isSwiping = false;

                    if (swipeDistance < 0)
                    {
                        OnMove?.Invoke(SwipeDirection.left);
                    }
                    else
                    {
                        OnMove?.Invoke(SwipeDirection.right);
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;
        }
    }

    private bool IsMouseOverUI()
    {

        if (EventSystem.current == null || Input.mousePosition == null)
        {
            return false;
        }

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition; ;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject.GetComponent<MouseUIClickthrough>() != null)
            {
                raycastResultList.RemoveAt(i);
                i--;
            }
        }

        return raycastResultList.Count > 0;
    }

    private void OnDestroy()
    {
        GameProcessController.OnChanged -= NewState;
    }
}

[Serializable]
public enum SwipeDirection { left = 0, right}
