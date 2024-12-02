using UnityEngine;

public class UndeadKnightAttackState : EnemyState
{
    private Enemy_UndeadKnight enemy;
    public UndeadKnightAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animatorBoolName, Enemy_UndeadKnight _enemy) : base(_enemyBase, _stateMachine, _animatorBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(enemy.aggroState);
    }
}
