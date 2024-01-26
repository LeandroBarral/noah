using System.Collections.Generic;
using UnityEngine;

internal class GroundedState : IState<PlayerStateMachine.PlayerStates>
{
    public ISet<IStateTransition<PlayerStateMachine.PlayerStates>> Transitions { get; }

    readonly PlayerStateMachine context;

    public GroundedState(PlayerStateMachine context)
    {
        this.context = context;
        Transitions = new HashSet<IStateTransition<PlayerStateMachine.PlayerStates>>(){
            new StateTransition<PlayerStateMachine.PlayerStates>(
                PlayerStateMachine.PlayerStates.Jumping,
                new FuncPredicate(() => context.IsJumpPressed)
            )
        };
    }

    public void Enter()
    {
        Debug.Log("GroundedState Enter");
    }

    public void Exit()
    {
        Debug.Log("GroundedState Exit");
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
    }
}

internal class JumpingState : IState<PlayerStateMachine.PlayerStates>
{
    public ISet<IStateTransition<PlayerStateMachine.PlayerStates>> Transitions { get; }

    PlayerStateMachine context;

    public JumpingState(PlayerStateMachine context)
    {
        this.context = context;
        Transitions = new HashSet<IStateTransition<PlayerStateMachine.PlayerStates>>(){
            new StateTransition<PlayerStateMachine.PlayerStates>(
                PlayerStateMachine.PlayerStates.Grounded,
                new FuncPredicate(() => context.MovementInput.y <= 0)
            )
        };
    }

    public void Enter()
    {
        Debug.Log("JumpingState Enter");
    }

    public void Exit()
    {
    }

    public void FixedUpdate()
    {
    }

    public void Update()
    {
    }
}