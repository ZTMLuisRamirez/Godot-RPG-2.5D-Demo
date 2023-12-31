using Godot;
using RPG.General;
using RPG.UI;

namespace RPG.Stats;

public partial class TreasureChest : StaticBody3D
{
	[Export] private BonusResource bonus;
	private InteractionIcon interactionIconNode;

	public override void _Ready()
	{
		base._Ready();

		interactionIconNode = GetNode<InteractionIcon>("InteractionIcon");
		interactionIconNode.Interacted += HandleInteracted;
	}

	private void HandleInteracted()
	{
		GameEvents.RaiseBonus(bonus);

		interactionIconNode.QueueFree();
	}
}