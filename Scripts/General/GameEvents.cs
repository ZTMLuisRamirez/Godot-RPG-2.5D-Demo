using System;
using Godot;

using RPG.General;

public static class GameEvents
{
	public static event Action OnStartGame;
	public static event Action<bool> OnPauseToggled;

	public static void RaiseStartGame() => OnStartGame?.Invoke();
	public static void RaisePauseToggled(bool isPaused) => OnPauseToggled?.Invoke(isPaused);
}
