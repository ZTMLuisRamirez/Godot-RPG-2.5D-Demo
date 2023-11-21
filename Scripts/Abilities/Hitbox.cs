using Godot;
using RPG.Characters;
using System;

public partial class Hitbox : Area3D, IHitbox
{
	public bool CanStun()
	{
		return true;
	}

	public float GetDamage()
	{
		return 5;
	}
}
