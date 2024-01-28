
namespace LobaApps.Architecture.State
{
    using System;
    using System.Collections.Generic;

    public interface IState<EState> : IStateLifeCycle
        where EState : Enum
    {
        ISet<IStateTransition<EState>> Transitions { get; }
    }
}