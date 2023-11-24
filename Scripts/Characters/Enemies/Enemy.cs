using Godot;
using RPG.Combat;
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
        if (body is not IHitbox hitbox) { return; }

        StatResource health = GetStatResource(Stat.Health);

        health.StatValue -= hitbox.GetDamage();

        ShaderMat.SetShaderParameter("active", true);
        HurtShaderTimerNode.Start();

        if (hitbox.CanStun())
        {
            RaiseStun();
        }
    }
}