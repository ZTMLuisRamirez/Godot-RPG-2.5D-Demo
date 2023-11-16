using Godot;
using RPG.General;

namespace RPG.Characters.States;

public partial class PlayerAttackState : PlayerState
{
	public override State StateType => State.Attack;

	[Export(PropertyHint.Range, "0,20,0.1")] private float moveDistance = 5;
	[Export(PropertyHint.Range, "0,3,0.1")] private float attackDistance = 2f;
	[Export(PropertyHint.Range, "0,3,0.1")] private float attackZone = 0.8f;

	private Timer comboTimerNode;
	private int comboCounter = 1;
	private float lastAttackTime;

	public override void _Ready()
	{
		base._Ready();

		comboTimerNode = GetNode<Timer>("ComboTimer");
		comboTimerNode.Timeout += HandleTimeout;
	}

	public override void _PhysicsProcess(double delta)
	{
		var direction = GetFacingDirection();
		characterNode.Velocity = direction * (float)(delta * moveDistance);
		characterNode.MoveAndSlide();
	}

	public override void EnterState()
	{
		base.EnterState();

		animPlayerNode.Play($"{GameConstants.ATTACK_ANIM}{comboCounter}");

		animPlayerNode.AnimationFinished += HandleAnimationFinished;
	}

	public override void ExitState()
	{
		base.ExitState();

		comboTimerNode.Start();

		animPlayerNode.AnimationFinished -= HandleAnimationFinished;
	}

	private void HandleAnimationFinished(StringName animName)
	{
		hitboxShapeNode.Disabled = true;
		comboCounter++;

		if (comboCounter > 4)
		{
			comboCounter = 1;
		}

		stateMachineNode.SwitchState(State.Idle);
	}

	private void HandleTimeout()
	{
		comboCounter = 1;
	}

	private void PerformHit()
	{
		hitboxShapeNode.Disabled = false;

		var newPosition = sprite3DNode.FlipH ? Vector3.Left : Vector3.Right;
		var halfMultiplier = 0.5f;
		newPosition *= halfMultiplier;

		hitboxNode.Position = newPosition;
	}
}