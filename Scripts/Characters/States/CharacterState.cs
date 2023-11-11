using System;
using Godot;

namespace RPG.Characters.States;

public abstract partial class CharacterState : Node
{
    public abstract State StateType { get; }
    protected CharacterBody3D characterBodyNode;
    protected CharacterStateMachine stateMachineNode;
    protected Sprite3D sprite3DNode;
    protected AnimationPlayer animPlayerNode;

    public Func<bool> CanTransition = () => true;

    public override void _Ready()
    {
        characterBodyNode = GetOwner<CharacterBody3D>();
        stateMachineNode = GetParent<CharacterStateMachine>();
        sprite3DNode = characterBodyNode.GetNode<Sprite3D>("Sprite3D");
        animPlayerNode = sprite3DNode.GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public virtual void EnterState()
    {
        ProcessMode = Node.ProcessModeEnum.Inherit;
    }

    public virtual void ExitState()
    {
        ProcessMode = Node.ProcessModeEnum.Disabled;
    }

    protected void Flip()
    {
        var isNotMovingHorizontally = characterBodyNode.Velocity.X == 0;

        if (isNotMovingHorizontally)
        {
            return;
        }

        var isMovingLeft = characterBodyNode.Velocity.X < 0;

        sprite3DNode.FlipH = isMovingLeft;
    }

    // public abstract void Input(InputEvent inputEvent);

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