using Godot;
using RPG.General;
using RPG.Stats;

namespace RPG.Characters.States;

public abstract partial class PlayerState : CharacterState
{
    protected Area3D hitboxNode;
    protected CollisionShape3D hitboxShapeNode;

    public override void EnterState()
    {
        base.EnterState();

        hitboxNode = characterNode.GetNode<Area3D>("Hitbox");
        hitboxShapeNode = hitboxNode.GetNode<CollisionShape3D>("CollisionShape3D");

        hitboxShapeNode.Disabled = true;

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
        return sprite3DNode.FlipH ? Vector3.Left : Vector3.Right;
    }

    private void HandleDeath()
    {
        stateMachineNode.SwitchState(State.Death);
    }
}