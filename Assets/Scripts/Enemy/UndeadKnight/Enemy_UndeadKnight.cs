public class Enemy_UndeadKnight : Enemy
{


    public UndeadKnightIdleState idleState { get; private set; }
    public UndeadKnightMoveState moveState { get; private set; }
    public UndeadKnightAggroState aggroState { get; private set; }
    public UndeadKnightAttackState attackState { get; private set; }
    public UndeadKnightStunnedState stunnedState { get; private set; }
    public UndeadKnightDeathState deathState { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        idleState = new UndeadKnightIdleState(this, stateMachine, "Idle", this);
        moveState = new UndeadKnightMoveState(this, stateMachine, "Move", this);
        aggroState = new UndeadKnightAggroState(this, stateMachine, "Battle", this);
        attackState = new UndeadKnightAttackState(this, stateMachine, "Attack", this);
        stunnedState = new UndeadKnightStunnedState(this, stateMachine, "Stunned", this);
        deathState = new UndeadKnightDeathState(this, stateMachine, "Die", this);
    }


    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        disengageDistance = isPlayerDetected().distance;
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }

    protected override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deathState);

    }
}
