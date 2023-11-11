using Godot;
using RPG.Characters.States;
using RPG.General;

public partial class PlayerMoveState : PlayerState
{
	public override State StateType => State.Move;

	public override void EnterState()
	{
		base.EnterState();

		animPlayerNode.Play(Constants.RUN_ANIM);
	}

	public override void _PhysicsProcess(double delta)
	{
		var direction2d = GetMoveInput();

		characterBodyNode.Velocity = new(direction2d.X, 0, direction2d.Y);

		if (characterBodyNode.Velocity == Vector3.Zero)
		{
			stateMachineNode.SwitchState(State.Idle);
			return;
		}

		characterBodyNode.MoveAndSlide();

		Flip();
	}

	public override void _Input(InputEvent @event)
	{
		CheckForAttackState();
		CheckForDashState();
	}
}
