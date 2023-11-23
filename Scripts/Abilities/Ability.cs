using System;
using Godot;

namespace RPG.Abilities;

public abstract partial class Ability : Node3D
{
    [Export(PropertyHint.Range, "1,10,1")]
    public float Damage { get; private set; } = 10;

    [Export]
    public bool CanStun { get; private set; } = true;

    [Export] protected AnimationPlayer animationPlayerNode;
    [Export] protected CollisionShape3D collisionNode;

    public override void _Ready()
    {
        animationPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        QueueFree();
    }

    private void Hit()
    {
        collisionNode.Disabled = false;
    }
}