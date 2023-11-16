using Godot;
using RPG.General;

public partial class InteractionIcon : Area3D
{
	private Sprite3D spriteNode;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		spriteNode = GetNode<Sprite3D>("Sprite3D");

		BodyEntered += (body) => spriteNode.Visible = true;
		BodyExited += (body) => spriteNode.Visible = false;
	}

	public override void _Input(InputEvent inputEvent)
	{
		if (!HasOverlappingBodies()) return;

		if (!Input.IsActionJustPressed(GameConstants.INPUT_INTERACT)) return;

		GD.Print("Starting Interaction");
	}
}