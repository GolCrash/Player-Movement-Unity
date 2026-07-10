using UnityEngine;

public class AirStateJumping : FSM_Air_State
{
    public AirStateJumping(FSM_Air fsm_air, PlayerContext playerContext) : base(fsm_air, playerContext)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Вход в состояние Jumping");

        if (_playerContext.Motor.remainingJumps > 0)
        {
            _playerContext.Motor.SetVerticalVelocity(Mathf.Sqrt(_playerContext.MovementConfig.JumpCoeffs[0] * _playerContext.MovementConfig.JumpHeight * _playerContext.MovementConfig.Gravity));

            _playerContext.Motor.remainingJumps--;
        }
    }

    public override void ExitState()
    {
        Debug.Log("Выход из состояния Jumping");
    }

    public override void UpdateState()
    {
        if (_playerContext.Input.jumpPressed && _playerContext.Motor.remainingJumps > 0 && _playerContext.MovementConfig.CanJump)
        {
            _playerContext.Motor.SetVerticalVelocity(Mathf.Sqrt(_playerContext.MovementConfig.JumpCoeffs[1] * _playerContext.MovementConfig.JumpHeight * _playerContext.MovementConfig.Gravity));
            _playerContext.Motor.remainingJumps--;
        }

        if (_playerContext.Motor.velocity.y <= 0f)
        {
            _fsm_air.SetState<AirStateFall>();
            return;
        }
    }
}
