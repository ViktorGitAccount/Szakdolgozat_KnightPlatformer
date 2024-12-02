public class UndeadArcher_MoveState : UndeadArcher_GroundedState
{
    public UndeadArcher_MoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animatorBoolName, Enemy_UndeadArcher _enemy) : base(_enemyBase, _stateMachine, _animatorBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();



        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
