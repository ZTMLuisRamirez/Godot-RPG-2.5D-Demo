using Godot;
using RPG.General;

namespace RPG.Characters.Player;

public partial class DashState : PlayerState
{
	public override State StateType => State.Dash;

	[Export] private PackedScene crystalScene;
	[Export(PropertyHint.Range, "0,20,0.1")] private float speed = 10f;
	[Export] private Timer dashTimerNode;
	[Export] private Timer cooldownTimerNode;

	private bool isMoving = false;
	private Vector3 direction;

	public override void _Ready()
	{
		base._Ready();

		dashTimerNode.Timeout += HandleDashTimeout;

		CanTransition = () => cooldownTimerNode.IsStopped();
	}

	public override void EnterState()
	{
		base.EnterState();

		characterNode.AnimPlayerNode.Play(GameConstants.DASH_ANIM);

		Vector2 vector2Direction = GetMoveInput();
		direction = new(vector2Direction.X, 0, vector2Direction.Y);

		if (direction == Vector3.Zero)
		{
			direction = GetFacingDirection();
		}

		// Instantiate Crystal
		Node3D crystal = crystalScene.Instantiate<Node3D>();
		GetTree().CurrentScene.AddChild(crystal);
		crystal.GlobalPosition = characterNode.GlobalPosition;

		dashTimerNode.Start();
	}

	public override void _PhysicsProcess(double delta)
	{
		characterNode.Velocity = direction * speed;
		characterNode.MoveAndSlide();
		Flip();
	}

	private void HandleDashTimeout()
	{
		cooldownTimerNode.Start();

		characterNode.Velocity = Vector3.Zero;
		direction = Vector3.Zero;
		stateMachineNode.SwitchState(State.Idle);
	}
}