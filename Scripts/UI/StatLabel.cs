using Godot;
using RPG.Stats;

namespace RPG.UI;

public partial class StatLabel : Label
{
	[Export] private StatResource statResource;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		statResource.OnUpdate += HandleUpdate;

		Text = statResource.StatValue.ToString();
	}

	private void HandleUpdate()
	{
		Text = statResource.StatValue.ToString();
	}
}