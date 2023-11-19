using Godot;
using RPG.Characters;
using System;

namespace RPG.General;

public partial class Camera : Camera3D
{
	[Export] private Node3D target;
	[Export(PropertyHint.Range, "0,100,1")] private Vector3 positionFromTarget;
	private bool isFollowingTarget = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameEvents.OnStartGame += HandleStartGame;
		GameEvents.OnEndGame += HandleEndGame;
	}

	private void HandleStartGame()
	{
		Reparent(target, false);

		Position = positionFromTarget;

		isFollowingTarget = true;
	}

	private void HandleEndGame()
	{
		Reparent(GetTree().CurrentScene);
	}
}
