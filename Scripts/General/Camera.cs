using Godot;

namespace RPG.General;

public partial class Camera : Camera3D
{
	[Export] private Node3D target;
	[Export] private Vector3 positionFromTarget;

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
	}

	private void HandleEndGame()
	{
		Reparent(GetTree().CurrentScene);
	}
}