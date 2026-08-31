using AssemblyCSharp.Assets.Scripts.Scripts_CH.FSM_AirState;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class AirStateJumping : FSM_Air_State
{
    private RaycastHit _wallHit;

    public AirStateJumping(FSM_Air fsm_air, PlayerContext playerContext) : base(fsm_air, playerContext)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Âõîä â ñîñòîÿíèå Jumping");

        if (_playerContext.Motor.remainingJumps > 0)
        {
            _playerContext.Motor.SetVerticalVelocity(Mathf.Sqrt(_playerContext.MovementConfig.JumpCoeffs[0] * _playerContext.MovementConfig.JumpHeight * _playerContext.MovementConfig.Gravity));

            _playerContext.Motor.remainingJumps--;
        }
    }

    public override void ExitState()
    {
        Debug.Log("Âûõîä èç ñîñòîÿíèÿ Jumping");
    }

    public override void UpdateState()
    {
        if (_playerContext.Input.jumpPressed && _playerContext.Motor.remainingJumps > 0 && _playerContext.MovementConfig.CanJump)
        {
            _playerContext.Motor.SetVerticalVelocity(Mathf.Sqrt(_playerContext.MovementConfig.JumpCoeffs[1] * _playerContext.MovementConfig.JumpHeight * _playerContext.MovementConfig.Gravity));
            _playerContext.Motor.remainingJumps--;
        }

        if (_playerContext.Motor._currentVelocity.y <= 0f)
        {
            _fsm_air.SetState<AirStateFall>();
            return;
        }

        if (ÑanClimb())
        {
            AlignToWall(_wallHit);

            _fsm_air.SetState<MoveStatewWallClimb>();
            return;
        }
    }

    private bool ÑanClimb()
    {
        Vector3 origin = _playerContext.CharacterController.transform.position - Vector3.up * 1;

        Vector3 direction = _playerContext.CharacterController.transform.forward;

        float distance = 1f;

        return Physics.Raycast(origin, direction, out _wallHit, distance);
    }

    private void AlignToWall(RaycastHit hit)
    {
        Vector3 wallNormal = hit.normal;

        Quaternion targetRotation = Quaternion.LookRotation(-wallNormal, Vector3.up);

        _playerContext.CharacterController.transform.rotation = targetRotation;
    }
}
