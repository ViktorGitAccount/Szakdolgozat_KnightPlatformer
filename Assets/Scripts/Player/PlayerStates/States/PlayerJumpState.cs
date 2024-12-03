using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private bool isJumping;

    public PlayerJumpState(Player _player, PlayerStateMachine _statemachine, string _animatorBoolName) : base(_player, _statemachine, _animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        isJumping = true;

        player.jumpSound.Play(); 
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

            HandleJump();

            if (rb.velocity.y < 0)
            {
                stateMachine.ChangeState(player.airState);
            }

    }

    public void HandleJump()
    {


        if (Input.GetKeyUp(KeyCode.Space) && isJumping)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
            isJumping = false;
            
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += (player.gravityDefaultScale - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += (player.lowJumpMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime * Vector2.up;
        }
    }
}
