using Godot;
using RPG.Characters.States;
using RPG.General;
using System;

namespace RPG.Characters.Enemies;

public partial class EnemyMushroomReturnState : EnemyState
{
    public override State StateType => State.Return;
    [Export(PropertyHint.Range, "0,20,0.1")] private float speed = 1;
    private float movementDelta;

    public override void EnterState()
    {
        base.EnterState();

        characterNode.AnimPlayerNode.Play(GameConstants.RUN_ANIM);
        agentNode.TargetPosition = initialPathPosition;

        agentNode.VelocityComputed += HandleVelocityComputed;
        chaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    public override void ExitState()
    {
        base.ExitState();

        chaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
        agentNode.VelocityComputed -= HandleVelocityComputed;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (agentNode.IsNavigationFinished())
        {
            stateMachineNode.SwitchState(State.Patrol);
            return;
        }

        agentNode.Velocity = CalculateUnsafeVelocity(speed);
    }

    private void HandleVelocityComputed(Vector3 safeVelocity)
    {
        characterNode.Velocity = safeVelocity;
        characterNode.MoveAndSlide();

        Flip();
    }
}