using System.Linq;
using Godot;

namespace RPG.Utilities;

public static class Extensions
{
    public static CharacterBody3D GetFirstTarget(this Area3D area)
    {
        return area.GetOverlappingBodies()
            .Where(child => child is CharacterBody3D)
            .Cast<CharacterBody3D>()
            .FirstOrDefault();
    }
}