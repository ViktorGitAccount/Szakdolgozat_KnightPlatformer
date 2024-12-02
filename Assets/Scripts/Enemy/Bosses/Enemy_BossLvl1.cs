using System.Collections;
using UnityEngine;

public class Enemy_BossLvl1 : Enemy
{

    public IdleStateBoss idleState { get; private set; }
    public MoveStateBoss moveState { get; private set; }
    public AttackStateBoss attackState { get; private set; }
    public DeadStateBoss deadState { get; private set; }
    public BattleStateBoss battleState { get; private set; }

    public float arenaDelay = 5f;

    [Header("Arena")]
    public GameObject wall1;
    public GameObject wall2;

    [Header("Phase Two")]
    public GameObject arrowsContainer;
    private bool phase2Trigger = false;



    protected override void Awake()
    {
        base.Awake();

        idleState = new IdleStateBoss(this, stateMachine, "IsIdle", this);
        moveState = new MoveStateBoss(this, stateMachine, "IsMoving", this);
        attackState = new AttackStateBoss(this, stateMachine, "IsAttacking", this);
        deadState = new DeadStateBoss(this, stateMachine, "IsDead", this);
        battleState = new BattleStateBoss(this, stateMachine, "IsBattle", this);
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

        if (!phase2Trigger && currentHeatlh <= GetMaxHealthValue() / 2)
        {
            TriggerPhase2();
        }
    }

    private void StopAllArrows()
    {
        foreach (Transform arrow in arrowsContainer.transform)
        {
            FallingArrow fallingArrow = arrow.GetComponent<FallingArrow>();
            if (fallingArrow != null)
            {
                fallingArrow.CancelInvoke();
            }
        }
        arrowsContainer.SetActive(false);
    }

    private void TriggerPhase2()
    {
        phase2Trigger = true;

        TriggerAllArrows();
    }

    private void TriggerAllArrows()
    {
        foreach (Transform arrow in arrowsContainer.transform)
        {
            FallingArrow fallingArrow = arrow.GetComponent<FallingArrow>();
            if (fallingArrow != null)
            {
                fallingArrow.EnableFalling();
            }
        }
    }

    protected override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);

        StopAllArrows();

        StartCoroutine(DeactivateWalLDelayed());
    }

    private IEnumerator DeactivateWalLDelayed()
    {
        yield return new WaitForSeconds(arenaDelay);

        if (wall1 != null) wall1.SetActive(false);
        if (wall2 != null) wall2.SetActive(false);
    }
}
