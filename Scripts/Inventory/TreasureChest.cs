using Godot;

namespace RPG.Inventory;

public partial class TreasureChest : StaticBody3D
{
	[Export] private ItemResource item;
	private InteractionIcon interactionIconNode;

	public override void _Ready()
	{
		base._Ready();

		interactionIconNode = GetNode<InteractionIcon>("InteractionIcon");
		interactionIconNode.Interacted += HandleInteracted;
	}

	private void HandleInteracted()
	{
		GD.Print("Opening Treasure Chest");
	}
}
