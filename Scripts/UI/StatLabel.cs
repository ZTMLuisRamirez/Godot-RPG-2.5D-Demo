using Godot;
using RPG.Stats;
using System;

public partial class StatLabel : Label
{
	[Export] private StatResource statResource;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		statResource.OnUpdate += HandleUpdate;

		Text = statResource.StatValue.ToString();
	}

	private void HandleUpdate(float statValue)
	{
		Text = statResource.StatValue.ToString();
	}
}
