using Godot;
using RPG.General;

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
		characterNode.AgentNode.TargetPosition = GetPointGlobalPosition();
		characterNode.AnimPlayerNode.Play(GameConstants.IDLE_ANIM);

		characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
		characterNode.AgentNode.NavigationFinished += HandleNavigationFinished;
		characterNode.AgentNode.VelocityComputed += HandleVelocityComputed;
	}

	public override void ExitState()
	{
		base.ExitState();

		characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
		characterNode.AgentNode.NavigationFinished -= HandleNavigationFinished;
		characterNode.AgentNode.VelocityComputed -= HandleVelocityComputed;
		idleTimerNode.Stop();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (!idleTimerNode.IsStopped()) return;

		MoveWithAI(speed);
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

		characterNode.AgentNode.TargetPosition = GetPointGlobalPosition();

		characterNode.AnimPlayerNode.Play(GameConstants.RUN_ANIM);
	}
}