using Godot;
using RPG.Characters.States;
using RPG.General;
using System;

public partial class EnemyMushroomPatrolState : EnemyState
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
		agentNode.NavigationFinished += HandleNavigationFinished;
		agentNode.VelocityComputed += HandleVelocityComputed;
		idleTimerNode.Timeout += HandleTimeout;
	}

	public override void EnterState()
	{
		base.EnterState();

		agentNode.TargetPosition = GetPointGlobalPosition();
		animPlayerNode.Play(GameConstants.IDLE_ANIM);
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
		characterBodyNode.Velocity = safeVelocity;
		characterBodyNode.MoveAndSlide();

		Flip();
	}

	private void HandleNavigationFinished()
	{
		animPlayerNode.Play(GameConstants.IDLE_ANIM);

		var rng = new RandomNumberGenerator();
		idleTimerNode.WaitTime = rng.RandfRange(0, maxIdleTime);
		idleTimerNode.Start();
	}

	private void HandleTimeout()
	{
		pointIndex = Mathf.Wrap(pointIndex + 1, 0, points.Length);

		agentNode.TargetPosition = GetPointGlobalPosition();

		animPlayerNode.Play(GameConstants.RUN_ANIM);
	}
}
