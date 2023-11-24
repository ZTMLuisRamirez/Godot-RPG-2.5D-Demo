using Godot;
using RPG.Stats;

namespace RPG.Characters.Enemies;

public abstract partial class EnemyState : CharacterState
{
    protected Vector3 initialPathPosition;
    protected Path3D pathNode;

    protected bool isPlayerDetected = false;

    public override void _Ready()
    {
        base._Ready();

        pathNode = characterNode.GetParent<Path3D>();

        characterNode.ToggleHitbox(true);

        Vector3 startingPointPosition = pathNode.Curve.GetPointPosition(0);
        initialPathPosition = startingPointPosition + pathNode.GlobalPosition;
    }

    public override void EnterState()
    {
        base.EnterState();

        if (StateType != State.Death)
        {
            characterNode.GetStatResource(Stat.Health)
                .OnZeroOrNegative += HandleDeath;
            characterNode.OnStun += HandleStun;
        }
    }

    public override void ExitState()
    {
        base.ExitState();

        if (StateType != State.Death)
        {
            characterNode.GetStatResource(Stat.Health)
                .OnZeroOrNegative -= HandleDeath;
            characterNode.OnStun -= HandleStun;
        }
    }

    protected void MoveWithAI(float speed)
    {
        Vector3 nextPathPosition = characterNode.AgentNode.GetNextPathPosition();
        Vector3 currentPosition = characterNode.GlobalPosition;
        Vector3 velocity = nextPathPosition - currentPosition;
        velocity = velocity.Normalized();
        velocity *= speed;

        characterNode.Velocity = velocity;
        characterNode.MoveAndSlide();

        Flip();
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