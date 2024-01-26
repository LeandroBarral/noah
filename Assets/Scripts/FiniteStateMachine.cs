using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class FiniteStateMachine<EState> where EState : Enum
{
    protected Dictionary<EState, IState<EState>> CachedStates { get; }
    protected HashSet<IStateTransition<EState>> AnyTransitions { get; }

    protected EState CurrentStateKey { get; private set; }
    protected IState<EState> CurrentState { get; private set; }

    protected FiniteStateMachine(EState initialState)
    {
        CachedStates = new Dictionary<EState, IState<EState>>();
        AnyTransitions = new HashSet<IStateTransition<EState>>();

        if (TryGetOrBuildState(initialState, out IState<EState> state))
        {
            CurrentStateKey = initialState;
            CurrentState = state;
        }
    }

    protected abstract IState<EState> StateFactory(EState stateKey);
    
    public virtual void Start()
    {
        CurrentState.Enter();
    }

    public virtual void Update()
    {
        if (TryGetTransition(out IStateTransition<EState> transition))
        {
            StartTransition(transition.To);
        }

        CurrentState.Update();
    }

    public virtual void FixedUpdate()
    {
        CurrentState.FixedUpdate();
    }

    public virtual void Exit()
    {
        CurrentState.Exit();
    }

    public void StartTransition(EState nextStateKey)
    {
        if (CurrentStateKey.Equals(nextStateKey))
        {
            return;
        }

        if (TryGetOrBuildState(nextStateKey, out IState<EState> nextState))
        {
            CurrentState.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
        }
    }

    public void AddTransition(EState from, EState to, IStateCondition condition)
    {
        if (TryGetOrBuildState(from, out IState<EState> fromState))
        {
            fromState.Transitions.Add(new StateTransition<EState>(to, condition));
        }
    }

    public void AddAnyTransition(EState to, IStateCondition condition)
    {
        AnyTransitions.Add(new StateTransition<EState>(to, condition));
    }

    private bool TryGetOrBuildState(EState stateKey, out IState<EState> state)
    {
        if (CachedStates.TryGetValue(stateKey, out state))
        {
            return true;
        }

        try
        {
            state = StateFactory(stateKey);
            CachedStates.Add(stateKey, state);

            return true;
        }
        catch
        {
            Debug.LogError($"State {stateKey} not found in cache and cannot be built");

            return false;
        }
    }

    private bool TryGetTransition(out IStateTransition<EState> transition)
    {
        foreach (IStateTransition<EState> anyTransition in AnyTransitions)
        {
            if (anyTransition.Condition.Evaluate())
            {
                transition = anyTransition;
                return true;
            }
        }

        foreach (IStateTransition<EState> currentStateTransition in CurrentState.Transitions)
        {
            if (currentStateTransition.Condition.Evaluate())
            {
                transition = currentStateTransition;
                return true;
            }
        }

        transition = null;
        return false;
    }
}
