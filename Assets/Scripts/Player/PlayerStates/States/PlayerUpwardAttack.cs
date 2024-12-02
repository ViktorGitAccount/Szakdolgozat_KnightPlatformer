using System.Collections;
using UnityEngine;

public class PlayerUpwardAttack : PlayerState
{
    private Vector3 originalAttackCheckPosition; // To store the original position of attackCheck.

    public PlayerUpwardAttack(Player _player, PlayerStateMachine _stateMachine, string _animatorBoolName)
        : base(_player, _stateMachine, _animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // Store the original attackCheck position to reset later if needed.
        originalAttackCheckPosition = player.attackCheck.localPosition; // Using localPosition for relative positioning.

        // Set attackCheck position above the player.
        // Adjust the values according to how high you want it to be.
        player.attackCheck.localPosition = new Vector3(0, 1.0f, 0); // Adjust the offset to be above player.
    }

    public override void Exit()
    {
        base.Exit();

        // Reset the attackCheck position to its original position.
        player.attackCheck.localPosition = originalAttackCheckPosition;

        // Optional: Start a coroutine to make the player busy for 0.15 seconds after the attack.
        player.StartCoroutine("BusyFor", 0.15f);
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();   

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
