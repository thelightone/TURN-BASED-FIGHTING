using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRound : State
{
    public NewRound(BattleSystem battleSystem) : base(battleSystem)
    {

    }

    public override void Start()
    {
        BattleSystem.buffManager.BuffDurationDecrease();

        BattleSystem.round++;
        BattleSystem._scoreCounter.text = BattleSystem.round.ToString();

        BattleSystem.SetState(new PlayerTurn(BattleSystem));

        BattleSystem.leftStatus.text = BattleSystem.buffManager.BuffStatus(BattleSystem.leftFighter);
        BattleSystem.rightStatus.text = BattleSystem.buffManager.BuffStatus(BattleSystem.rightFighter);
    }

}

