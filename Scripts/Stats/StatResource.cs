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
    public event Action OnUpdate;

    [Export] public Stat StatType { get; set; }

    private float _statValue;
    [Export]
    public float StatValue
    {
        get => _statValue;
        set
        {
            _statValue = Mathf.Clamp(value, 0, Mathf.Inf);

            OnUpdate?.Invoke();

            if (_statValue <= 0)
            {
                OnZeroOrNegative?.Invoke();
            }
        }
    }
}