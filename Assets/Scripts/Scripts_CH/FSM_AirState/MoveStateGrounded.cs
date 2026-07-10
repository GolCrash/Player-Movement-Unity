using UnityEngine;

public class AirStateGrounded : FSM_Air_State
{
    public AirStateGrounded(FSM_Air fsm_air, PlayerContext playerContext) : base(fsm_air, playerContext)
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
            _playerContext.Motor.remainingJumps = 2;

            _fsm_air.SetState<AirStateJumping>();
            return;
        }

         if (!_playerContext.CharacterController.isGrounded)
        {
            _playerContext.Motor.remainingJumps = 1;

            _fsm_air.SetState<AirStateFall>();
            return;
        }
    }
}