using UnityEngine;

public class PlayerMotor
{
    private readonly CharacterController _characterController;
    private readonly Transform _transform;
    private readonly PlayerMovementConfig _playerMovementConfig;

    public int remainingJumps;

    public Vector3 _currentHorizontalVelocity;
    private Vector3 _desiredHorizontalVelocity;

    private Vector3 _direction;
    private float _currentSpeed;
    private float _desiredSpeed;

    public PlayerMotor(CharacterController characterController, Transform transform, PlayerMovementConfig playerMovementConfig)
    {
        _characterController = characterController;
        _transform = transform;
        _playerMovementConfig = playerMovementConfig;
    }

    public void SetHorizontalVelocity(Vector3 deriction, float speed)
    {
        _direction = deriction;
        _desiredSpeed = speed;

        _desiredHorizontalVelocity.x = _direction.x * speed;
        _desiredHorizontalVelocity.z = _direction.z * speed;
    }

    public void SetVerticalVelocity(float verticalVelocity)
    {
        _currentHorizontalVelocity.y = verticalVelocity;
    }

    public void MovePlayer()
    {
        HandleGravity();
        UpdateHorizontalVelocity();
        _characterController.Move(_transform.TransformDirection(_currentHorizontalVelocity) * Time.deltaTime);

        Debug.Log(_currentHorizontalVelocity);
    }

    public void HandleGravity()
    {
        if (_characterController.isGrounded)
        {
            if (_currentHorizontalVelocity.y < 0)
                _currentHorizontalVelocity.y = _playerMovementConfig.GroundStickForce;
        }
        else
            _currentHorizontalVelocity.y -= _playerMovementConfig.Gravity * Time.deltaTime;
    }

    private void UpdateHorizontalVelocity()
    {
        //_acceleration = _characterController.isGrounded ? _playerMovementConfig.GrondedAcceleration : _playerMovementConfig.AirAcceleration;

        if (_characterController.isGrounded)
        {
            _currentSpeed = _desiredSpeed;
        }
        else
        {
            _currentSpeed = Mathf.MoveTowards(
            _currentSpeed,
            _desiredSpeed * _playerMovementConfig.AirSpeedMultiplie,
            _playerMovementConfig.AirAcceleration * Time.deltaTime);
        }

        _currentHorizontalVelocity.x = _direction.x * _currentSpeed;
        _currentHorizontalVelocity.z = _direction.z * _currentSpeed;
    }
}