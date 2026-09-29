using UnityEngine;

public class AirStateGrounded : FSM_Air_State
{
    public AirStateGrounded(FSM_Air fsm_air, PlayerContext playerContext, PlayerMotor motor) : base(fsm_air, playerContext, motor)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Вход в состояние Grounded");
    }

    public override void ExitState()
    {
        Debug.Log("Выход из состояния Grounded");
    }

    public override void UpdateState()
    {
        if (_playerContext.Input.jumpPressed && _playerContext.MovementConfig.CanJump)
        {
            _motor.remainingJumps = 2;

            _fsm_air.SetState<AirStateJumping>();
            return;
        }

         if (!_playerContext.CharacterController.isGrounded)
        {
            _motor.remainingJumps = 1;

            _fsm_air.SetState<AirStateFall>();
            return;
        }
    }
}