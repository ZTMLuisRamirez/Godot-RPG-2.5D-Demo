using System.Linq;
using Godot;
using RPG.General;
using RPG.Stats;

namespace RPG.Characters.States;

public abstract partial class PlayerState : CharacterState
{
    public override void EnterState()
    {
        base.EnterState();

        if (StateType != State.Death)
        {
            characterNode.GetStatResource(Stat.Health)
                .OnZeroOrNegative += HandleDeath;
        }
    }

    public override void ExitState()
    {
        base.ExitState();

        if (StateType != State.Death)
        {
            characterNode.GetStatResource(Stat.Health)
                .OnZeroOrNegative -= HandleDeath;
        }
    }

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
        return characterNode.SpriteNode.FlipH ? Vector3.Left : Vector3.Right;
    }

    private void HandleDeath()
    {
        stateMachineNode.SwitchState(State.Death);
    }
}