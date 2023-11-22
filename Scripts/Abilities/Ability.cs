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

    private void Hit()
    {
        collisionNode.Disabled = false;
    }
}