using Godot;
using RPG.Characters;
using RPG.General;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RPG.Abilities;

public partial class Crystal : Node3D
{
	[Export(PropertyHint.Range, "1,10,1")] private float damage = 10;
	private Timer timerNode;
	private AnimationPlayer animationPlayerNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timerNode = GetNode<Timer>("Timer");
		animationPlayerNode = GetNode<AnimationPlayer>("AnimationPlayer");
		timerNode.Timeout += HandleTimeout;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	private void HandleTimeout()
	{
		animationPlayerNode.Play(GameConstants.EXPLOSION_ANIM);
	}

	private void Hit()
	{
		GetNode<CollisionShape3D>("Hitbox/CollisionShape3D").Disabled = false;
	}

	public float GetDamage() => damage;
	public bool CanStun() => true;
}
