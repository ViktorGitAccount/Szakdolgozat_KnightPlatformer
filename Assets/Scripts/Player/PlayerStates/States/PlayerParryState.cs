using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryState : PlayerState
{
    public PlayerParryState(Player _player, PlayerStateMachine _statemachine, string _animatorBoolName) : base(_player, _statemachine, _animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.parryDuration;
        player.animator.SetBool("PerfectParry", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {

            if (player.skill.parry.parryArrowUnlocked)
            {
                if (hit.GetComponent<Arrow>() != null)
                {
                    hit.GetComponent<Arrow>().FlippedArrow();
                    SuccessfulParry();
                }
            }


            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanBeStunned())
                    {
                        SuccessfulParry();

                        player.skill.parry.UseSkill(); // going to restore health on parry
                    }
                }
        }

        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }

    private void SuccessfulParry()
    {
        stateTimer = 2;
        player.fx.ShakeScreen();
        player.animator.SetBool("PerfectParry", true);
    }
}
