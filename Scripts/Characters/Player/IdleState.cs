using Godot;
using RPG.General;

namespace RPG.Characters.Player;

public partial class IdleState : PlayerState
{
    public override State StateType => State.Idle;

    public override void EnterState()
    {
        base.EnterState();

        characterNode.AnimPlayerNode.Play(GameConstants.IDLE_ANIM);
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 direction = GetMoveInput();

        if (direction != Vector2.Zero)
        {
            stateMachineNode.SwitchState(State.Move);
        }
    }

    public override void _Input(InputEvent @event)
    {
        CheckForAttackInput();
        CheckForDashInput();
    }
}