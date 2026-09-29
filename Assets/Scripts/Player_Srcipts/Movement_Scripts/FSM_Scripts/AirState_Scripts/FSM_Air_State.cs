using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class FSM_Air_State
{
    protected readonly FSM_Air _fsm_air;
    protected readonly PlayerContext _playerContext;
    protected readonly PlayerMotor _motor;

    protected FSM_Air_State(FSM_Air fsm_air, PlayerContext playerContext, PlayerMotor motor)
    {
        _fsm_air = fsm_air;
        _playerContext = playerContext;
        _motor = motor;
    }

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}
