
namespace LobaApps
{
    using System.Collections.Generic;
    using LobaApps.Architecture.State;
    using UnityEngine;

    internal class PlayerGroundedRunState : IState<PlayerGroundedStateMachine.States>
    {
        public ISet<IStateTransition<PlayerGroundedStateMachine.States>> Transitions { get; }

        private readonly PlayerGroundedStateMachine stateMachine;

        public PlayerGroundedRunState(PlayerGroundedStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            Transitions = new HashSet<IStateTransition<PlayerGroundedStateMachine.States>>(){
                new Transition<PlayerGroundedStateMachine.States>(
                    PlayerGroundedStateMachine.States.Idle,
                    new FuncPredicate(() => !this.stateMachine.Context.IsMovementPressed)
                ),
            };
        }

        public void Start()
        {
            // Debug.Log(">> [Enter] State -> Run");
            stateMachine.Context.PlayerAnimation.Run();
        }

        public void Exit()
        {
            // Debug.Log("<< [Exit] State -> Run");
        }

        public void Update()
        {
        }
    }
}