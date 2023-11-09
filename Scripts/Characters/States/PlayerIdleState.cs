using Godot;
using System;

namespace RPG.Characters.States;

public class PlayerIdleState : CharacterState
{
    public PlayerIdleState(CharacterStateMachine newStateMachineNode) : base(newStateMachineNode)
    {
    }

    public override void EnterState()
    {
        GD.Print("Entered State");
    }

    public override void ExitState()
    {
        throw new NotImplementedException();
    }

    public override void ProcessState()
    {
        throw new NotImplementedException();
    }
}
