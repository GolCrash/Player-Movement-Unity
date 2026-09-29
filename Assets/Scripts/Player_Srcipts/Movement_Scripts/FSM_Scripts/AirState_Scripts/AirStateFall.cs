using UnityEngine;

public class AirStateFall : FSM_Air_State
{
    public AirStateFall(FSM_Air fsm_air, PlayerContext playerContext, PlayerMotor motor) : base(fsm_air, playerContext, motor)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Вход в состояние Fall");
    }

    public override void ExitState()
    {
        Debug.Log("Выход из состояния Fall");
    }

    public override void UpdateState()
    {
        if (_playerContext.CharacterController.isGrounded)
        { 
            _fsm_air.SetState<AirStateGrounded>();
            return;
        }

        if (_playerContext.Input.jumpPressed && _motor.remainingJumps != 0 && _playerContext.MovementConfig.CanJump)
        {
            _fsm_air.SetState<AirStateJumping>();
            return;
        }
    }
}