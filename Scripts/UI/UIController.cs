using Godot;
using RPG.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG.UI;

public partial class UIController : Control
{
	private Dictionary<UIContainer, MarginContainer> containers = new();
	private Button startButton;

	public override void _Ready()
	{
		containers = GetChildren()
			.Where(child => child is MarginContainer)
			.Cast<MarginContainer>()
			.ToDictionary(child =>
			{
				if (!Enum.TryParse(child.Name, true, out UIContainer result))
				{
					GD.PrintErr(
						$"{child.Name} is not a valid container."
					);
				}

				return result;
			});

		startButton = containers[UIContainer.Start].FindChild("Button") as Button;

		startButton.Pressed += HandleStartPressed;
		GameEvents.OnPauseToggled += HandlePauseToggled;
		GameEvents.OnVictory += HandleVictory;
		GameEvents.OnEndGame += HandleEndGame;

		containers[UIContainer.Start].Visible = true;
	}

	private void HandlePauseToggled(bool isPaused)
	{
		HideContainers();

		var visibleContainer = isPaused ? UIContainer.Pause : UIContainer.Stats;

		containers[visibleContainer].Visible = true;
	}

	private void HandleStartPressed()
	{
		HideContainers();

		containers[UIContainer.Stats].Visible = true;

		GameEvents.RaiseStartGame();
	}

	private void HideContainers()
	{
		foreach (var entry in containers)
		{
			entry.Value.Visible = false;
		}
	}

	private void HandleVictory()
	{
		HideContainers();

		containers[UIContainer.Victory].Visible = true;
	}

	private void HandleEndGame()
	{
		HideContainers();

		containers[UIContainer.Defeat].Visible = true;
	}
}
