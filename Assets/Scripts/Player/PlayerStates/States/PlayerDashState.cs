using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _statemachine, string _animatorBoolName) : base(_player, _statemachine, _animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;

        if(player.IsGroundDetected())
            player.fx.PlayDustFx();

        player.MakeInvincible(true);

        player.jumpSound.Play();

    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);

        if (player.IsGroundDetected())
            player.fx.PlayDustFx();

        player.MakeInvincible(false);
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);



        player.SetVelocity(player.dashSpeed * player.dashDir,0);

        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);

        player.fx.CreateAfterImage();
    }
}
