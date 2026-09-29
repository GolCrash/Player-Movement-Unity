using UnityEngine;

public class PlayerInstaller : MonoBehaviour
{
    [SerializeField]
    private PlayerMovementConfig _movementConfig;

    private PlayerContext _playerContext;

    private void Awake()
    {
        PlayerInputController input = GetComponent<PlayerInputController>();

        CharacterController characterController = GetComponent<CharacterController>();

        Inventory inventory = GetComponent<Inventory>();

        PlayerCamera camera = GetComponentInChildren<PlayerCamera>();

        MovementWithFSM movement = GetComponent<MovementWithFSM>();

        PlayerIneractive ineractive = GetComponent<PlayerIneractive>();

        camera.Initialize(_movementConfig);

        _playerContext = new PlayerContext(input, characterController, camera, inventory, _movementConfig);

        movement.Initialize(_playerContext);
        ineractive.Initialize(_playerContext);
    }
}
