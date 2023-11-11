using Godot;
using RPG.Characters.States;
using System;
using System.Collections.Generic;

namespace RPG.Characters;

public partial class PlayerStateMachine : CharacterStateMachine
{
    public CharacterState idleState;
    // public CharacterState moveState;
    // public CharacterState attackState;
    // public CharacterState dashState;
    // public CharacterState deathState;

    // Called when the node enters the scene tree for the first time.
    // public override void _Ready()
    // {
    // idleState = new PlayerIdleState(this);
    // if (targetLayers == 0)
    // {
    // 	Debug.Log(
    // 		$"The {ToString()} component does not have a target layer."
    // 	);
    // }

    // currentState = idleState;
    // currentState.EnterState();
    // }
}
