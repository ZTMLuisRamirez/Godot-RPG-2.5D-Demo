using Godot;
using System;
using System.Collections.Generic;
using RPG.Characters.States;
using System.Linq;
using System.Data;
using System.Diagnostics;

namespace RPG.Characters;

public partial class CharacterStateMachine : Node
{
	public Dictionary<State, CharacterState> states = new();
	public CharacterState currentState;

	public override void _Ready()
	{
		states = GetChildren()
			.Where(child => child is CharacterState)
			.Cast<CharacterState>()
			.ToDictionary(child => child.StateType);

		if (!states.ContainsKey(State.Idle))
		{
			GD.PrintErr($"{Owner.Name} does not have an idle state.");
		}

		currentState = states[State.Idle];
		currentState.EnterState();
	}

	public void SwitchState(State newState)
	{
		if (!states.ContainsKey(newState))
		{
			GD.PrintErr(
				$"The {currentState.Name} node from the {Owner.Name} scene is attempting to switch to the {newState.ToString()} state, but it does not exist."
			);
		}

		currentState.ExitState();
		currentState = states[newState];
		currentState.EnterState();
	}

	// public bool IsCurrentState(string stateToCheck)
	// {
	// 	Enum.TryParse<State>(stateToCheck, true, out State result);

	// 	return currentState.StateType == result;
	// }
}
