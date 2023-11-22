using Godot;
using RPG.Stats;

namespace RPG.Characters.Enemies;

public partial class Enemy : Character
{
    public override void _Ready()
    {
        base._Ready();

        HurtboxNode.AreaEntered += HandleBodyEntered;
    }

    private void HandleBodyEntered(Node3D body)
    {
        if (body is not IHitbox hitbox) return;

        var health = GetStatResource(Stat.Health);

        health.StatValue -= hitbox.GetDamage();

        if (hitbox.CanStun())
        {
            RaiseStun();
        }
    }
}