using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Windows;

public class MovementWithFSM : MonoBehaviour
{
    [SerializeField]
    private PlayerMovementConfig _movementConfig;

    private FSM_Movement _fsmMove;
    private FSM_Air _fsmAir;
    private CharacterController _characterController;
    private PlayerInputController _input;
    private PlayerCamera _camera;
    private PlayerMotor _motor;
    private PlayerContext _playerContext;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _input = GetComponent<PlayerInputController>();
        _camera = GetComponentInChildren<PlayerCamera>();

        _movementConfig = new PlayerMovementConfig();
        _motor = new PlayerMotor(_characterController, transform, _movementConfig);
        _playerContext = new PlayerContext(_input, _characterController, _camera, _motor, _movementConfig);

        _fsmMove = new FSM_Movement();
        _fsmAir = new FSM_Air();

        _fsmMove.AddState(new MoveStateIdle(_fsmMove, _playerContext));
        _fsmMove.AddState(new MoveStateWalk(_fsmMove, _playerContext));
        _fsmMove.AddState(new MoveStateRun(_fsmMove, _playerContext));
        _fsmMove.AddState(new MoveStateSneak(_fsmMove, _playerContext));
        _fsmMove.AddState(new MoveStateSlide(_fsmMove, _playerContext));

        _fsmAir.AddState(new AirStateFall(_fsmAir, _playerContext));
        _fsmAir.AddState(new AirStateGrounded(_fsmAir, _playerContext));
        _fsmAir.AddState(new AirStateJumping(_fsmAir, _playerContext));
    }

    private void Start()
    {
        _fsmMove.SetState<MoveStateIdle>();
        _fsmAir.SetState<AirStateGrounded>();
    }

    private void Update()
    {
        _fsmMove.Update();
        _fsmAir.Update();

        _motor.MovePlayer();
    }
}
