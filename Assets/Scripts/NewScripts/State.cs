using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State  
{
    protected BattleSystem BattleSystem;

    public State (BattleSystem battleSystem)
    {
        BattleSystem = battleSystem;
    }

    public virtual void Start()
    {
        return;
    }
    public virtual void Attack()
    {
        return;
    }
    public virtual void Buff()
    {
        return;
    }
}
