using Godot;

namespace RPG.UI;

public partial class UIContainer : MarginContainer
{
    [Export] public TextureRect TextureNode { get; private set; }
    [Export] public Label LabelNode { get; private set; }
    [Export] public Button ButtonNode { get; private set; }
}