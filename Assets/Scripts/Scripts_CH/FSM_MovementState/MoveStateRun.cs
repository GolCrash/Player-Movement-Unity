using UnityEngine;

public class MoveStateRun : FSM_Move_State
{
    public MoveStateRun(FSM_Movement fsm_movement, PlayerContext playerContext) : base(fsm_movement, playerContext)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Вход в состояние Run");
    }

    public override void ExitState()
    {
        Debug.Log("Выход в состояние Run");
    }

    public override void UpdateState()
    {

        if (_playerContext.Input.moveInput == Vector2.zero)
        { 
            _fsm_movement.SetState<MoveStateIdle>();
            return;
        }

        if (!_playerContext.Input.runPressed)
        { 
            _fsm_movement.SetState<MoveStateWalk>(); 
            return;
        }

        _playerContext.Motor.SetHorizontalVelocity(new Vector3(_playerContext.Input.moveInput.x, 0, _playerContext.Input.moveInput.y).normalized * _playerContext.MovementConfig.RunSpeed);
    }
}
