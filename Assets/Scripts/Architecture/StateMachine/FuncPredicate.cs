
namespace LobaApps.Architecture.State
{
    using System;

    public class FuncPredicate : IStateCondition
    {
        readonly Func<bool> predicate;

        public FuncPredicate(Func<bool> predicate)
        {
            this.predicate = predicate;
        }

        public bool Evaluate() => predicate();
    }
}