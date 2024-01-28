
namespace LobaApps.Architecture.State
{
    using System;
    using System.Collections.Generic;

    public interface IHierarchicalStateMachine<EStateMachine> : IStateLifeCycle, IStateCreator<EStateMachine, IStateMachine>
        where EStateMachine : Enum
    {
        IDictionary<EStateMachine, IStateMachine> Cache { get; }
        EStateMachine CurrentKey { get; }
        IStateMachine Current { get; }
        ISet<IStateTransition<EStateMachine>> Transitions { get; }
    }
}