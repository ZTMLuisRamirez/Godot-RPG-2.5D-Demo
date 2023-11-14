using Godot;
using RPG.General;
using System;

namespace RPG.Characters.States;

public partial class EnemyMushroomReturnState : EnemyState
{
    public override State StateType => State.Return;
    [Export(PropertyHint.Range, "0,20,0.1")] private float speed = 1;
    private float movementDelta;

    public override void _Ready()
    {
        base._Ready();

        agentNode.VelocityComputed += HandleVelocityComputed;
    }

    public override void EnterState()
    {
        base.EnterState();

        animPlayerNode.Play(GameConstants.RUN_ANIM);
        agentNode.TargetPosition = initialPathPosition;
    }

    public override void _PhysicsProcess(double delta)
    {
        // if (IsWithinStateRange(stateController.chaseState))
        // {
        //     stateController.SwitchState(stateController.chaseState);
        //     return;
        // }

        if (agentNode.IsNavigationFinished())
        {
            stateMachineNode.SwitchState(State.Patrol);
            return;
        }

        agentNode.Velocity = CalculateUnsafeVelocity(speed);
    }

    private void HandleVelocityComputed(Vector3 safeVelocity)
    {
        characterBodyNode.Velocity = safeVelocity;
        characterBodyNode.MoveAndSlide();

        Flip();
    }



    // private void OnDisable()
    // {
    //     animatorComp.SetBool(Constants.IS_MOVING_PARAM, false);
    // }

    // private bool ReachedDestination()
    // {
    //     if (agentComp.pathPending) return false;

    //     if (agentComp.remainingDistance > agentComp.stoppingDistance) return false;

    //     if (agentComp.hasPath || agentComp.velocity.sqrMagnitude != 0f) return false;

    //     return true;
    // }
}
