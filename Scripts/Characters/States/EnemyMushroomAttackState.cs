using Godot;
using RPG.Characters.States;
using RPG.General;
using System;
using System.Linq;

public partial class EnemyMushroomAttackState : EnemyState
{
	public override State StateType => State.Attack;

	private Vector3 targetPosition;

	public override void EnterState()
	{
		base.EnterState();

		characterNode.AnimPlayerNode.Play(GameConstants.ATTACK_ANIM);
		characterNode.Velocity = Vector3.Zero;

		var target = attackAreaNode.GetOverlappingBodies()
			.Where(child => child is CharacterBody3D)
			.Cast<CharacterBody3D>()
			.FirstOrDefault();

		targetPosition = target.GlobalPosition;
		characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
		characterNode.OnStun += HandleStun;
	}

	public override void ExitState()
	{
		base.ExitState();

		characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
		characterNode.OnStun -= HandleStun;
	}

	private void PerformHit()
	{
		hitboxShapeNode.Disabled = false;
		hitboxNode.GlobalPosition = targetPosition;
	}

	private void HandleAnimationFinished(StringName animName)
	{
		hitboxShapeNode.Disabled = true;

		var target = attackAreaNode.GetOverlappingBodies()
			.Where(child => child is CharacterBody3D)
			.Cast<CharacterBody3D>()
			.FirstOrDefault();

		if (target == null)
		{
			stateMachineNode.SwitchState(State.Chase);
			return;
		}

		characterNode.AnimPlayerNode.Play(GameConstants.ATTACK_ANIM);
	}
}