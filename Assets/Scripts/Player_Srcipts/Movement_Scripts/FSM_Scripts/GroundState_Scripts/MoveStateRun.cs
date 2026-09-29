using UnityEngine;

public class MoveStateRun : FSM_Move_State
{

    private Vector3 inputDir;
    private Vector3 worldDir;

    public MoveStateRun(FSM_Movement fsm_movement, PlayerContext playerContext, PlayerMotor motor) : base(fsm_movement, playerContext, motor)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Вход в состояние Run");

        //_playerContext.Animator.SetBool("Run", true);
    }

    public override void ExitState()
    {
        Debug.Log("Выход в состояние Run");

       //_playerContext.Animator.SetBool("Run", false);
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

        if (_playerContext.Input.moveInput.y == -1)
        {
            _fsm_movement.SetState<MoveStateWalk>();
            return;
        }

        if (_playerContext.Input.sneakPressed && _playerContext.Input.moveInput.y == 1)
        {
            _fsm_movement.SetState<MoveStateSlide>();
            return;
        }

        inputDir = new Vector3(_playerContext.Input.moveInput.x, 0, _playerContext.Input.moveInput.y).normalized;

        worldDir = _playerContext.CharacterController.transform.TransformDirection(inputDir);

        _motor.SetHorizontalVelocity(worldDir, _playerContext.MovementConfig.RunSpeed);
    }
}
