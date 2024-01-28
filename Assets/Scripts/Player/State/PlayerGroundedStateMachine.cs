
namespace LobaApps
{
    using System;
    using LobaApps.Architecture.State;

    public class PlayerGroundedStateMachine : FiniteStateMachine<PlayerGroundedStateMachine.States>
    {
        public readonly PlayerStateMachine Context;

        public PlayerGroundedStateMachine(PlayerStateMachine context) : base(States.Idle)
        {
            Context = context;
            Transitions.Add(new Transition<States>(States.Run, new FuncPredicate(() => Context.IsMovementPressed)));
        }

        public override IState<States> Factory(States stateKey) => stateKey switch
        {
            States.Idle => new PlayerGroundedIdleState(this),
            States.Run => new PlayerGroundedRunState(this),
            _ => throw new ArgumentOutOfRangeException(nameof(stateKey), stateKey, null),
        };

        public enum States
        {
            Idle,
            // Walk,
            Run,
        }
    }
}