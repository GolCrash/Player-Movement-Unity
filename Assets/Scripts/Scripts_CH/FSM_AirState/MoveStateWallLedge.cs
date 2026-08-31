using UnityEngine;

public class MoveStateWallLedge : FSM_Air_State
{
    public MoveStateWallLedge(FSM_Air fsm_air, PlayerContext playerContext) : base(fsm_air, playerContext)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Вход в состояние Ledge");
    }

    public override void ExitState()
    {
        Debug.Log("Выход из состояния Ledge");
    }

    public override void UpdateState()
    {
        if (_playerContext.Input.jumpPressed)
        {
            _playerContext.Motor.SetHorizontalVelocity(_playerContext.CharacterController.transform.forward - Vector3.back * 1, _playerContext.MovementConfig.WalkSpeed);
            _fsm_air.SetState<AirStateJumping>();
            return;
        }

        if (_playerContext.Input.moveInput.y == 1)
        {
            _fsm_air.SetState<AirStateGrounded>();
            return;
        }
    }
}
