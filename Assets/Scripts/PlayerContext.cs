using UnityEngine;

public class PlayerContext
{
    public PlayerInputController Input { get; }
    public CharacterController CharacterController { get; }
    public PlayerCamera Camera { get; }
    public PlayerMotor Motor { get; }
    public PlayerMovementConfig MovementConfig { get; }

    public PlayerContext(PlayerInputController input, CharacterController characterController, PlayerCamera camera, PlayerMotor motor, PlayerMovementConfig movementConfig)
    {
        Input = input;
        CharacterController = characterController;
        Camera = camera;
        Motor = motor;
        MovementConfig = movementConfig;
    }
}
