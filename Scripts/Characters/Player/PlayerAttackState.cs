using System.Linq;
using Godot;
using RPG.Characters.States;
using RPG.General;

namespace RPG.Characters.Player;

public partial class PlayerAttackState : PlayerState
{
	public override State StateType => State.Attack;

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
		characterNode.Velocity = direction * (float)(delta * characterNode.MoveDistance);
		characterNode.MoveAndSlide();
	}

	public override void EnterState()
	{
		base.EnterState();

		characterNode.AnimPlayerNode.Play($"{GameConstants.ATTACK_ANIM}{comboCounter}");

		characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
		characterNode.HitboxNode.BodyEntered += HandleBodyEntered;
	}

	public override void ExitState()
	{
		base.ExitState();

		comboTimerNode.Start();

		characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
		characterNode.HitboxNode.BodyEntered -= HandleBodyEntered;
	}

	private void HandleAnimationFinished(StringName animName)
	{
		characterNode.ToggleHitbox(true);
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
		characterNode.ToggleHitbox(false);

		var newPosition = characterNode.SpriteNode.FlipH ? Vector3.Left : Vector3.Right;
		var halfMultiplier = 0.5f;
		newPosition *= halfMultiplier;

		characterNode.HitboxNode.Position = newPosition;
	}

	private void HandleBodyEntered(Node3D body)
	{
		if (comboCounter != characterNode.lightningComboThreshold) return;

		var enemy = characterNode.HitboxNode.GetOverlappingBodies()
			.Where(child => child is CharacterBody3D)
			.Cast<CharacterBody3D>()
			.FirstOrDefault();

		if (enemy == null) return;

		// Instantiate Crystal
		var lightning = characterNode.lightningScene.Instantiate<Node3D>();
		GetTree().CurrentScene.AddChild(lightning);
		lightning.GlobalPosition = body.GlobalPosition;
	}
}