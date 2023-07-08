using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurn : State
{ 
    public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
    {

    }

    public override void Start()
    {
        BattleSystem.buttonsRight.SetActive(true);
        BattleSystem.buffRight.SetActive(true);
    }

    public override void Attack()
    {
        BattleSystem.buttonsRight.SetActive(false);
        BattleSystem.rightFighter.Attack();

        if (BattleSystem.leftFighter.health <= 0)
            BattleSystem.Restart();

        BattleSystem.SetState(new NewRound(BattleSystem));
    }
    public override void Buff()
    {
        BattleSystem.buffManager.Buff(BattleSystem.rightFighter);
        BattleSystem.buffRight.SetActive(false);

        BattleSystem.rightStatus.text = BattleSystem.buffManager.BuffStatus(BattleSystem.rightFighter);
    }
}
