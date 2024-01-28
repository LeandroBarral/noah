
namespace LobaApps.Architecture.State
{
    using System;
    using System.Collections.Generic;

    public interface IStateMachine : IStateLifeCycle
    {
    }

    public interface IStateMachine<EState> : IStateMachine, IStateCreator<EState, IState<EState>>
        where EState : Enum
    {
        IDictionary<EState, IState<EState>> Cache { get; }
        EState CurrentKey { get; }
        IState<EState> Current { get; }
        ISet<IStateTransition<EState>> Transitions { get; }
    }

}