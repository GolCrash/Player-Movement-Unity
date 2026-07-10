using UnityEngine;

public class PlayerMotor
{
    private readonly CharacterController _characterController;
    private readonly Transform _transform;
    private readonly PlayerMovementConfig _playerMovementConfig;

    public int remainingJumps;

    public Vector3 velocity;

    public PlayerMotor(CharacterController characterController, Transform transform, PlayerMovementConfig playerMovementConfig)
    {
        _characterController = characterController;
        _transform = transform;
        _playerMovementConfig = playerMovementConfig;
    }

    public void SetHorizontalVelocity(Vector3 horizontalVelocity)
    {
        velocity.x = horizontalVelocity.x;
        velocity.z = horizontalVelocity.z;
    }

    public void SetVerticalVelocity(float verticalVelocity)
    {
        velocity.y = verticalVelocity;
    }

    public void MovePlayer()
    {
        HandleGravity();
        _characterController.Move(_transform.TransformDirection(velocity) * Time.deltaTime);
        
        //Debug.Log(_transform.TransformDirection(velocity).magnitude);
    }

    public void HandleGravity()
    {
        if (_characterController.isGrounded)
        {
            if (velocity.y < 0)
                velocity.y = _playerMovementConfig.GroundStickForce;
        }
        else
            velocity.y -= _playerMovementConfig.Gravity * Time.deltaTime;
    }
}