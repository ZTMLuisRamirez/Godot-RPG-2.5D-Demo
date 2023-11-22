using Godot;
using RPG.Characters.States;
using RPG.General;

namespace RPG.Characters.Player;

public partial class DeathState : CharacterState
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