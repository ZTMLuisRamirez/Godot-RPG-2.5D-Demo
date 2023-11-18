using Godot;

namespace RPG.Characters;

public partial class NPCharacter : Character
{
    private InteractionIcon interactionIconNode;

    public override void _Ready()
    {
        base._Ready();

        interactionIconNode = GetNode<InteractionIcon>("InteractionIcon");
        interactionIconNode.Interacted += HandleInteracted;
    }

    private void HandleInteracted()
    {
        GD.Print("Starting Dialogue with NPC");
    }
}