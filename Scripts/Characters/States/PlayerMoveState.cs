using Godot;
using RPG.Characters.States;
using RPG.General;

public partial class PlayerMoveState : PlayerState
{
	public override State StateType => State.Move;

	[Export(PropertyHint.Range, "0,20,0.1")] private float speed = 3f;

	public override void EnterState()
	{
		base.EnterState();

		characterNode.AnimPlayerNode.Play(GameConstants.RUN_ANIM);
	}

	public override void _PhysicsProcess(double delta)
	{
		var direction2d = GetMoveInput();

		characterNode.Velocity = new(direction2d.X, 0, direction2d.Y);
		characterNode.Velocity *= speed;

		if (characterNode.Velocity == Vector3.Zero)
		{
			stateMachineNode.SwitchState(State.Idle);
			return;
		}

		characterNode.MoveAndSlide();

		Flip();
	}

	public override void _Input(InputEvent @event)
	{
		CheckForAttackState();
		CheckForDashState();
	}
}