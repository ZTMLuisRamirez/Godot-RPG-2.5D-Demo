using System;
using Godot;
using RPG.General;
using RPG.Utilities;

namespace RPG.Characters.Enemies;

public partial class MushroomAttackState : EnemyState
{
	public override State StateType => State.Attack;

	private Vector3 targetPosition;

	public override void EnterState()
	{
		base.EnterState();

		characterNode.AnimPlayerNode.Play(GameConstants.ATTACK_ANIM);
		characterNode.Velocity = Vector3.Zero;

		CharacterBody3D target = characterNode.AttackAreaNode
			.GetFirstTarget();

		targetPosition = target.GlobalPosition;
		characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;

		FaceTarget();
	}

	public override void ExitState()
	{
		base.ExitState();

		characterNode.ToggleHitbox(true);

		characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
	}

	private void PerformHit()
	{
		characterNode.ToggleHitbox(false);
		characterNode.HitboxNode.GlobalPosition = targetPosition;
	}

	private void FaceTarget()
	{
		Vector3 offset = targetPosition - characterNode.GlobalPosition;

		characterNode.SpriteNode.FlipH = offset.X < 0;
	}

	private void HandleAnimationFinished(StringName animName)
	{
		characterNode.ToggleHitbox(true);

		CharacterBody3D target = characterNode.AttackAreaNode
			.GetFirstTarget();

		if (target == null)
		{
			stateMachineNode.SwitchState(State.Chase);
			return;
		}

		characterNode.AnimPlayerNode.Play(GameConstants.ATTACK_ANIM);

		// Possible Solution
		targetPosition = target.GlobalPosition;
		FaceTarget();
	}
}