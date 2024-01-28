
namespace LobaApps.Architecture.State
{
    using System;

    public interface IStateTransition<EState> where EState : Enum
    {
        EState Target { get; }
        IStateCondition Condition { get; }
    }
}