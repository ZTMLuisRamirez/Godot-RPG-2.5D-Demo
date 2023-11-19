using Godot;
using RPG.General;

namespace RPG.Characters.States;

public partial class PlayerDeathState : CharacterState
{
	public override State StateType => State.Death;

	public override void EnterState()
	{
		base.EnterState();

		animPlayerNode.Play(GameConstants.DEATH_ANIM);
		animPlayerNode.AnimationFinished += HandleAnimationFinished;
	}

	private void HandleAnimationFinished(StringName animName)
	{
		GameEvents.RaiseEndGame();

		characterNode.QueueFree();
	}
}