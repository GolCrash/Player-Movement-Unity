using UnityEngine;

public class MoveStateWallLedge : FSM_Air_State
{
    private readonly PlayerStateMachine _playerFSM;

    private bool isMove;
    private float _ledgeStartSpeed;

    public MoveStateWallLedge(FSM_Air fsm_air, PlayerContext playerContext, PlayerMotor motor, PlayerStateMachine playerFSM) : base(fsm_air, playerContext, motor)
    {
        _playerFSM = playerFSM;
    }

    public override void EnterState()
    {
        BlockedGroundMove();

        _playerContext.Camera.SetRotationLimit(45f);

        isMove = false;
        _ledgeStartSpeed = 4f;

        Debug.Log("Вход в состояние Ledge");
    }

    public override void ExitState()
    {
        _playerContext.Camera.ClearRotationLimit();
        _playerContext.Camera.ResetSensMultiplie();

        isMove = false;
        _playerFSM.Ground.isActive = true;

        Debug.Log("Выход из состояния Ledge");
    }

    public override void UpdateState()
    {
        if (_playerContext.Input.jumpPressed)
        {
            //_playerContext.Motor.SetHorizontalVelocity(_playerContext.CharacterController.transform.forward - Vector3.back * 1, _playerContext.MovementConfig.WalkSpeed);
            _fsm_air.SetState<AirStateJumping>();
            return;
        }

        if (_playerContext.Input.moveInput.y == 1)
        {
            isMove = true;
            return;
        }

        if (_ledgeStartSpeed == 5f)
        {
            Debug.Log(_ledgeStartSpeed);
            Debug.Log(isMove);

            _fsm_air.SetState<AirStateFall>();
            return;
        }

        if (!isMove)
            _motor.SetVerticalVelocity(0);

        if (isMove)
        {
            _ledgeStartSpeed = Mathf.MoveTowards(_ledgeStartSpeed, 5f, Time.deltaTime);
            _motor.SetHorizontalVelocity(_playerContext.CharacterController.transform.forward, _ledgeStartSpeed);
            _motor.SetVerticalVelocity(Mathf.Sqrt(_playerContext.MovementConfig.Gravity));
        }
    }

    private void BlockedGroundMove()
    {
        _playerFSM.Ground.isActive = false;

        _motor.SetHorizontalVelocity(0f);
    }
}
