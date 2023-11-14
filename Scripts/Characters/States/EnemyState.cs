using Godot;
using RPG.General;

namespace RPG.Characters.States;

public abstract partial class EnemyState : CharacterState
{
    protected Vector3 initialPathPosition;
    protected NavigationAgent3D agentNode;
    protected Path3D pathNode;
    // protected EnemyController enemyController;

    public override void _Ready()
    {
        base._Ready();

        agentNode = characterBodyNode.GetNode<NavigationAgent3D>("NavigationAgent3D");
        pathNode = characterBodyNode.GetParent<Path3D>();

        var startingPointPosition = pathNode.Curve.GetPointPosition(0);
        initialPathPosition = startingPointPosition + pathNode.GlobalPosition;

        // enemyController = GetComponent<EnemyController>();

        // if (stateController.patrolState != null)
        // {
        //     originalPosition = stateController.patrolState.GetPositionFunc();
        // }
        // else
        // {
        //     originalPosition = transform.position;
        // }

        // agentComp.updateRotation = false;
    }

    protected Vector3 CalculateUnsafeVelocity(float speed)
    {
        var nextPathPosition = agentNode.GetNextPathPosition();
        var currentPosition = characterBodyNode.GlobalPosition;
        var velocity = nextPathPosition - currentPosition;
        velocity = velocity.Normalized();
        velocity *= speed;

        return velocity;
    }
}
