using UnityEngine;
using UnityEngine.Windows;

public class MoveStateIdle : FSM_Move_State
{
    public MoveStateIdle(FSM_Movement fsm_movement, PlayerContext playerContext) : base(fsm_movement, playerContext)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Вход в состояние Idle");
        _playerContext.Motor.SetHorizontalVelocity(new Vector3(0, 0, 0), 0);
    }

    public override void ExitState()
    {
        Debug.Log("Выход из состояния Idle");
    }

    public override void UpdateState()
    {
        if (_playerContext.Input.moveInput != Vector2.zero)
        {
            _fsm_movement.SetState<MoveStateWalk>();
            return;
        }

        if (_playerContext.Input.sneakPressed)
        { 
            _fsm_movement.SetState<MoveStateSneak>(); 
            return;
        }

        
    }
}
