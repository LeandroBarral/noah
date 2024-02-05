
namespace LobaApps
{
    using System;
    using LobaApps.Architecture.State;

    public class PlayerAirStateMachine : FiniteStateMachine<PlayerAirStateMachine.States>
    {
        public readonly PlayerStateMachine Context;

        public PlayerAirStateMachine(PlayerStateMachine context) : base(States.Fall)
        {
            Context = context;
            Transitions.Add(new Transition<States>(States.Jump, new FuncPredicate(() => Context.IsJumping)));
            Transitions.Add(new Transition<States>(States.Fall, new FuncPredicate(() => Context.IsFalling)));
        }

        public override void Exit()
        {
            base.Exit();

            Context.PlayerAnimation.Landing();
        }

        public override IState<States> Factory(States stateKey) => stateKey switch
        {
            States.Fall => new PlayerAirFallState(this),
            States.Jump => new PlayerAirJumpState(this),
            _ => throw new ArgumentOutOfRangeException(nameof(stateKey), stateKey, null),
        };

        public enum States
        {
            Fall,
            Jump,
        }
    }
}