using System.Text;

public class PlayerTurn : State
{
    public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
    {
        
    }
    public override void Start()
    {
        BattleSystem.buttonsLeft.SetActive(true);
        BattleSystem.buffLeft.SetActive(true);
    }

    public override void Attack()
    {
        BattleSystem.buttonsLeft.SetActive(false);
        BattleSystem.leftFighter.Attack();
        BattleSystem.SetState(new EnemyTurn(BattleSystem));

        if (BattleSystem.rightFighter.health<=0)
            BattleSystem.Restart();
    }
    public override void Buff()
    {
        BattleSystem.buffManager.Buff(BattleSystem.leftFighter);
        BattleSystem.buffLeft.SetActive(false);

        BattleSystem.leftStatus.text = BattleSystem.buffManager.BuffStatus(BattleSystem.leftFighter);
    }
}
