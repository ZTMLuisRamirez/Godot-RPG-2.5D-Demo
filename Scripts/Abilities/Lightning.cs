using Godot;
using RPG.General;

namespace RPG.Abilities;

public partial class Lightning : Node3D
{
	[Export(PropertyHint.Range, "1,10,1")] private float damage = 10;
	private AnimationPlayer animationPlayerNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animationPlayerNode = GetNode<AnimationPlayer>("AnimationPlayer");
	}

	private void Hit()
	{
		GetNode<CollisionShape3D>("Hitbox/CollisionShape3D").Disabled = false;
	}

	public float GetDamage() => damage;
	public bool CanStun() => true;
}
