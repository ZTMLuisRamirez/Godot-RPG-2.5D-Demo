using Godot;
using RPG.Characters.States;
using RPG.General;
using System;

namespace RPG.Characters.Enemies;

public partial class MushroomIdleState : EnemyState
{
	public override State StateType => State.Idle;

	public override void EnterState()
	{
		base.EnterState();

		characterNode.AnimPlayerNode.Play(GameConstants.IDLE_ANIM);
		chaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
	}

	public override void ExitState()
	{
		base.ExitState();

		chaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
	}

	public override void _Process(double delta)
	{
		if (characterNode.GlobalPosition.DistanceTo(initialPathPosition) > 1.2f)
		{
			stateMachineNode.SwitchState(State.Return);
			return;
		}
	}
}