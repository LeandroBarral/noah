using System;
using System.Collections.Generic;

public interface IState<EState> where EState : Enum
{
    ISet<IStateTransition<EState>> Transitions { get; }

    void Enter();
    void Exit();
    void FixedUpdate();
    void Update();
}

public interface IStateTransition<EState> where EState : Enum
{
    EState To { get; }
    IStateCondition Condition { get; }
}

public class StateTransition<EState> : IStateTransition<EState> where EState : Enum
{
    public EState To { get; }
    public IStateCondition Condition { get; }

    public StateTransition(EState to, IStateCondition condition)
    {
        To = to;
        Condition = condition;
    }
}

public interface IStateCondition
{
    bool Evaluate();
}

public class FuncPredicate : IStateCondition
{
    readonly Func<bool> predicate;

    public FuncPredicate(Func<bool> predicate)
    {
        this.predicate = predicate;
    }

    public bool Evaluate() => predicate();
}