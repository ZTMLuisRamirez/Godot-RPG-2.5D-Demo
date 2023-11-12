using Godot;
using RPG.General;
using System;

namespace RPG.Characters.States;

public partial class EnemyMushroomIdleState : EnemyState
{
	public override State StateType => State.Idle;

	public override void EnterState()
	{
		animPlayerNode.Play(Constants.IDLE_ANIM);
	}

	public override void _Process(double delta)
	{
		// if (transform.position != originalPosition)
		// {
		//     stateController.SwitchState(stateController.returnState);
		//     return;
		// }

		// if (IsWithinStateRange(stateController.chaseState))
		// {
		//     stateController.SwitchState(stateController.chaseState);
		// }
	}

	// private void OnDisable()
	// {
	//     animatorComp.SetBool(Constants.IS_IDLE_PARAM, false);
	// }
}
