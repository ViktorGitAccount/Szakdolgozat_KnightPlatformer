using UnityEngine;

public class PlayerHealState : PlayerState
{
    int healAmount;
    public PlayerHealState(Player _player, PlayerStateMachine _statemachine, string _animatorBoolName) : base(_player, _statemachine, _animatorBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();


        if (!player.skill.heal.CanUseSkill())
            stateMachine.ChangeState(player.idleState);

    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();
        player.SetZeroVelocity();

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);

    }

}
