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

    // public float GetRawStat(Stat stat) => currentStats[stat];

    // public float GetModifiedStat(Stat stat)
    // {
    //     if (!currentStats.ContainsKey(stat)) return 0;

    //     var value = currentStats[stat];

    //     if (modifiers.ContainsKey(stat))
    //     {
    //         value += modifiers[stat];
    //     }

    //     value = Mathf.Clamp(value, 0, Mathf.Infinity);

    //     return value;
    // }
}
