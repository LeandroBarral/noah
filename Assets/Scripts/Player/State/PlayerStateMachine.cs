
namespace LobaApps
{
    using System;
    using LobaApps.Architecture.Core;
    using LobaApps.Architecture.State;
    using UnityEngine;

    public class PlayerStateMachine : HierarchicalStateMachine<PlayerStateMachine.Machines>
    {
        public readonly Settings MachineSettings;
        public readonly PlayerAnimation PlayerAnimation;
        public readonly Player Player;
        readonly InputReader InputReader;
        readonly CharacterController Controller;

        public Vector2 MovementInput;
        public Vector3 AppliedMovement;
        public bool IsJumpPressed;

        float gravity = -9.81f;
        readonly float groundedGravity = -0.05f;

        float initialJumpVelocity;

        public bool IsJumping { get; private set; } = false;
        public bool IsMovementPressed => MovementInput.sqrMagnitude > 0.1f;
        public bool IsFalling => AppliedMovement.y < 0.0f && !IsJumpPressed;

        public PlayerStateMachine(
            Player player,
            InputReader inputReader,
            CharacterController controller,
            PlayerAnimation playerAnimation,
            Settings settings
        ) : base(Machines.Grounded)
        {
            InputReader = inputReader;
            Controller = controller;
            PlayerAnimation = playerAnimation;
            MachineSettings = settings;
            Player = player;

            BuildTransitions();
        }

        public override void Start()
        {
            base.Start();

            InputReader.OnJump += OnJumpHandler;
            InputReader.OnMove += OnMoveHandler;

            InputReader.EnableGameplay();

            SetupJump();
        }

        private void SetupJump()
        {
            float timeToApex = MachineSettings.MaxJumpTime / 2;
            gravity = -2 * MachineSettings.MaxJumpHeight / Mathf.Pow(timeToApex, 2);
            initialJumpVelocity = 2 * MachineSettings.MaxJumpHeight / timeToApex;
        }

        public override void Update()
        {
            base.Update();

            ApplyRotation();
            ApplyMovement();
            ApplyGravity();
            ApplyJump();
        }

        public override void Exit()
        {
            base.Exit();

            InputReader.OnJump -= OnJumpHandler;
            InputReader.OnMove -= OnMoveHandler;
        }

        public override IStateMachine Factory(Machines type) => type switch
        {
            Machines.Grounded => new PlayerGroundedStateMachine(this),
            Machines.Air => new PlayerAirStateMachine(this),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid state type"),
        };

        private void ApplyJump()
        {
            if (IsJumpPressed && !IsJumping && Controller.isGrounded)
            {
                IsJumping = true;
                float previousYVelocity = AppliedMovement.y;
                float newYVelocity = previousYVelocity + initialJumpVelocity;
                float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
                AppliedMovement.y = nextYVelocity;
            }
            else if (!IsJumpPressed && IsJumping && Controller.isGrounded)
            {
                IsJumping = false;
                IsJumpPressed = false;
            }
        }

        private void ApplyMovement()
        {
            Controller.Move(Time.deltaTime * MachineSettings.WalkSpeed * AppliedMovement);
        }

        private void ApplyGravity()
        {
            if (Controller.isGrounded)
            {
                AppliedMovement.y = groundedGravity;
            }
            else if (IsFalling)
            {
                float previousYVelocity = AppliedMovement.y;
                float newYVelocity = previousYVelocity + (gravity * MachineSettings.FallMultiplier * Time.deltaTime);
                float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
                AppliedMovement.y = nextYVelocity;
            }
            else
            {
                float previousYVelocity = AppliedMovement.y;
                float newYVelocity = previousYVelocity + (gravity * Time.deltaTime);
                float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
                AppliedMovement.y = nextYVelocity;
            }
        }

        private void ApplyRotation()
        {
            if (IsMovementPressed)
            {
                Vector3 positionToLookAt;

                positionToLookAt.x = AppliedMovement.x;
                positionToLookAt.y = 0;
                positionToLookAt.z = AppliedMovement.z;

                Quaternion currentRotation = Player.transform.rotation;

                Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);

                Player.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, MachineSettings.RotationSmooth * Time.deltaTime);
            }
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

        private void BuildTransitions()
        {
            Transitions.Add(new Transition<Machines>(Machines.Air, new FuncPredicate(() => !Controller.isGrounded)));
            Transitions.Add(new Transition<Machines>(Machines.Grounded, new FuncPredicate(() => Controller.isGrounded)));
        }

        public enum Machines
        {
            Grounded,
            Air,
        }

        [Serializable]
        public class Settings
        {
            readonly public float WalkSpeed;
            readonly public float RotationSmooth;
            readonly public float MaxJumpHeight;
            readonly public float MaxJumpTime;
            readonly public int MaxJumps;
            readonly public float FallMultiplier;
            readonly public LayerMask GroundLayer;

            public Settings(float walkSpeed, float rotationSmooth, float maxJumpHeight, float maxJumpTime, int maxJumps, float fallMultiplier, LayerMask groundLayer)
            {
                WalkSpeed = walkSpeed;
                RotationSmooth = rotationSmooth;
                MaxJumpHeight = maxJumpHeight;
                MaxJumpTime = maxJumpTime;
                MaxJumps = maxJumps;
                FallMultiplier = fallMultiplier;
                GroundLayer = groundLayer;
            }
        }
    }
}