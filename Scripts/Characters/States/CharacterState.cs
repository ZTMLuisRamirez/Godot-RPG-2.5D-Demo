using Godot;

namespace RPG.Characters.States;

public abstract class CharacterState
{
    protected CharacterStateMachine stateMachineNode;

    public CharacterState(CharacterStateMachine newStateMachineNode)
    {
        stateMachineNode = newStateMachineNode;
    }

    public abstract void EnterState();

    public abstract void ProcessState();

    public abstract void ExitState();

    // protected CharacterBody2D characterNode;
    // protected CharacterStateMachineController stateController;

    // // Called when the node enters the scene tree for the first time.
    // public override void _Ready()
    // {
    //     characterNode = GetOwner<CharacterBody2D>();
    //     stateController = GetParent<CharacterStateMachineController>();
    // }

    // protected AnimatorEventBubbler eventBubblerComp;
    // protected Animator animatorComp;
    // protected Movement movementComp;
    // protected SpriteRenderer spriteComp;
    // protected NavMeshAgent agentComp;
    // protected CharacterStats statsComp;

    // public Func<float> GetRangeFunc = () => -1;
    // public Func<Vector3> GetPositionFunc = () => Vector3.zero;

    // protected virtual void Awake()
    // {
    //     stateController = GetComponent<CharacterStateController>();
    //     eventBubblerComp = GetComponentInChildren<AnimatorEventBubbler>();
    //     animatorComp = GetComponentInChildren<Animator>();
    //     movementComp = GetComponent<Movement>();
    //     agentComp = GetComponent<NavMeshAgent>();
    //     spriteComp = GetComponentInChildren<SpriteRenderer>();
    //     statsComp = GetComponent<CharacterStats>();
    // }

    // protected void Flip(Vector3 forward)
    // {
    //     var isNotMovingHorizontally = forward.x == 0;

    //     if (isNotMovingHorizontally)
    //     {
    //         return;
    //     }

    //     var isMovingLeft = forward.x < 0;
    //     spriteComp.flipX = isMovingLeft;
    // }

    // protected bool IsWithinStateRange(CharacterState state)
    // {
    //     if (state == null) return false;

    //     var range = state.GetRangeFunc();

    //     return Physics.CheckSphere(
    //         transform.position,
    //         range,
    //         stateController.targetLayers
    //     );
    // }

    // protected Collider GetTarget(float range)
    // {
    //     var targets = Physics.OverlapSphere(
    //         transform.position,
    //         range,
    //         stateController.targetLayers
    //     );

    //     return targets.Length != 0 ? targets[0] : null;
    // }

    // protected Vector3 GetStartingPosition()
    // {
    //     Vector3 startingPosition = transform.position;
    //     startingPosition.y = 1;

    //     return startingPosition;
    // }
}