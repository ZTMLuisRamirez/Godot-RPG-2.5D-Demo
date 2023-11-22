using Godot;
using RPG.Characters.States;
using RPG.General;

namespace RPG.Characters.Player;

public partial class MoveState : PlayerState
{
	[Export(PropertyHint.Range, "0,20,0.1")] private float speed = 3f;

	public override State StateType => State.Move;

	public override void EnterState()
	{
		base.EnterState();

		characterNode.AnimPlayerNode.Play(GameConstants.RUN_ANIM);
	}

	public override void _PhysicsProcess(double delta)
	{
		var direction = GetMoveInput();

		characterNode.Velocity = new(direction.X, 0, direction.Y);
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
		CheckForAttackInput();
		CheckForDashInput();
	}
}