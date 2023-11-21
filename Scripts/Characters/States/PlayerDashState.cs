using Godot;
using RPG.Characters.States;
using RPG.General;

public partial class PlayerDashState : PlayerState
{
	[Export] private PackedScene crystalScene;

	public override State StateType => State.Dash;

	[Export] private float speed = 10f;
	private bool isMoving = false;
	private Vector3 direction;
	private Timer dashTimerNode;
	private Timer cooldownTimerNode;

	public override void _Ready()
	{
		base._Ready();

		dashTimerNode = GetNode<Timer>("DashTimer");
		cooldownTimerNode = GetNode<Timer>("CooldownTimer");

		dashTimerNode.Timeout += HandleDashTimeout;

		CanTransition = () => cooldownTimerNode.IsStopped();
	}

	public override void EnterState()
	{
		base.EnterState();

		characterNode.AnimPlayerNode.Play(GameConstants.DASH_ANIM);
		dashTimerNode.Start();
	}

	public override void _PhysicsProcess(double delta)
	{
		characterNode.Velocity = direction * speed;
		characterNode.MoveAndSlide();
		Flip();
	}

	private void BeginDashMovement()
	{
		var vector2Direction = GetMoveInput();
		direction = new(vector2Direction.X, 0, vector2Direction.Y);

		if (direction == Vector3.Zero)
		{
			direction = GetFacingDirection();
		}

		// Instantiate Crystal
		var crystal = crystalScene.Instantiate<Node3D>();
		GetTree().CurrentScene.AddChild(crystal);
		crystal.GlobalPosition = characterNode.GlobalPosition;
	}

	private void HandleDashTimeout()
	{
		cooldownTimerNode.Start();

		characterNode.Velocity = Vector3.Zero;
		direction = Vector3.Zero;
		stateMachineNode.SwitchState(State.Idle);
	}
}