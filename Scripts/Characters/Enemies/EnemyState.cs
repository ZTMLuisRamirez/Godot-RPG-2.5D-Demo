using System;
using Godot;
using RPG.Characters.States;
using RPG.General;
using RPG.Stats;

namespace RPG.Characters.Enemies;

public abstract partial class EnemyState : CharacterState
{
    protected Vector3 initialPathPosition;
    protected NavigationAgent3D agentNode;
    protected Path3D pathNode;
    protected Area3D chaseAreaNode;
    protected Area3D attackAreaNode;
    protected Area3D hitboxNode;
    protected CollisionShape3D hitboxShapeNode;

    protected bool isPlayerDetected = false;

    public override void _Ready()
    {
        base._Ready();

        agentNode = characterNode.GetNode<NavigationAgent3D>("NavigationAgent3D");
        pathNode = characterNode.GetParent<Path3D>();
        chaseAreaNode = characterNode.GetNode<Area3D>("ChaseArea");
        attackAreaNode = characterNode.GetNode<Area3D>("AttackArea");
        hitboxNode = characterNode.GetNode<Area3D>("Hitbox");
        hitboxShapeNode = hitboxNode.GetNode<CollisionShape3D>("CollisionShape3D");

        hitboxShapeNode.Disabled = true;

        var startingPointPosition = pathNode.Curve.GetPointPosition(0);
        initialPathPosition = startingPointPosition + pathNode.GlobalPosition;
    }

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

    protected Vector3 CalculateUnsafeVelocity(float speed)
    {
        var nextPathPosition = agentNode.GetNextPathPosition();
        var currentPosition = characterNode.GlobalPosition;
        var velocity = nextPathPosition - currentPosition;
        velocity = velocity.Normalized();
        velocity *= speed;

        return velocity;
    }

    protected void HandleChaseAreaBodyEntered(Node3D body)
    {
        stateMachineNode.SwitchState(State.Chase);
    }

    private void HandleDeath()
    {
        stateMachineNode.SwitchState(State.Death);
    }

    protected void HandleStun()
    {
        stateMachineNode.SwitchState(State.Stun);
    }
}