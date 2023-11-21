using System;
using Godot;

namespace RPG.Characters.States;

public abstract partial class CharacterState : Node
{
    public abstract State StateType { get; }
    protected Character characterNode;
    protected CharacterStateMachine stateMachineNode;

    public Func<bool> CanTransition = () => true;

    public override void _Ready()
    {
        characterNode = GetOwner<Character>();
        stateMachineNode = GetParent<CharacterStateMachine>();
    }

    public virtual void EnterState()
    {
        ProcessMode = Node.ProcessModeEnum.Inherit;
    }

    public virtual void ExitState()
    {
        ProcessMode = Node.ProcessModeEnum.Disabled;
    }

    protected void Flip()
    {
        var isNotMovingHorizontally = characterNode.Velocity.X == 0;

        if (isNotMovingHorizontally) return;

        var isMovingLeft = characterNode.Velocity.X < 0;

        characterNode.SpriteNode.FlipH = isMovingLeft;
    }
}