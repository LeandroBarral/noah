
namespace LobaApps
{
    using System.Collections.Generic;
    using LobaApps.Architecture.State;
    using UnityEngine;

    internal class PlayerGroundedIdleState : IState<PlayerGroundedStateMachine.States>
    {
        public ISet<IStateTransition<PlayerGroundedStateMachine.States>> Transitions { get; }

        private readonly PlayerGroundedStateMachine stateMachine;

        public PlayerGroundedIdleState(PlayerGroundedStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
            Transitions = new HashSet<IStateTransition<PlayerGroundedStateMachine.States>> {
                new Transition<PlayerGroundedStateMachine.States>(
                    PlayerGroundedStateMachine.States.Run,
                    new FuncPredicate(() => this.stateMachine.Context.IsMovementPressed)
                ),
            };
        }

        public void Start()
        {
            Debug.Log(">> [Enter] State -> Idle");
            stateMachine.Context.Animator.CrossFade(PlayerAnimationHashes.Idle, 0.1f);
        }

        public void Exit()
        {
            Debug.Log("<< [Exit] State -> Idle");
        }

        public void Update()
        {
        }
    }
}