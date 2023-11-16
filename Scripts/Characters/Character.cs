using System;
using Godot;
using RPG.Stats;
using System.Linq;

namespace RPG.Characters;

public abstract partial class Character : CharacterBody3D
{
    [Export] protected StatResource[] stats;

    public event Action OnDeath = () => { };

    public StatResource GetStatResource(Stat stat)
    {
        return stats.Where(child => child.StatType == stat)
            .FirstOrDefault();
    }
}