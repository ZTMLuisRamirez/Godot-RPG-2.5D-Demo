using Godot;
using RPG.Characters;
using RPG.Stats;

namespace RPG.Combat;

public partial class Hitbox : Area3D, IHitbox
{
	public float GetDamage()
	{
		return GetOwner<Character>().GetStatResource(Stat.Strength).StatValue;
	}

	public bool CanStun() => false;
}