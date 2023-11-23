using System;
using Godot;
using RPG.General;
using RPG.Utilities;

namespace RPG.Characters.Enemies;

public partial class MushroomChaseState : EnemyState
{
	public override State StateType => State.Chase;

	[Export(PropertyHint.Range, "0,20,0.1")] private float speed = 1;
	[Export] private Timer timerNode;

	private CharacterBody3D target;

	public override void EnterState()
	{
		base.EnterState();

		characterNode.AnimPlayerNode.Play(GameConstants.RUN_ANIM);

		characterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
		characterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
		characterNode.AgentNode.VelocityComputed += HandleVelocityComputed;
		timerNode.Timeout += HandleTimeout;

		target = characterNode.ChaseAreaNode.GetFirstTarget();
	}

	public override void ExitState()
	{
		base.ExitState();

		characterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
		characterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
		characterNode.AgentNode.VelocityComputed -= HandleVelocityComputed;
		timerNode.Timeout -= HandleTimeout;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (target == null)
		{
			stateMachineNode.SwitchState(State.Return);
			return;
		}

		if (characterNode.AttackAreaNode.HasOverlappingBodies())
		{
			stateMachineNode.SwitchState(State.Attack);
			return;
		}

		MoveWithAI(speed);
	}

	private void HandleVelocityComputed(Vector3 safeVelocity)
	{
		characterNode.Velocity = safeVelocity;
		characterNode.MoveAndSlide();

		Flip();
	}

	private void HandleChaseAreaBodyExited(Node3D body)
	{
		stateMachineNode.SwitchState(State.Return);
	}

	private void HandleAttackAreaBodyEntered(Node3D body)
	{
		stateMachineNode.SwitchState(State.Attack);
	}

	private void HandleTimeout()
	{
		characterNode.AgentNode.TargetPosition = target.GlobalPosition;
	}
}