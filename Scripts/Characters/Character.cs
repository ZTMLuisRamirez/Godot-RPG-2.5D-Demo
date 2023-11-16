using System;
using Godot;
using RPG.Stats;
using System.Linq;

namespace RPG.Characters;

public abstract partial class Character : CharacterBody3D
{
    [Export] protected StatResource[] stats;

    public event Action OnDeath = () => { };

    protected Area3D areaHurtbox;

    public override void _Ready()
    {
        areaHurtbox = GetNode<Area3D>("Hurtbox");

        areaHurtbox.AreaEntered += HandleBodyEntered;
    }

    private void HandleBodyEntered(Node3D body)
    {
        if (body is not IHitbox hitbox) return;

        var health = stats.Where(child => child.StatType == Stat.Health)
            .FirstOrDefault();

        health.StatValue -= hitbox.GetDamage();
    }

    public StatResource GetStatResource(Stat stat)
    {
        return stats.Where(child => child.StatType == stat)
            .FirstOrDefault();
    }
}