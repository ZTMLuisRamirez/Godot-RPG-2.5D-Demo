using Godot;
using RPG.Characters.States;
using RPG.General;

namespace RPG.Characters.NPC;

public abstract partial class NPCState : CharacterState
{
    public override void _Ready()
    {
        base._Ready();

        characterNode.AnimPlayerNode.Play(GameConstants.IDLE_ANIM);
    }
}
