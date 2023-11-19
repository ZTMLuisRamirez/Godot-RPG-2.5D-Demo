using Godot;
using RPG.General;

namespace RPG.UI;

public partial class EnemyCountLabel : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GameEvents.OnNewEnemyCount += HandleNewEnemyCount;
	}

	private void HandleNewEnemyCount(int count)
	{
		Text = count.ToString();
	}
}
