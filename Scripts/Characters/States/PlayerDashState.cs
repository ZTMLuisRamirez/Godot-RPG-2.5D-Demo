using Godot;
using RPG.Characters.States;
using RPG.General;

public partial class PlayerDashState : PlayerState
{
	public override State StateType => State.Dash;

	[Export] private float speed = 10f;
	private bool isMoving = false;
	private Vector3 direction;

	public override void EnterState()
	{
		base.EnterState();

		// if (ability == null)
		// {
		// 	ability = abilityController.GetAbility<DashAbility>();
		// }

		animPlayerNode.Play(Constants.DASH_ANIM);

		// StartCoroutine(ability.RunCooldown());
	}

	public override void ExitState()
	{
		base.ExitState();

		isMoving = false;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);

		if (!isMoving) return;

		var vector2Direction = GetMoveInput();
		direction = new(vector2Direction.X, 0, vector2Direction.Y);

		if (direction == Vector3.Zero)
		{
			direction = GetFacingDirection();
		}

		characterBodyNode.Velocity = direction * (float)(speed * delta);
		characterBodyNode.MoveAndSlide();
	}

	private void BeginDashMovement()
	{
		isMoving = true;
	}

	// private float newSpeed;
	// private float timer = 0;
	// private Vector3 direction;
	// private bool isReadyToMove = false;
	// private DashAbility ability;

	// private void Update()
	// {
	// 	timer += Time.deltaTime;

	// 	if (timer > ability.duration)
	// 	{
	// 		stateController.SwitchState(stateController.idleState);
	// 	}
	// }

	// private void OnDisable()
	// {
	// 	timer = 0;
	// 	animatorComp.SetBool(Constants.IS_DASHING_PARAM, false);
	// 	direction = Vector3.zero;
	// }
}
