using Godot;
using RPG.General;

namespace RPG.Characters.States;

public abstract partial class PlayerState : CharacterState
{
    protected Vector2 GetMoveInput()
    {
        return Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT,
            GameConstants.INPUT_MOVE_RIGHT,
            GameConstants.INPUT_MOVE_FORWARD,
            GameConstants.INPUT_MOVE_BACKWARD
        );
    }

    protected void CheckForAttackState()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            stateMachineNode.SwitchState(State.Attack);
        }
    }

    protected void CheckForDashState()
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH))
        {
            stateMachineNode.SwitchState(State.Dash);
        }
    }

    protected Vector3 GetFacingDirection()
    {
        return sprite3DNode.FlipH ? Vector3.Left : Vector3.Right;
    }
}
