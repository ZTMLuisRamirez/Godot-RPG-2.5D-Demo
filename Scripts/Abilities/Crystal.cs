using Godot;
using RPG.General;

namespace RPG.Abilities;

public partial class Crystal : Ability
{
	[Export] private Timer timerNode;
	[Export] private float duration = 1;

	public override void _Ready()
	{
		base._Ready();

		timerNode.Timeout += HandleTimeout;
		timerNode.WaitTime = duration;
		animationPlayerNode.SpeedScale = duration;

		timerNode.Start();
	}

	private void HandleTimeout()
	{
		animationPlayerNode.SpeedScale = 1;
		animationPlayerNode.Play(GameConstants.EXPLOSION_ANIM);
	}
}