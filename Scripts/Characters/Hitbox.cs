using Godot;
using RPG.Characters;
using RPG.Stats;
using System;

namespace RPG.Characters;

public partial class Hitbox : Area3D, IHitbox
{
	public float GetDamage()
	{
		return GetOwner<Character>().GetStatResource(Stat.Strength).StatValue;
	}

	public bool CanStun() => false;
}