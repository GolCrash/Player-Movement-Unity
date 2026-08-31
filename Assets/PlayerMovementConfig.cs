using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Scriptable Objects/PlayerMovementConfig")]
public class PlayerMovementConfig : ScriptableObject
{
    [Header("Grounded movement configs")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float sneakSpeed = 3f;
    [SerializeField] float crouchHeightCoeff = 2f;
    [SerializeField] float groundedAcceleration = 1f; //Пока не используем
    [SerializeField] float airAcceleration = 10f;
    [SerializeField] float airSpeedMultiplie = 0.5f;
    [SerializeField] float sensMouse = 0.187f;
    [SerializeField] float sensMultiplie = 1f;

    [Header("Jump configs")]
    [SerializeField] float[] jumpCoeffs = { 1f, 0.75f };
    [SerializeField] float jumpHeight = 2f;

    [Header("General configs")]
    [SerializeField] float gravity = 9.8f;
    [SerializeField] float groundStickForce = -2f;

    [Header("Player parametrs")]
    [SerializeField] float defaultHeight = 1f;
    [SerializeField] float defaultScale = 2f;


    public bool CanJump = true;
    public float WalkSpeed => walkSpeed;
    public float RunSpeed => runSpeed;
    public float SneakSpeed => sneakSpeed;
    public float CrouchHeightCoeff => crouchHeightCoeff;
    public float[] JumpCoeffs => jumpCoeffs;
    public float JumpHeight => jumpHeight;
    public float Gravity => gravity;
    public float GroundStickForce => groundStickForce;
    public float AirAcceleration => airAcceleration;
    public float GrondedAcceleration => groundedAcceleration;
    public float AirSpeedMultiplie => airSpeedMultiplie;
    public float DefaultHeight => defaultHeight;
    public float DefaultScale => defaultScale;
    public float SensMouse => sensMouse;
    public float SensMultiplie => sensMultiplie;
}
