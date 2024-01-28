
namespace LobaApps.Architecture.State
{
    using System;

    public class Transition<EState> : IStateTransition<EState> where EState : Enum
    {
        public EState Target { get; }
        public IStateCondition Condition { get; }

        public Transition(EState target, IStateCondition condition)
        {
            Target = target;
            Condition = condition;
        }
    }
}