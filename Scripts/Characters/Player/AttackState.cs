using System.Linq;
using Godot;
using RPG.Characters.Enemies;
using RPG.General;

namespace RPG.Characters.Player;

public partial class AttackState : PlayerState
{
	public override State StateType => State.Attack;

	[Export] private PackedScene lightningScene;
	[Export] private Timer comboTimerNode;
	[Export] private int comboThreshold = 2;

	private int comboCounter = 1;

	public override void _Ready()
	{
		base._Ready();

		comboTimerNode.Timeout += () => comboCounter = 1;
	}

	public override void EnterState()
	{
		base.EnterState();

		characterNode.AnimPlayerNode.Play(
			$"{GameConstants.ATTACK_ANIM}{comboCounter}"
		);
		characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
		characterNode.HitboxNode.BodyEntered += HandleBodyEntered;

		characterNode.AnimPlayerNode.SpeedScale = 1.5f;
	}

	public override void ExitState()
	{
		base.ExitState();

		comboTimerNode.Start();

		characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
		characterNode.HitboxNode.BodyEntered -= HandleBodyEntered;

		characterNode.AnimPlayerNode.SpeedScale = 1;
	}

	private void HandleAnimationFinished(StringName animName)
	{
		characterNode.ToggleHitbox(true);

		comboCounter = Mathf.Wrap(++comboCounter, 1, comboThreshold + 1);

		stateMachineNode.SwitchState(State.Idle);
	}

	private void PerformHit()
	{
		characterNode.ToggleHitbox(false);

		Vector3 newPosition = characterNode.SpriteNode.FlipH ? Vector3.Left : Vector3.Right;
		int distanceMultiplier = 3;
		newPosition *= distanceMultiplier;

		characterNode.HitboxNode.Position = newPosition;
	}

	private void HandleBodyEntered(Node3D body)
	{
		if (comboCounter != comboThreshold) { return; }

		Node3D enemy = characterNode.HitboxNode.GetOverlappingBodies()
			.Where(child => child is Enemy)
			.FirstOrDefault();

		if (enemy == null) { return; }

		Node3D lightning = lightningScene.Instantiate<Node3D>();
		GetTree().CurrentScene.AddChild(lightning);
		lightning.GlobalPosition = body.GlobalPosition;
	}
}