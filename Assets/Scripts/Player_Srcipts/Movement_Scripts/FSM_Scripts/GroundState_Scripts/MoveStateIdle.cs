using UnityEngine;
using UnityEngine.Windows;

public class MoveStateIdle : FSM_Move_State
{
    public MoveStateIdle(FSM_Movement fsm_movement, PlayerContext playerContext, PlayerMotor motor) : base(fsm_movement, playerContext, motor)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Вход в состояние Idle");

        _motor.SetHorizontalVelocity(new Vector3(0, 0, 0), 0);

        //_playerContext.Animator.SetBool("Idle", true);
    }

    public override void ExitState()
    {
        Debug.Log("Выход из состояния Idle");

        //_playerContext.Animator.SetBool("Idle", false);
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