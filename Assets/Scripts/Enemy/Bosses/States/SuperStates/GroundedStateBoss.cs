using UnityEngine;

public class GroundedStateBoss : EnemyState
{

    protected Enemy_BossLvl1 enemy;
    protected Transform player;

    public GroundedStateBoss(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animatorBoolName, Enemy_BossLvl1 _enemy) : base(_enemyBase, _stateMachine, _animatorBoolName)
    {
        this.enemy = _enemy;

    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.isPlayerDetected() || Vector2.Distance(enemy.transform.position, player.position) < 2)
            stateMachine.ChangeState(enemy.battleState);
    }
}
