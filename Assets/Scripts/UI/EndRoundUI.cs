using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndRoundUI : MonoBehaviour
{
    [SerializeField] private GameObject GamePanel, MenuPanel;
    [SerializeField] private Text roundStatus, but;
    [SerializeField] private Text currentRound;

    private Animator _animator;

    private void Start()
    {
        GameProcessController.OnChanged += NewState;
        GamePanel.SetActive(true);
        MenuPanel.SetActive(false);
        currentRound.text = GetCuurentLvl.GetLVLIndex().ToString();
        _animator = GetComponent<Animator>();
    }

    private void NewState(GameStates state)
    {
        switch (state)
        {
            case GameStates.lose:
                roundStatus.text = "GAME OVER";
                but.text = "RESTART";
                GamePanel.SetActive(false);
                MenuPanel.SetActive(true);
                _animator.SetTrigger("OpenMenu");
                break;
            case GameStates.win:
                roundStatus.text = "WIN";
                but.text = "NEXT";
                GamePanel.SetActive(false);
                MenuPanel.SetActive(true);
                _animator.SetTrigger("OpenMenu");
                break;
            case GameStates.restart:
                GamePanel.SetActive(true);
                MenuPanel.SetActive(false);
                _animator.SetTrigger("OpenGame");
                break;
            case GameStates.start:
                GamePanel.SetActive(true);
                MenuPanel.SetActive(false);
                _animator.SetTrigger("OpenGame");
                break;
        }
    }

    private void OnDestroy()
    {
        GameProcessController.OnChanged -= NewState;
    }
}
