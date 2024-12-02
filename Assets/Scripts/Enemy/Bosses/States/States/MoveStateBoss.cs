public class MoveStateBoss : GroundedStateBoss
{
    public MoveStateBoss(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animatorBoolName, Enemy_BossLvl1 _enemy) : base(_enemyBase, _stateMachine, _animatorBoolName, _enemy)
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

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

        if (enemy.usePatrolPoints)
        {
            PatrolBetweenPoints();
        }
        else
        {
            PatrolWithEdgeDetection();
        }
    }

    private void PatrolBetweenPoints()
    {
        if (enemy.facingDir == 1 && enemy.transform.position.x >= enemy.patrolPointB.x)
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
        else if (enemy.facingDir == -1 && enemy.transform.position.x <= enemy.patrolPointA.x)
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    private void PatrolWithEdgeDetection()
    {
        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
