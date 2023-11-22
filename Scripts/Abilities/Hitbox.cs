using Godot;
using RPG.Combat;

namespace RPG.Abilities;

public partial class Hitbox : Area3D, IHitbox
{
	private Ability abilityNode;

	public override void _Ready()
	{
		abilityNode = GetOwner<Ability>();
	}

	public bool CanStun() => abilityNode.CanStun;
	public float GetDamage() => abilityNode.Damage;
}
