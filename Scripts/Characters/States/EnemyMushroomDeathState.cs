using Godot;
using RPG.General;
using System;
using System.Reflection;

namespace RPG.Characters.States;

public partial class EnemyMushroomDeathState : CharacterState
{
	public override State StateType => State.Death;

	public override void EnterState()
	{
		base.EnterState();

		animPlayerNode.Play(GameConstants.DEATH_ANIM);
		animPlayerNode.AnimationFinished += HandleAnimationFinished;
	}

	private void HandleAnimationFinished(StringName animName)
	{
		GameEvents.RaiseEnemyDefeated();

		characterNode.QueueFree();
	}
}