using Godot;
using RPG.Characters.States;
using System;

namespace RPG.Characters;

public abstract partial class CharacterStateMachine : Node
{
	protected CharacterState currentState;

	public void SwitchState(CharacterState newState)
	{
		currentState.ExitState();
		currentState = newState;
		currentState.EnterState();
	}
}
