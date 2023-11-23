using Godot;
using RPG.General;

namespace RPG.UI;

public partial class InteractionIcon : Area3D
{
	[Signal] public delegate void InteractedEventHandler();
	[Export] private Sprite3D spriteNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += (body) => spriteNode.Visible = true;
		BodyExited += (body) => spriteNode.Visible = false;
	}

	public override void _Input(InputEvent inputEvent)
	{
		if (!HasOverlappingBodies()) return;

		if (!Input.IsActionJustPressed(GameConstants.INPUT_INTERACT)) return;

		EmitSignal(SignalName.Interacted);
	}
}