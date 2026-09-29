using UnityEngine;

public class PlayerContext
{
    public PlayerInputController Input { get; }
    public CharacterController CharacterController { get; }
    public PlayerCamera Camera { get; }
    public Inventory Inventory { get; }
    public PlayerMovementConfig MovementConfig { get; }

    public PlayerContext(PlayerInputController input, CharacterController characterController, PlayerCamera camera, Inventory inventory, PlayerMovementConfig movementConfig)
    {
        Input = input;
        CharacterController = characterController;
        Camera = camera;
        Inventory = inventory;
        MovementConfig = movementConfig;
    }
}
