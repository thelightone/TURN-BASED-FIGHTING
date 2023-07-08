using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSystem : StateMachine
{
    public BuffManager buffManager;
    public PlayerController leftFighter;
    public PlayerController rightFighter;
    public GameObject buttonsLeft;
    public GameObject buttonsRight;
    public GameObject buffLeft;
    public GameObject buffRight;
    public TMP_Text leftStatus;
    public TMP_Text rightStatus;
    public TMP_Text _scoreCounter;
    public float round=1;

    public void Start()
    {
        SetState(new PlayerTurn(this));
    }

    public void OnAttack()
    {
        State.Attack();
    }

    public void OnBuff()
    {
        State.Buff();
    }
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
