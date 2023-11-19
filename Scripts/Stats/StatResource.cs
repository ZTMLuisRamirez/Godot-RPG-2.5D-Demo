using System;
using Godot;

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
    public event Action OnZeroOrNegative;
    public event Action<float> OnUpdate;

    [Export] public Stat StatType { get; set; }

    private float _statValue;
    [Export]
    public float StatValue
    {
        get => _statValue;
        set
        {
            _statValue = value;

            OnUpdate?.Invoke(_statValue);

            if (_statValue <= 0)
            {
                OnZeroOrNegative?.Invoke();
            }
        }
    }
}