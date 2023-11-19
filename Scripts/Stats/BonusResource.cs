using Godot;

namespace RPG.Stats;

[GlobalClass]
public partial class BonusResource : Resource
{
    [Export(PropertyHint.File, "*.png,")]
    public string SpriteTexture { get; private set; }

    [Export]
    public string Description { get; private set; }

    [Export]
    public Stat StatType { get; private set; }

    [Export(PropertyHint.Range, "0,100,1")]
    public float Amount { get; private set; }
}