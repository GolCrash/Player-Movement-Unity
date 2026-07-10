using System;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Movement
{
    public FSM_Move_State currentState {  get; private set; }

    private Dictionary<Type, FSM_Move_State> _states = new Dictionary<Type, FSM_Move_State>();

    public void AddState(FSM_Move_State state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : FSM_Move_State
    {
        var type = typeof(T);

        if (currentState != null && currentState.GetType() == type)
            return;

        if (_states.TryGetValue(type, out var newState))
        {
            currentState?.ExitState();

            currentState = newState;

            currentState.EnterState();
        }
    }

    public void Update()
    {
        currentState?.UpdateState();
    }
}