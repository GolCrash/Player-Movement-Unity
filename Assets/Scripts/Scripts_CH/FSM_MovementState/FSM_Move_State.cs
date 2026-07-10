using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class FSM_Move_State
{
    protected readonly FSM_Movement _fsm_movement;
    protected readonly PlayerContext _playerContext;

    protected FSM_Move_State(FSM_Movement fsm_movement, PlayerContext playerContext)
    {
        _fsm_movement = fsm_movement;
        _playerContext = playerContext;
    }

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void ExitState() { }
}
