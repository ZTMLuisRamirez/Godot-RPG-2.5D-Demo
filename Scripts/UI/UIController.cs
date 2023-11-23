using Godot;
using RPG.General;
using RPG.Stats;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG.UI;

public partial class UIController : Control
{
	private Dictionary<ContainerType, UIContainer> containers = new();
	private Button startButton;

	public override void _Ready()
	{
		containers = GetChildren()
			.Where(child => child is UIContainer)
			.Cast<UIContainer>()
			.ToDictionary(child =>
			{
				if (!Enum.TryParse(child.Name, true, out ContainerType result))
				{
					GD.PrintErr(
						$"{child.Name} is not a valid container."
					);
				}

				return result;
			});

		containers[ContainerType.Start].ButtonNode.Pressed += HandleStartPressed;
		containers[ContainerType.Reward].ButtonNode.Pressed += HandleRewardPressed;

		GameEvents.OnPauseToggled += HandlePauseToggled;
		GameEvents.OnVictory += HandleVictory;
		GameEvents.OnEndGame += HandleEndGame;
		GameEvents.OnBonus += HandleBonus;

		containers[ContainerType.Start].Visible = true;
	}

	private void HandlePauseToggled(bool isPaused)
	{
		HideContainers();

		var visibleContainer = isPaused ? ContainerType.Pause : ContainerType.Stats;

		containers[visibleContainer].Visible = true;
	}

	private void HandleStartPressed()
	{
		HideContainers();

		containers[ContainerType.Stats].Visible = true;

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

		containers[ContainerType.Victory].Visible = true;
	}

	private void HandleEndGame()
	{
		HideContainers();

		containers[ContainerType.Defeat].Visible = true;
	}

	private void HandleBonus(BonusResource resource)
	{
		HideContainers();

		var container = containers[ContainerType.Reward];

		container.TextureNode.Texture = resource.SpriteTexture;
		container.LabelNode.Text = resource.Description;

		container.Visible = true;

		GetTree().Paused = true;
	}

	private void HandleRewardPressed()
	{
		HideContainers();

		containers[ContainerType.Stats].Visible = true;

		GetTree().Paused = false;

		GameEvents.RaiseBonusClosed();
	}
}