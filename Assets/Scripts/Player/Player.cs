using System.Collections;
using UnityEngine;

public class Player : Core
{
    public bool isBusy;


    [Header("AudioFX")]

    public AudioSource parrySound;
    public AudioSource healSound;
    public AudioSource jumpSound;


    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float parryDuration = .2f;

    [Header("Move info")]
    public float moveSpeed = 12f;

    [Header("Jump info")]
    public float jumpForce;
    public float gravityDefaultScale = 2.5f;
    public float lowJumpMultiplier = 2f;
    [Header("Jump Buffer")]
    public bool jumpBuffered;
    private float jumpBufferTime = 0.2f;
    private float currentJumpBuffer = 0;
    [Header("Coyote Time")]
    public float coyoteDuration = 0.15f;
    public float coyoteTime;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    public SkillManager skill { get; private set; }

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerUpwardAttack upwardAttackState { get; private set; }
    public PlayerHealState healState { get; private set; }
    

    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerParryState parryState { get; private set; }

    public PlayerDeathState deathState { get; private set; }



    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        upwardAttackState = new PlayerUpwardAttack(this, stateMachine, "UpwardAttack");
        healState = new PlayerHealState(this, stateMachine, "Heal");

        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        parryState = new PlayerParryState(this, stateMachine, "Parry");

        deathState = new PlayerDeathState(this, stateMachine, "Die");

    }

    protected override void Start()
    {
        base.Start();

        skill = SkillManager.instance;

        stateMachine.Initialize(idleState);

    }

    protected override void Update()
    {
        if (Time.timeScale == 0)
            return;

        base.Update();

        stateMachine.currentState.Update();

        CheckforDashInput();

        JumpBufferCheck();

        CoyoteTimeCheck();

        if (Input.GetKeyDown(KeyCode.R))
            stateMachine.ChangeState(upwardAttackState);
    }

    private void JumpBufferCheck()
    {
        if (jumpBuffered)
        {
            currentJumpBuffer -= Time.deltaTime;
            if (currentJumpBuffer <= 0)
            {
                jumpBuffered = false;
            }
        }
    }

    public void SetupBufferJump()
    {
        jumpBuffered = true;
        currentJumpBuffer = jumpBufferTime;
    }

    private void CoyoteTimeCheck()
    {
        if (coyoteTime > 0)
        {
            coyoteTime -= Time.deltaTime;
        }
    }


    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;

    }

    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger();
    }

    private void CheckforDashInput()
    {
        if (IsWallDetected() || isDead)
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;


            stateMachine.ChangeState(dashState);
        }
    }

    protected override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deathState);

        if (IsGroundDetected())
        {
        GameManager.instance.lostCurrencyAmount = PlayerManager.instance.currency;
        PlayerManager.instance.currency = 0;

        }




    }
}
