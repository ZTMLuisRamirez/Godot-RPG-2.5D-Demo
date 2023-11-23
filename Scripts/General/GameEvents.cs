using System;
using RPG.Stats;

namespace RPG.General;

public static class GameEvents
{
	public static event Action OnStartGame;
	public static event Action OnEndGame;
	public static event Action<bool> OnPauseToggled;
	public static event Action OnEnemyDefeated;
	public static event Action OnVictory;
	public static event Action<int> OnNewEnemyCount;
	public static event Action<Stat, float> OnUpgradeStat;
	public static event Action<BonusResource> OnBonus;
	public static event Action OnBonusClosed;

	public static void RaiseStartGame() => OnStartGame?.Invoke();
	public static void RaisePauseToggled(bool isPaused)
		=> OnPauseToggled?.Invoke(isPaused);
	public static void RaiseEnemyDefeated() => OnEnemyDefeated?.Invoke();
	public static void RaiseVictory() => OnVictory?.Invoke();
	public static void RaiseNewEnemyCount(int count)
		=> OnNewEnemyCount?.Invoke(count);
	public static void RaiseEndGame() => OnEndGame?.Invoke();
	public static void RaiseUpgradeStat(Stat StatToUpgrade, float amount)
		=> OnUpgradeStat?.Invoke(StatToUpgrade, amount);
	public static void RaiseBonus(BonusResource bonus)
		=> OnBonus?.Invoke(bonus);
	public static void RaiseBonusClosed() => OnBonusClosed?.Invoke();
}
