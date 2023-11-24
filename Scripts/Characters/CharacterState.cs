using System;
using Godot;

namespace RPG.Characters;

public abstract partial class CharacterState : Node
{
    public abstract State StateType { get; }
    protected Character characterNode;
    protected StateMachine stateMachineNode;

    public Func<bool> CanTransition = () => true;

    public override void _Ready()
    {
        characterNode = GetOwner<Character>();
        stateMachineNode = GetParent<StateMachine>();

        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public virtual void EnterState()
    {
        // ProcessMode = Node.ProcessModeEnum.Inherit;
        SetPhysicsProcess(true);
        SetProcessInput(true);
    }

    public virtual void ExitState()
    {
        // ProcessMode = Node.ProcessModeEnum.Disabled;
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    protected void Flip()
    {
        bool isNotMovingHorizontally = characterNode.Velocity.X == 0;

        if (isNotMovingHorizontally) { return; }

        bool isMovingLeft = characterNode.Velocity.X < 0;

        characterNode.SpriteNode.FlipH = isMovingLeft;
    }
}