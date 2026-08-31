using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Windows;

public class MovementWithFSM : MonoBehaviour
{
    [SerializeField]
    private PlayerMovementConfig _movementConfig;

    private PlayerStateMachine _playerFSM;
    private CharacterController _characterController;
    private PlayerInputController _input;
    private PlayerCamera _camera;
    private PlayerMotor _motor;
    private PlayerContext _playerContext;

    private void Awake()
    {
        _movementConfig = new PlayerMovementConfig();
        _characterController = GetComponent<CharacterController>();
        _input = GetComponent<PlayerInputController>();
        _playerFSM = new PlayerStateMachine();

        _camera = GetComponentInChildren<PlayerCamera>();
        _camera.Initialize(_movementConfig);
        _motor = new PlayerMotor(_characterController, transform, _movementConfig);
        _playerContext = new PlayerContext(_input, _characterController, _camera, _motor, _movementConfig, _playerFSM);

        

        _playerFSM.AddAllState(_playerContext);
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
