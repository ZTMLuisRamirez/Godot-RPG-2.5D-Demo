using Godot;
using RPG.Characters.States;
using RPG.General;
using System;

namespace RPG.Characters.Enemies;

public partial class MushroomStunState : EnemyState
{

	public override State StateType => State.Stun;

	public override void EnterState()
	{
		base.EnterState();

		characterNode.AnimPlayerNode.Play(GameConstants.TAKE_HIT_ANIM);
		characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
	}

	public override void ExitState()
	{
		base.ExitState();

		characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
	}

	private void HandleAnimationFinished(StringName animName)
	{
		stateMachineNode.SwitchState(State.Idle);
	}
}
