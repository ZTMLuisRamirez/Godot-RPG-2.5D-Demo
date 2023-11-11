using Godot;
using RPG.General;

namespace RPG.Characters.States;

public partial class PlayerIdleState : PlayerState
{
    public override State StateType { get; } = State.Idle;

    public override void EnterState()
    {
        base.EnterState();

        animPlayerNode.Play(Constants.IDLE_ANIM);
    }

    public override void _Process(double delta)
    {
        var direction = GetMoveInput();

        if (direction != Vector2.Zero)
        {
            stateMachineNode.SwitchState(State.Move);
        }
    }

    public override void _Input(InputEvent @event)
    {
        CheckForAttackState();
        CheckForDashState();
    }
}
