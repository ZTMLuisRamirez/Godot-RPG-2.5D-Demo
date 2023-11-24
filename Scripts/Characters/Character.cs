using System;
using Godot;
using RPG.Stats;
using System.Linq;
using RPG.Combat;
using RPG.Characters.Enemies;

namespace RPG.Characters;

public abstract partial class Character : CharacterBody3D
{
    [Export] public StatResource[] stats;

    [ExportGroup("Required Nodes")]
    [Export] public Area3D HurtboxNode { get; private set; }
    [Export] public Sprite3D SpriteNode { get; private set; }
    [Export] public AnimationPlayer AnimPlayerNode { get; private set; }
    [Export] public Area3D HitboxNode { get; private set; }
    [Export] public Timer HurtShaderTimerNode { get; private set; }

    [ExportGroup("AI Nodes")]
    [Export] public Area3D ChaseAreaNode { get; private set; }
    [Export] public NavigationAgent3D AgentNode { get; private set; }
    [Export] public Area3D AttackAreaNode { get; private set; }

    private CollisionShape3D[] hitboxShapeNodes;
    public ShaderMaterial ShaderMat { get; private set; }

    public event Action OnDeath = () => { };
    public event Action OnStun;

    public override void _Ready()
    {
        hitboxShapeNodes = HitboxNode?.GetChildren()
            .Cast<CollisionShape3D>()
            .ToArray();

        ToggleHitbox(true);

        HurtShaderTimerNode.Timeout += HandleShaderTimeout;
        SpriteNode.TextureChanged += HandleTextureChanged;

        ShaderMat = (ShaderMaterial)SpriteNode.MaterialOverlay;
    }

    private void HandleTextureChanged()
    {
        ShaderMat.SetShaderParameter(
            "tex",
            SpriteNode.Texture
        );
    }

    public StatResource GetStatResource(Stat stat)
    {
        return stats.Where(child => child.StatType == stat)
            .FirstOrDefault();
    }

    public void ToggleHitbox(bool flag)
    {
        hitboxShapeNodes?.ToList()
            .ForEach(shapeNode => shapeNode.SetDeferred("disabled", flag));
    }

    protected void RaiseStun() => OnStun?.Invoke();

    private void HandleShaderTimeout()
    {
        ShaderMat.SetShaderParameter("active", false);
    }
}