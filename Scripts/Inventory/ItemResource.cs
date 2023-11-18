using System;
using Godot;

namespace RPG.Inventory;

[GlobalClass]
public partial class ItemResource : Resource
{
    [Export(PropertyHint.File, "*.png,")]
    public string SpriteTexture { get; set; }
}