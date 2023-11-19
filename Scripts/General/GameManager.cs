using System;
using Godot;
using RPG.Stats;

namespace RPG.General;

public partial class GameManager : Node
{
    private bool canPause = false;
    private int enemyCount = 0;

    public override void _Ready()
    {
        ProcessMode = Node.ProcessModeEnum.Always;

        GameEvents.OnStartGame += HandleStartGame;
        GameEvents.OnEndGame += HandleEndGame;
        GameEvents.OnEnemyDefeated += HandleEnemyDefeated;
        GameEvents.OnBonus += HandleBonus;
        GameEvents.OnBonusClosed += HandleBonusClosed;
    }

    private void HandleEnemyDefeated()
    {
        enemyCount--;

        GameEvents.RaiseNewEnemyCount(enemyCount);

        if (enemyCount <= 0)
        {
            GameEvents.RaiseVictory();

            canPause = false;
            GetTree().Paused = true;
        }
    }

    private void HandleStartGame()
    {
        canPause = true;

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
        if (!canPause) return;

        if (!Input.IsActionJustPressed(GameConstants.INPUT_PAUSE)) return;

        GetTree().Paused = !GetTree().Paused;

        GameEvents.RaisePauseToggled(GetTree().Paused);
    }

    private void HandleEndGame()
    {
        GetTree().Paused = true;
        canPause = false;
    }

    private void HandleBonus(BonusResource resource)
    {
        canPause = false;
    }

    private void HandleBonusClosed()
    {
        canPause = true;
    }
}