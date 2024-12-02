using UnityEngine;

public class UndeadArcher_AggroState : EnemyState
{
    private Transform player;
    private Enemy_UndeadArcher enemy;
    //private int moveDir;
    public UndeadArcher_AggroState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animatorBoolName, Enemy_UndeadArcher _enemy) : base(_enemyBase, _stateMachine, _animatorBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;

        enemy.moveSpeed = 6f;

    }

    public override void Exit()
    {
        base.Exit();

        enemy.moveSpeed = 2f;
    }

    public override void Update()
    {
        base.Update();

        if (!enemy.IsGroundDetected())
        {
            enemy.SetZeroVelocity();
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
            return;
        }



        if (enemy.isPlayerDetected())
        {
            stateTimer = enemy.battleTime;

            if (enemy.isPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        }
        else
        {

            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > enemy.disengageDistance)
                stateMachine.ChangeState(enemy.idleState);
        }

        float distanceToPlayerX = Mathf.Abs(player.position.x - enemy.transform.position.x);

        if (distanceToPlayerX < 1.2f)
        {
            enemy.SetZeroVelocity();
            return;
        }

        if (player.position.x > enemy.transform.position.x && enemy.facingDir == -1)
            enemy.Flip();
        else if (player.position.x < enemy.transform.position.x && enemy.facingDir == 1)
            enemy.Flip();

        //enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCoolDown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
