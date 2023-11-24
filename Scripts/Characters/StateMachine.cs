using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace RPG.Characters;

public partial class StateMachine : Node
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

		if (!states[newState].CanTransition()) { return; }

		currentState.ExitState();
		currentState = states[newState];
		currentState.EnterState();
	}
}