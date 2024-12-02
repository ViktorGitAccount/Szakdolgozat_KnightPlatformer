public class UndeadArcher_IdleState : UndeadArcher_GroundedState
{
    public UndeadArcher_IdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animatorBoolName, Enemy_UndeadArcher _enemy) : base(_enemyBase, _stateMachine, _animatorBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.moveState);
    }
}
