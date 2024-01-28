
namespace LobaApps
{
    using System;
    using LobaApps.Architecture.State;
    using UnityEngine;

    public class PlayerStateMachine : HierarchicalStateMachine<PlayerStateMachine.Machines>
    {
        public readonly InputReader InputReader;
        public readonly CharacterController Controller;
        public readonly Settings MachineSettings;
        public readonly Animator Animator;
        public readonly Player Player;

        public Vector2 MovementInput;
        public Vector3 AppliedMovement;
        public bool IsMovementPressed => MovementInput.sqrMagnitude > 0.1f;
        public bool IsJumpPressed;

        private float rotationFactorPerFrame;

        public PlayerStateMachine(Player player, InputReader inputReader, CharacterController controller, Animator animator, Settings settings)
            : base(Machines.Grounded)
        {
            InputReader = inputReader;
            Controller = controller;
            Animator = animator;
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
        }

        public override void Update()
        {
            base.Update();

            ApplyGravity();
            
            HandleRotation();

            ApplyMovement();
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
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
        };

        private void ApplyMovement()
        {
            Controller.Move(Time.deltaTime * MachineSettings.WalkSpeed * AppliedMovement);
        }

        private void ApplyGravity()
        {
            AppliedMovement.y -= 9.81f * Time.deltaTime;
        }

        private void HandleRotation()
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
            Transitions.Add(new Transition<Machines>(Machines.Air, new FuncPredicate(() => IsJumpPressed)));
            Transitions.Add(new Transition<Machines>(Machines.Grounded, new FuncPredicate(() => Controller.isGrounded && !IsJumpPressed)));
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
            readonly public float JumpForce;
            readonly public int MaxJumps;
            readonly public LayerMask GroundLayer;

            public Settings(float walkSpeed, float rotationSmooth, float jumpForce, int maxJumps, LayerMask groundLayer)
            {
                WalkSpeed = walkSpeed;
                RotationSmooth = rotationSmooth;
                JumpForce = jumpForce;
                MaxJumps = maxJumps;
                GroundLayer = groundLayer;
            }
        }
    }
}