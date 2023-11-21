using Godot;
using RPG.Characters.States;
using RPG.General;

namespace RPG.Characters.Player;

public partial class PlayerIdleState : PlayerState
{
    public override State StateType => State.Idle;

    public override void EnterState()
    {
        base.EnterState();

        characterNode.AnimPlayerNode.Play(GameConstants.IDLE_ANIM);
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
        CheckForAttackInput();
        CheckForDashInput();
    }
}