using Godot;
using RPG.General;

namespace RPG.Characters.Enemies;

public partial class MushroomDeathState : EnemyState
{
	public override State StateType => State.Death;

	public override void EnterState()
	{
		base.EnterState();

		characterNode.AnimPlayerNode.Play(GameConstants.DEATH_ANIM);
		characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
	}

	private void HandleAnimationFinished(StringName animName)
	{
		GameEvents.RaiseEnemyDefeated();

		characterNode.QueueFree();
	}
}