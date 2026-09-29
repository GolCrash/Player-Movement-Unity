using UnityEngine;

public class MovementWithFSM : MonoBehaviour
{
    private PlayerContext _playerContext;

    private PlayerMotor _motor;
    private PlayerStateMachine _playerFSM;

    public void Initialize(PlayerContext context)
    {
        _playerContext = context;

        _motor = new PlayerMotor(context.CharacterController, transform, context.MovementConfig);
        _playerFSM = new PlayerStateMachine();

        _playerFSM.AddAllState(_playerContext, _motor, _playerFSM);
    }

    private void Start()
    {
        _playerFSM.SetState();
    }

    private void Update()
    {
        _playerFSM.Update();
        _motor.MovePlayer();
    }
}
