using System;
using System.Collections.Generic;
using UnityEngine;

public class FSM_Air
{
    public bool isActive { get; set; } = true;

    public FSM_Air_State currentState {  get; private set; }

    private Dictionary<Type, FSM_Air_State> _states = new Dictionary<Type, FSM_Air_State>();

    public void AddState(FSM_Air_State state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<T>() where T : FSM_Air_State
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
        if (isActive)
            currentState?.UpdateState();
    }
}