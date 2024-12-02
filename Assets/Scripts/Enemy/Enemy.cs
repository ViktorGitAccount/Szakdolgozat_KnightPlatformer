using UnityEngine;

public class Enemy : Core
{
    [SerializeField] protected LayerMask whatIsPlayer;
    [Header("Move info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;
    public float disengageDistance;

    [Header("Stun info")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool canBeStunned;

    [Header("Attack info")]
    public float attackDistance;
    public float attackCoolDown;
    [HideInInspector] public float lastTimeAttacked;

    [Header("Patrol Settings")]
    public Vector2 patrolPointA;
    public Vector2 patrolPointB;
    public bool usePatrolPoints;


    private Enemy enemy;
    private ItemDrop myDropSystem;
    public Stat currencyDropAmount;


    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();

        enemy = GetComponent<Enemy>();
        myDropSystem = GetComponent<ItemDrop>();
    }

    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

    }


    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

    }

    protected override void Die()
    {
        base.Die();

        PlayerManager.instance.currency += currencyDropAmount.GetValue();

        myDropSystem.GenerateDrop();

        Destroy(gameObject, 10f);
    }


    public virtual void OpencounterAttackWindow()
    {
        canBeStunned = true;
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
    }

    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    public virtual void AnimationRangedAttackTrigger()
    {

    }
    public virtual RaycastHit2D isPlayerDetected()
    {
        RaycastHit2D playerDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);
        RaycastHit2D wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsGround);

        if (wallDetected)
        {
            if (wallDetected.distance < playerDetected.distance)
                return default(RaycastHit2D);
        }

        return playerDetected;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));

        if (usePatrolPoints)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawSphere(patrolPointA, 0.2f);
            Gizmos.DrawSphere(patrolPointB, 0.2f);

            Gizmos.color = Color.red;

            Gizmos.DrawLine(patrolPointA, patrolPointB);
        }

    }
}
