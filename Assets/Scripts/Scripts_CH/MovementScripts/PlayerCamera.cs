using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] float sensMouse = 0.187f;
    [SerializeField] Transform player;

    public float pitch;
    public float yaw;

    private void Update()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        yaw += mouseDelta.x * sensMouse;
        player.rotation = Quaternion.Euler(0, yaw, 0);

        pitch -= mouseDelta.y * sensMouse;
        pitch = Mathf.Clamp(pitch, -70f, 70f);

        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
