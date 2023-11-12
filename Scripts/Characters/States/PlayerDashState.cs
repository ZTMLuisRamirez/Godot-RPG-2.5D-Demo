using Godot;
using RPG.Characters.States;
using RPG.General;

public partial class PlayerDashState : PlayerState
{
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

		// if (ability == null)
		// {
		// 	ability = abilityController.GetAbility<DashAbility>();
		// }

		animPlayerNode.Play(Constants.DASH_ANIM);
		dashTimerNode.Start();
	}

	public override void _PhysicsProcess(double delta)
	{
		characterBodyNode.Velocity = direction * speed;
		characterBodyNode.MoveAndSlide();
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
	}

	private void HandleDashTimeout()
	{
		cooldownTimerNode.Start();

		characterBodyNode.Velocity = Vector3.Zero;
		direction = Vector3.Zero;
		stateMachineNode.SwitchState(State.Idle);
	}
}
