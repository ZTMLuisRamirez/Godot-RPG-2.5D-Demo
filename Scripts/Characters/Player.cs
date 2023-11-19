using System;
using System.Linq;
using Godot;
using RPG.General;
using RPG.Stats;

namespace RPG.Characters;

public partial class Player : Character
{
    protected Area3D areaHurtbox;

    public override void _Ready()
    {
        areaHurtbox = GetNode<Area3D>("Hurtbox");

        areaHurtbox.AreaEntered += HandleBodyEntered;
        GameEvents.OnBonus += HandleBonus;
    }
    private void HandleBodyEntered(Node3D body)
    {
        if (body is not IHitbox hitbox) return;

        var health = GetStatResource(Stat.Health);

        health.StatValue -= hitbox.GetDamage();
    }

    private void HandleBonus(BonusResource resource)
    {
        var targetStat = GetStatResource(resource.StatType);

        targetStat.StatValue += resource.Amount;
    }

}