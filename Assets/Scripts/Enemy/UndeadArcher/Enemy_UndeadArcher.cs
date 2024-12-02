using UnityEngine;

public class Enemy_UndeadArcher : Enemy
{
    [Header("Archer Stuff")]
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed;
    [SerializeField] private int arrowDamage;
    public float safeDistance; // How close player should be to meleeAttack

    Core stats;

    public UndeadArcher_IdleState idleState { get; private set; }
    public UndeadArcher_MoveState moveState { get; private set; }
    public UndeadArcher_AggroState aggroState { get; private set; }
    public UndeadArcher_AttackState attackState { get; private set; }
    public UndeadArcher_DeadState deathState { get; private set; }


    protected override void Awake()
    {
        base.Awake();

        idleState = new UndeadArcher_IdleState(this, stateMachine, "Idle", this);
        moveState = new UndeadArcher_MoveState(this, stateMachine, "Move", this);
        aggroState = new UndeadArcher_AggroState(this, stateMachine, "Idle", this);
        attackState = new UndeadArcher_AttackState(this, stateMachine, "Attack", this);
        deathState = new UndeadArcher_DeadState(this, stateMachine, "Die", this);
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


    public override void AnimationRangedAttackTrigger()
    {
        GameObject newArrow = Instantiate(arrowPrefab, attackCheck.position, Quaternion.identity);

        newArrow.GetComponent<Arrow>().SetupArrow(arrowSpeed * facingDir);
    }

    protected override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deathState);

    }
}
