using Godot;
using RPG.General;

namespace RPG.Characters.Enemies;

public partial class MushroomReturnState : EnemyState
{
    public override State StateType => State.Return;

    [Export(PropertyHint.Range, "0,20,0.1")] private float speed = 1;

    public override void EnterState()
    {
        base.EnterState();

        characterNode.AnimPlayerNode.Play(GameConstants.RUN_ANIM);

        characterNode.AgentNode.TargetPosition = initialPathPosition;
        characterNode.AgentNode.VelocityComputed += HandleVelocityComputed;
    }

    public override void ExitState()
    {
        base.ExitState();

        characterNode.AgentNode.VelocityComputed -= HandleVelocityComputed;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.AgentNode.IsNavigationFinished())
        {
            stateMachineNode.SwitchState(State.Patrol);
            return;
        }

        if (characterNode.ChaseAreaNode.HasOverlappingBodies())
        {
            stateMachineNode.SwitchState(State.Chase);
            return;
        }

        characterNode.AgentNode.Velocity = CalculateUnsafeVelocity(speed);
    }

    private void HandleVelocityComputed(Vector3 safeVelocity)
    {
        characterNode.Velocity = safeVelocity;
        characterNode.MoveAndSlide();

        Flip();
    }
}