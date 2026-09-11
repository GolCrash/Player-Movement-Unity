using AssemblyCSharp.Assets.Scripts.Scripts_CH.FSM_AirState;

public class PlayerStateMachine
{
	public FSM_Movement Ground { get; }
	public FSM_Air Air { get; }

	public PlayerStateMachine()
	{
		Ground = new FSM_Movement();
		Air = new FSM_Air();
	}

	public void Update()
	{
		Ground.Update();
		Air.Update();
	}

	public void SetState()
	{
		Ground.SetState<MoveStateIdle>();
		Air.SetState<AirStateGrounded>();
	}

	public void AddAllState(PlayerContext _playerContext)
	{
        Ground.AddState(new MoveStateIdle(Ground, _playerContext));
        Ground.AddState(new MoveStateWalk(Ground, _playerContext));
        Ground.AddState(new MoveStateRun(Ground, _playerContext));
        Ground.AddState(new MoveStateSneak(Ground, _playerContext));
        Ground.AddState(new MoveStateSlide(Ground, _playerContext));

        Air.AddState(new AirStateFall(Air, _playerContext));
        Air.AddState(new AirStateGrounded(Air, _playerContext));
        Air.AddState(new AirStateJumping(Air, _playerContext));
        Air.AddState(new MoveStateWallClimb(Air, _playerContext));
		Air.AddState(new MoveStateWallLedge(Air, _playerContext));
        Air.AddState(new MoveStateWallRun(Air, _playerContext));
    }
}