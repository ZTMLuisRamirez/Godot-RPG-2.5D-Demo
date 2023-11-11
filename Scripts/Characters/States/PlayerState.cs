using Godot;
using RPG.General;

namespace RPG.Characters.States;

public abstract partial class PlayerState : CharacterState
{
    protected Vector2 GetMoveInput()
    {
        return Input.GetVector(
            Constants.INPUT_MOVE_LEFT,
            Constants.INPUT_MOVE_RIGHT,
            Constants.INPUT_MOVE_FORWARD,
            Constants.INPUT_MOVE_BACKWARD
        );
    }

    protected void CheckForAttackState()
    {
        if (Input.IsActionJustPressed(Constants.INPUT_ATTACK))
        {
            stateMachineNode.SwitchState(State.Attack);
        }
    }

    protected void CheckForDashState()
    {
        if (Input.IsActionJustPressed(Constants.INPUT_DASH))
        {
            stateMachineNode.SwitchState(State.Dash);
        }
    }

    protected Vector3 GetFacingDirection()
    {
        return sprite3DNode.FlipH ? Vector3.Left : Vector3.Right;
    }
}
