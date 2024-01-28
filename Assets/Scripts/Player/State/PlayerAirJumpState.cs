
namespace LobaApps
{
    using System.Collections.Generic;
    using LobaApps.Architecture.State;
    using UnityEngine;

    internal class PlayerAirJumpState : IState<PlayerAirStateMachine.States>
    {
        public ISet<IStateTransition<PlayerAirStateMachine.States>> Transitions { get; }

        private readonly PlayerAirStateMachine stateMachine;

        public PlayerAirJumpState(PlayerAirStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            Transitions = new HashSet<IStateTransition<PlayerAirStateMachine.States>>();
        }

        public void Start()
        {
            Debug.Log(">> [Enter] State -> Jump");
            stateMachine.Context.Animator.CrossFade(PlayerAnimationHashes.JumpStart, 0.1f);
        }

        public void Exit()
        {
            Debug.Log("<< [Exit] State -> Jump");
        }

        public void Update()
        {
        }
    }
}