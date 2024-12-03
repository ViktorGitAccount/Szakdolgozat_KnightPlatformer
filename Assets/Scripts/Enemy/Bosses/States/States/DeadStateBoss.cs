public class DeadStateBoss : EnemyState
{
    protected Enemy_BossLvl1 enemy;

    public DeadStateBoss(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animatorBoolName, Enemy_BossLvl1 _enemy) : base(_enemyBase, _stateMachine, _animatorBoolName)
    {
        this.enemy = _enemy;
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        enemy.deadSound.Play();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();
    }
}
