using Godot;
using RPG.Combat;
using RPG.General;
using RPG.Stats;

namespace RPG.Characters.Player;

public partial class Player : Character
{
    public float speed;

    public override void _Ready()
    {
        base._Ready();

        HurtboxNode.AreaEntered += HandleBodyEntered;
        GameEvents.OnBonus += HandleBonus;
    }

    private void HandleBodyEntered(Node3D body)
    {
        if (body is not IHitbox hitbox) { return; }

        StatResource health = GetStatResource(Stat.Health);

        health.StatValue -= hitbox.GetDamage();

        ShaderMat.SetShaderParameter("active", true);
        HurtShaderTimerNode.Start();
    }

    private void HandleBonus(BonusResource resource)
    {
        StatResource targetStat = GetStatResource(resource.StatType);

        targetStat.StatValue += resource.Amount;
    }
}