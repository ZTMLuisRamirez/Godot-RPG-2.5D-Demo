using System;
using System.Linq;
using Godot;
using RPG.Stats;

namespace RPG.Characters;

public partial class Enemy : Character
{
    protected Area3D areaHurtbox;

    public override void _Ready()
    {
        base._Ready();

        areaHurtbox = GetNode<Area3D>("Hurtbox");

        areaHurtbox.AreaEntered += HandleBodyEntered;
    }

    private void HandleBodyEntered(Node3D body)
    {
        if (body is not IHitbox hitbox) return;

        var health = stats.Where(child => child.StatType == Stat.Health)
            .FirstOrDefault();

        health.StatValue -= hitbox.GetDamage();

        if (hitbox.CanStun())
        {
            RaiseStun();
        }
    }
}