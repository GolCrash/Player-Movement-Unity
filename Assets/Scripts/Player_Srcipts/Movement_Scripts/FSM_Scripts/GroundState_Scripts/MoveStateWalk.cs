using UnityEngine;
using UnityEngine.Windows;

public class MoveStateWalk : FSM_Move_State 
{
    private Vector3 inputDir;
    private Vector3 worldDir;

    public MoveStateWalk(FSM_Movement fsm_movement, PlayerContext playerContext, PlayerMotor motor) : base(fsm_movement, playerContext, motor)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Вход в состояние Walk");

        //_playerContext.Animator.SetBool("Walk", true);
    }

    public override void ExitState()
    {
        Debug.Log("Выход из состояния Walk");

        //_playerContext.Animator.SetBool("Walk", false);
    }

    public override void UpdateState()
    {
        if (_playerContext.Input.moveInput == Vector2.zero)
        { 
            _fsm_movement.SetState<MoveStateIdle>();
            return;
        }

        if (_playerContext.Input.runPressed && _playerContext.Input.moveInput.y != -1)
        { 
            _fsm_movement.SetState<MoveStateRun>();
            return;
        }

        if (_playerContext.Input.sneakPressed)
        { 
            _fsm_movement.SetState<MoveStateSneak>();
            return;
        }

        inputDir = new Vector3(_playerContext.Input.moveInput.x, 0, _playerContext.Input.moveInput.y).normalized;

        worldDir = _playerContext.CharacterController.transform.TransformDirection(inputDir);

        _motor.SetHorizontalVelocity(worldDir, _playerContext.MovementConfig.WalkSpeed);
    }
}
