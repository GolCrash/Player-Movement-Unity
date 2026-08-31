using UnityEngine;

public class PlayerMotor
{
    private readonly CharacterController _characterController;
    private readonly Transform _transform;
    private readonly PlayerMovementConfig _playerMovementConfig;

    public int remainingJumps;

    public Vector3 _currentVelocity;
    private Vector3 _desiredVelocity;

    private Vector3 _direction;
    public float _currentSpeed;
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

        _currentVelocity.x = _direction.x * speed;
        _currentVelocity.z = _direction.z * speed;
    }

    public void SetHorizontalVelocity(float speed)
    {
        _desiredSpeed = speed;

        _currentVelocity.x = _direction.x * speed;
        _currentVelocity.z = _direction.z * speed;
    }

    public void SetVerticalVelocity(float verticalVelocity)
    {
        _currentVelocity.y = verticalVelocity;
    }

    public void MovePlayer()
    {
        HandleGravity();
        UpdateHorizontalVelocity();
        _characterController.Move(_transform.TransformDirection(_currentVelocity) * Time.deltaTime);

       // Debug.Log(_currentHorizontalVelocity);
    }

    public void HandleGravity()
    {
        if (_characterController.isGrounded)
        {
            if (_currentVelocity.y < 0)
                _currentVelocity.y = _playerMovementConfig.GroundStickForce;
        }
        else
            _currentVelocity.y -= _playerMovementConfig.Gravity * Time.deltaTime;
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
           // _currentSpeed = Mathf.MoveTowards(
           // _currentSpeed,
           // _desiredSpeed * _playerMovementConfig.AirSpeedMultiplie,
           // _playerMovementConfig.AirAcceleration * Time.deltaTime);

            _currentSpeed = _desiredSpeed;
        }

        _currentVelocity.x = _direction.x * _currentSpeed;
        _currentVelocity.z = _direction.z * _currentSpeed;
    }
}