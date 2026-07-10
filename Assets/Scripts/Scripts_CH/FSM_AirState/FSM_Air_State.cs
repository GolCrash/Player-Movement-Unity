using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class FSM_Air_State
{
    protected readonly FSM_Air _fsm_air;
    protected readonly PlayerContext _playerContext;

    protected FSM_Air_State(FSM_Air fsm_air, PlayerContext playerContext)
    {
        _fsm_air = fsm_air;
        _playerContext = playerContext;
    }

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}
