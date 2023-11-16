using RPG.Characters.States;

namespace RPG.Characters.NPC;

public partial class NPCIdleState : NPCState
{
    public override State StateType => State.Idle;
}