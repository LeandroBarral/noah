using System;
using UnityEngine;

public class PlayerStateMachine : FiniteStateMachine<PlayerStateMachine.PlayerStates>
{
    readonly InputReader inputReader;
    readonly CharacterController controller;
    float walkSpeed;
    float rotationSmooth;
    float jumpForce;
    int maxJumps;
    LayerMask groundLayer;

    public Vector2 MovementInput;
    public Vector3 AppliedMovement;
    public bool IsMovementPressed => MovementInput.sqrMagnitude > 0.1f;
    public bool IsJumpPressed;

    PlayerStateMachine(InputReader inputReader, CharacterController controller)
        : base(PlayerStates.Grounded)
    {
        this.inputReader = inputReader;
        this.controller = controller;
    }

    public static PlayerStateMachine Create(InputReader inputReader, CharacterController controller)
    {
        return new PlayerStateMachine(inputReader, controller);
    }

    public PlayerStateMachine Settings(float walkSpeed, float rotationSmooth, float jumpForce, int maxJumps, LayerMask groundLayer)
    {
        this.walkSpeed = walkSpeed;
        this.rotationSmooth = rotationSmooth;
        this.jumpForce = jumpForce;
        this.maxJumps = maxJumps;
        this.groundLayer = groundLayer;

        return this;
    }

    public override void Start()
    {
        base.Start();

        inputReader.OnJump += OnJumpHandler;
        inputReader.OnMove += OnMoveHandler;
    }

    public override void Update()
    {
        base.Update();

        ApplyMovement();
    }

    public override void Exit()
    {
        base.Exit();

        inputReader.OnJump -= OnJumpHandler;
        inputReader.OnMove -= OnMoveHandler;
    }

    protected override IState<PlayerStates> StateFactory(PlayerStates stateKey) => stateKey switch
    {
        PlayerStates.Grounded => new GroundedState(this),
        // case PlayerStates.Idle:
        //     return new IdleState();
        // case PlayerStates.Walking:
        //     return new WalkingState();
        PlayerStates.Jumping => new JumpingState(this),
        _ => throw new NotImplementedException($"StateFactory for State->'{stateKey}' was not implemented"),
    };

    private void ApplyMovement()
    {
        controller.Move(Time.deltaTime * walkSpeed * AppliedMovement);
    }

    private void OnJumpHandler(bool isJumpPressed)
    {
        IsJumpPressed = isJumpPressed;
    }

    private void OnMoveHandler(Vector2 moveDirection)
    {
        MovementInput = moveDirection;

        AppliedMovement.x = MovementInput.x;
        AppliedMovement.z = MovementInput.y;
    }

    public enum PlayerStates
    {
        Grounded,
        Idle,
        Walking,
        Jumping
    }
}
