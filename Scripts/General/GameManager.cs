using System;
using Godot;

namespace RPG.General;

public partial class GameManager : Node
{
    private bool isGameStarted = false;

    public override void _Ready()
    {
        GameEvents.OnStartGame += HandleStartGame;
        ProcessMode = Node.ProcessModeEnum.Always;
    }

    private void HandleStartGame()
    {
        isGameStarted = true;
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (!isGameStarted) return;

        if (!Input.IsActionJustPressed(GameConstants.INPUT_PAUSE)) return;

        GetTree().Paused = !GetTree().Paused;

        GameEvents.RaisePauseToggled(GetTree().Paused);
    }
}