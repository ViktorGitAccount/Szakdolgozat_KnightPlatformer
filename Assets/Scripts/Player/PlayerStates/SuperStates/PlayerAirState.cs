using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _statemachine, string _animatorBoolName) : base(_player, _statemachine, _animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        player.rb.gravityScale = 2.5f;
    }

    public override void Update()
    {
        base.Update();

        player.rb.gravityScale = 6f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player.coyoteTime > 0)
            {
                stateMachine.ChangeState(player.jumpState);
                player.coyoteTime = 0;
            }
            else
                player.SetupBufferJump();
        }


        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);

        if (player.IsGroundDetected())
        {
            if (player.jumpBuffered)
            {
                player.jumpBuffered = false;
                stateMachine.ChangeState(player.jumpState);
            }
            else
            {
                player.fx.PlayDustFx();
                stateMachine.ChangeState(player.idleState);
            }
                
        }


        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);
    }

}
