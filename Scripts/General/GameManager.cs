using System;
using Godot;

namespace RPG.General;

public partial class GameManager : Node
{
    private bool isGameRunning = false;
    private int enemyCount = 0;

    public override void _Ready()
    {
        ProcessMode = Node.ProcessModeEnum.Always;

        GameEvents.OnStartGame += HandleStartGame;
        GameEvents.OnEndGame += HandleEndGame;
        GameEvents.OnEnemyDefeated += HandleEnemyDefeated;
    }

    private void HandleEnemyDefeated()
    {
        enemyCount--;

        GameEvents.RaiseNewEnemyCount(enemyCount);

        if (enemyCount <= 0)
        {
            GameEvents.RaiseVictory();

            isGameRunning = false;
            GetTree().Paused = true;
        }
    }

    private void HandleStartGame()
    {
        isGameRunning = true;

        var scene = GetTree().CurrentScene;

        if (scene is LevelManager levelManager)
        {
            enemyCount = levelManager.enemyContainerNode
                .GetChildCount();

            GameEvents.RaiseNewEnemyCount(enemyCount);
        }
    }

    public override void _Input(InputEvent inputEvent)
    {
        if (!isGameRunning) return;

        if (!Input.IsActionJustPressed(GameConstants.INPUT_PAUSE)) return;

        GetTree().Paused = !GetTree().Paused;

        GameEvents.RaisePauseToggled(GetTree().Paused);
    }

    private void HandleEndGame()
    {
        isGameRunning = false;
    }
}