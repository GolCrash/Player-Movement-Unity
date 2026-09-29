using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class FSM_Move_State
{
    protected readonly FSM_Movement _fsm_movement;
    protected readonly PlayerContext _playerContext;
    protected readonly PlayerMotor _motor;

    protected FSM_Move_State(FSM_Movement fsm_movement, PlayerContext playerContext, PlayerMotor motor)
    {
        _fsm_movement = fsm_movement;
        _playerContext = playerContext;
        _motor = motor;
    }

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}
