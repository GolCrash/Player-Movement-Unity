using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Scriptable Objects/PlayerMovementConfig")]
public class PlayerMovementConfig : ScriptableObject
{
    [Header("Grounded movement configs")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float sneakSpeed = 3f;
    [SerializeField] float crouchHeightCoeff = 2f;

    [Header("Jump configs")]
    [SerializeField] float[] jumpCoeffs = { 2f, 1.5f };
    [SerializeField] float jumpHeight = 2f;

    [Header("General configs")]
    [SerializeField] float gravity = 9.8f;
    [SerializeField] float groundStickForce = -2f;


    public bool CanJump = true;
    public float WalkSpeed => walkSpeed;
    public float RunSpeed => runSpeed;
    public float SneakSpeed => sneakSpeed;
    public float CrouchHeightCoeff => crouchHeightCoeff;
    public float[] JumpCoeffs => jumpCoeffs;
    public float JumpHeight => jumpHeight;
    public float Gravity => gravity;
    public float GroundStickForce => groundStickForce;
}
