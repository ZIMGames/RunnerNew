using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameProcessController : MonoBehaviour
{
    public delegate void GameStatus(GameStates newStatus);
    public static event GameStatus OnChanged;

    [SerializeField] private GameObject[] LvlPrefab;
    private bool winRound;
    private GameObject lvlObj;
    private int lvlIndex;
    private GameStates state;

    private void Awake()
    {
        PrepareRound();
    }

    private void Start()
    {
        state = GameStates.wait;
        OnChanged?.Invoke(state);
        PlayerCollision.OnEndRound += EndRound;
    }

    private void Update()
    {
        if ((state == GameStates.wait) && Input.GetMouseButtonDown(0))
        {
            state = GameStates.start;
            OnChanged?.Invoke(state);
        }
    }

    private void PrepareRound()
    {
        lvlIndex = GetCuurentLvl.GetLVLIndex();
        if (lvlIndex > LvlPrefab.Length)
        {
            GetCuurentLvl.ChangeLVLIndex(1);
            lvlIndex = 1;
        }

        lvlObj = Instantiate(LvlPrefab[lvlIndex - 1]) as GameObject;
    }

    private void RestartRound()
    {
        state = GameStates.restart;
        OnChanged?.Invoke(state);
        state = GameStates.wait;
        OnChanged?.Invoke(state);
    }

    private void EndRound(bool win)
    {
        winRound = win;
        state = GameStates.wait;
        OnChanged?.Invoke(state);

        if (winRound)
        {
            state = GameStates.win;
            GetCuurentLvl.ChangeLVLIndex(++lvlIndex);
        }
        else
        {
            state = GameStates.lose;
        }
        OnChanged?.Invoke(state);
    }

    public void ButtonEventHandler()
    {
        if (winRound)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        } else
        {
            RestartRound();
        }
    }

    private void OnDestroy()
    {
        PlayerCollision.OnEndRound -= EndRound;
    }
}

[Serializable]
public enum GameStates { wait = 0, start, restart, lose, win}
