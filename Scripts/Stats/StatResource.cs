using System;
using Godot;
using Godot.Collections;

namespace RPG.Stats;

public enum Stat
{
    Strength,
    Reflex, // Evasion
    Armor,
    Health,
}

[GlobalClass]
public partial class StatResource : Resource
{
    public event Action OnZeroOrNegative = () => { };

    [Export] public Stat StatType { get; set; }

    private float _statValue;
    [Export]
    public float StatValue
    {
        get => _statValue;
        set
        {
            _statValue = value;

            if (_statValue <= 0)
            {
                OnZeroOrNegative.Invoke();
            }
        }
    }
}