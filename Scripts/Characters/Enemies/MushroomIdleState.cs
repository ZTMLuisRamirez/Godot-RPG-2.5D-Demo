using Godot;
using RPG.General;

namespace RPG.Characters.Enemies;

public partial class MushroomIdleState : EnemyState
{
	public override State StateType => State.Idle;

	[Export] private Timer durationTimer;

	public override void EnterState()
	{
		base.EnterState();

		characterNode.AnimPlayerNode.Play(GameConstants.IDLE_ANIM);

		durationTimer.Timeout += HandleTimeout;
		durationTimer.Start();
	}

	public override void ExitState()
	{
		base.ExitState();

		durationTimer.Timeout -= HandleTimeout;
		durationTimer.Stop();
	}

	public override void _PhysicsProcess(double delta)
	{
		if (characterNode.ChaseAreaNode.HasOverlappingBodies())
		{
			stateMachineNode.SwitchState(State.Chase);
		}
	}

	private void HandleTimeout()
	{
		stateMachineNode.SwitchState(State.Return);
	}
}