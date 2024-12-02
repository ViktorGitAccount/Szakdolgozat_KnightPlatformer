using UnityEngine;

public class UndeadKnightAggroState : EnemyState
{
    private Transform player;
    private Enemy_UndeadKnight enemy;
    private int moveDir;
    private bool flippedOnce;
    public UndeadKnightAggroState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animatorBoolName, Enemy_UndeadKnight _enemy) : base(_enemyBase, _stateMachine, _animatorBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = PlayerManager.instance.player.transform;

        if (player.GetComponent<Core>().isDead)
            stateMachine.ChangeState(enemy.moveState);

        stateTimer = enemy.battleTime;
        flippedOnce = false;

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

        enemy.animator.SetFloat("xVelocity", enemy.rb.velocity.x);

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
            if (flippedOnce == false)
            {
                flippedOnce = true;
                enemy.Flip();
            }

            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > enemy.disengageDistance)
                stateMachine.ChangeState(enemy.idleState);
        }

        float distanceToPlayerX = Mathf.Abs(player.position.x - enemy.transform.position.x);

        if (distanceToPlayerX < 1.2f)
        {
            enemy.SetZeroVelocity();
            return;
        }


        if (player.position.x > enemy.transform.position.x + .3f)
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x - .3f)
            moveDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
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
