
namespace LobaApps
{
    using System.Collections.Generic;
    using LobaApps.Architecture.State;

    internal class PlayerAirFallState : IState<PlayerAirStateMachine.States>
    {
        public ISet<IStateTransition<PlayerAirStateMachine.States>> Transitions { get; }

        private readonly PlayerAirStateMachine stateMachine;

        public PlayerAirFallState(PlayerAirStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            Transitions = new HashSet<IStateTransition<PlayerAirStateMachine.States>>();
        }

        public void Start()
        {
            // Debug.Log(">> [Enter] State -> Fall");
            stateMachine.Context.PlayerAnimation.Falling(1);
        }

        public void Exit()
        {
            // Debug.Log("<< [Exit] State -> Fall");
        }

        public void Update()
        {
        }
    }
}