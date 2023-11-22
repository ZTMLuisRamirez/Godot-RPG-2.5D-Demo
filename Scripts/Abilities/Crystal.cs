using Godot;
using RPG.General;

namespace RPG.Abilities;

public partial class Crystal : Ability
{
	[Export] private Timer timerNode;

	public override void _Ready()
	{
		timerNode.Timeout += HandleTimeout;
	}

	private void HandleTimeout()
	{
		animationPlayerNode.Play(GameConstants.EXPLOSION_ANIM);
	}
}
