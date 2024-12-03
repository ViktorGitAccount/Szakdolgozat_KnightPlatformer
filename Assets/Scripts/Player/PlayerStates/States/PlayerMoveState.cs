using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{

    public PlayerMoveState(Player _player, PlayerStateMachine _statemachine, string _animatorBoolName) : base(_player, _statemachine, _animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.fx.PlayDustFx();
    }

    public override void Exit()
    {
        base.Exit();

        player.fx.PlayDustFx();
    }

    public override void Update()
    {
        base.Update();
        

        if (player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);

        player.SetVelocity(xInput * player.moveSpeed, player.GetComponent<Rigidbody2D>().velocity.y);

        if (xInput == 0)
            stateMachine.ChangeState(player.idleState);
    }
}
