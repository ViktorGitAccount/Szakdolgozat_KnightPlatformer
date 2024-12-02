using UnityEngine;

public class UndeadKnightGroundedState : EnemyState
{
    protected Enemy_UndeadKnight enemy;
    protected Transform player;

    public UndeadKnightGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animatorBoolName, Enemy_UndeadKnight _enemy) : base(_enemyBase, _stateMachine, _animatorBoolName)
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
            stateMachine.ChangeState(enemy.aggroState);
    }
}
