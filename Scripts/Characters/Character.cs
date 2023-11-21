using System;
using Godot;
using RPG.Stats;
using System.Linq;

namespace RPG.Characters;

public abstract partial class Character : CharacterBody3D
{
    [Export] public StatResource[] stats;

    [ExportGroup("Nodes")]
    [Export] public Area3D HurtboxNode { get; private set; }
    [Export] public Sprite3D SpriteNode { get; private set; }
    [Export] public AnimationPlayer AnimPlayerNode { get; private set; }
    [Export] public Area3D HitboxNode { get; private set; }

    [ExportGroup("Move State Settings")]
    [Export(PropertyHint.Range, "0,20,0.1")]
    public float MoveSpeed { get; private set; } = 3f;

    [ExportGroup("Dash State Settings")]
    [Export] public PackedScene CrystalScene { get; private set; }
    [Export(PropertyHint.Range, "0,20,0.1")]
    public float DashSpeed { get; private set; } = 10f;
    [Export(PropertyHint.Range, "0,20,0.1")]
    public float DashDuration { get; private set; } = 10f;
    [Export(PropertyHint.Range, "0,20,0.1")]
    public float DashCooldownDuration { get; private set; } = 10f;

    [ExportGroup("Attack State Settings")]
    [Export(PropertyHint.Range, "0,20,0.1")]
    public float MoveDistance { get; private set; } = 5;

    [Export(PropertyHint.Range, "0,3,0.1")]
    public float attackDistance { get; private set; } = 2f;

    [Export(PropertyHint.Range, "0,3,0.1")]
    public float attackZone { get; private set; } = 0.8f;

    [Export] public int lightningComboThreshold { get; private set; } = 4;
    [Export] public PackedScene lightningScene { get; private set; }

    private CollisionShape3D[] hitboxShapeNodes;

    public event Action OnDeath = () => { };
    public event Action OnStun;

    public override void _Ready()
    {
        hitboxShapeNodes = HitboxNode?.GetChildren()
            .Cast<CollisionShape3D>()
            .ToArray();

        ToggleHitbox(true);
    }

    public StatResource GetStatResource(Stat stat)
    {
        return stats.Where(child => child.StatType == stat)
            .FirstOrDefault();
    }

    public void ToggleHitbox(bool flag)
    {
        hitboxShapeNodes?.ToList()
            .ForEach(shapeNode => shapeNode.Disabled = flag);
    }

    protected void RaiseStun() => OnStun?.Invoke();
}