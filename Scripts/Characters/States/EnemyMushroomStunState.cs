using Godot;
using RPG.Characters.States;
using RPG.General;
using System;

public partial class EnemyMushroomStunState : EnemyState
{

	public override State StateType => State.Stun;

	public override void EnterState()
	{
		base.EnterState();

		animPlayerNode.Play(GameConstants.TAKE_HIT_ANIM);
		animPlayerNode.AnimationFinished += HandleAnimationFinished;
	}

	public override void ExitState()
	{
		base.ExitState();

		animPlayerNode.AnimationFinished -= HandleAnimationFinished;
	}

	private void HandleAnimationFinished(StringName animName)
	{
		stateMachineNode.SwitchState(State.Idle);
	}
}
