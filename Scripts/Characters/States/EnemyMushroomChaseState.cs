using System;
using System.Linq;
using Godot;
using RPG.Characters.States;
using RPG.General;

public partial class EnemyMushroomChaseState : EnemyState
{
	public override State StateType => State.Chase;
	private CharacterBody3D player;

	[Export(PropertyHint.Range, "0,20,0.1")] private float speed = 1;

	public override void EnterState()
	{
		base.EnterState();

		characterNode.AnimPlayerNode.Play(GameConstants.RUN_ANIM);
		player = chaseAreaNode.GetOverlappingBodies()
			.Where(child => child is CharacterBody3D)
			.Cast<CharacterBody3D>()
			.FirstOrDefault();

		if (player == null)
		{
			stateMachineNode.SwitchState(State.Return);
			return;
		}

		agentNode.TargetPosition = player.GlobalPosition;

		attackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
		chaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
		agentNode.VelocityComputed += HandleVelocityComputed;
		characterNode.OnStun += HandleStun;
	}

	public override void ExitState()
	{
		base.ExitState();

		attackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
		chaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
		agentNode.VelocityComputed -= HandleVelocityComputed;
		characterNode.OnStun -= HandleStun;
	}

	public override void _PhysicsProcess(double delta)
	{
		agentNode.TargetPosition = player.GlobalPosition;

		agentNode.Velocity = CalculateUnsafeVelocity(speed);
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
}