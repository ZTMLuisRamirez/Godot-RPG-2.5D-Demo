using Godot;
using RPG.General;

namespace RPG.Characters.States;

public partial class PlayerDeathState : CharacterState
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
		GameEvents.RaiseEndGame();

		characterNode.QueueFree();
	}
}