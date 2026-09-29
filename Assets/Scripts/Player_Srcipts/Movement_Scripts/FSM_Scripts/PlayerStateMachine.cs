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

	public void AddAllState(PlayerContext _playerContext, PlayerMotor _motor, PlayerStateMachine _playerFSM)
	{
        Ground.AddState(new MoveStateIdle(Ground, _playerContext, _motor));
        Ground.AddState(new MoveStateWalk(Ground, _playerContext, _motor));
        Ground.AddState(new MoveStateRun(Ground, _playerContext, _motor));
        Ground.AddState(new MoveStateSneak(Ground, _playerContext, _motor));
        Ground.AddState(new MoveStateSlide(Ground, _playerContext, _motor));

        Air.AddState(new AirStateFall(Air, _playerContext, _motor));
        Air.AddState(new AirStateGrounded(Air, _playerContext, _motor));
        Air.AddState(new AirStateJumping(Air, _playerContext, _motor, _playerFSM));
        Air.AddState(new MoveStateWallClimb(Air, _playerContext, _motor, _playerFSM));
		Air.AddState(new MoveStateWallLedge(Air, _playerContext, _motor, _playerFSM));
        Air.AddState(new MoveStateWallRun(Air, _playerContext, _motor, _playerFSM));
    }
}