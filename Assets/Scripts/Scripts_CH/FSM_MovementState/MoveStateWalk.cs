using UnityEngine;
using UnityEngine.Windows;

public class MoveStateWalk : FSM_Move_State 
{
    public MoveStateWalk(FSM_Movement fsm_movement, PlayerContext playerContext) : base(fsm_movement, playerContext)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Вход в состояние Walk");
    }

    public override void ExitState()
    {
        Debug.Log("Выход из состояния Walk");
    }

    public override void UpdateState()
    {
        if (_playerContext.Input.moveInput == Vector2.zero)
        { 
            _fsm_movement.SetState<MoveStateIdle>();
            return;
        }

        if (_playerContext.Input.runPressed)
        { 
            _fsm_movement.SetState<MoveStateRun>();
            return;
        }

        if (_playerContext.Input.sneakPressed)
        { 
            _fsm_movement.SetState<MoveStateSneak>();
            return;
        }

        _playerContext.Motor.SetHorizontalVelocity(new Vector3(_playerContext.Input.moveInput.x, 0, _playerContext.Input.moveInput.y).normalized * _playerContext.MovementConfig.WalkSpeed);
    }
}
