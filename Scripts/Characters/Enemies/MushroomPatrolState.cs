using Godot;
using RPG.Characters.States;
using RPG.General;
using System;

namespace RPG.Characters.Enemies;

public partial class MushroomPatrolState : EnemyState
{
	public override State StateType => State.Patrol;

	[Export(PropertyHint.Range, "0,20,0.1")] private float speed = 1;
	[Export(PropertyHint.Range, "0,10,0.1")] private float maxIdleTime = 4;

	private Timer idleTimerNode;
	private Vector3[] points;
	private int pointIndex = 0;

	public override void _Ready()
	{
		base._Ready();

		idleTimerNode = GetNode<Timer>("Timer");

		points = pathNode.Curve.GetBakedPoints();
		idleTimerNode.Timeout += HandleTimeout;
	}

	public override void EnterState()
	{
		base.EnterState();

		pointIndex = 0;
		agentNode.TargetPosition = GetPointGlobalPosition();
		characterNode.AnimPlayerNode.Play(GameConstants.IDLE_ANIM);

		chaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
		agentNode.NavigationFinished += HandleNavigationFinished;
		agentNode.VelocityComputed += HandleVelocityComputed;
	}

	public override void ExitState()
	{
		base.ExitState();

		chaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
		agentNode.NavigationFinished -= HandleNavigationFinished;
		agentNode.VelocityComputed -= HandleVelocityComputed;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!idleTimerNode.IsStopped()) return;

		agentNode.Velocity = CalculateUnsafeVelocity(speed);
	}

	private Vector3 GetPointGlobalPosition()
	{
		return points[pointIndex] + pathNode.GlobalPosition;
	}

	private void HandleVelocityComputed(Vector3 safeVelocity)
	{
		characterNode.Velocity = safeVelocity;
		characterNode.MoveAndSlide();

		Flip();
	}

	private void HandleNavigationFinished()
	{
		characterNode.AnimPlayerNode.Play(GameConstants.IDLE_ANIM);

		var rng = new RandomNumberGenerator();
		idleTimerNode.WaitTime = rng.RandfRange(0, maxIdleTime);
		idleTimerNode.Start();
	}

	private void HandleTimeout()
	{
		pointIndex = Mathf.Wrap(pointIndex + 1, 0, points.Length);

		agentNode.TargetPosition = GetPointGlobalPosition();

		characterNode.AnimPlayerNode.Play(GameConstants.RUN_ANIM);
	}
}