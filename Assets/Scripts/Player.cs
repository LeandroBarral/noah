using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] InputReader inputReader;

    [Header("Movement Settings")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField, Range(0.01f, 1)] float rotationSmooth = .25f;
    [SerializeField, Range(0, 30)] float jumpForce = 7.5f;
    [SerializeField] int maxJumps = 2;
    [SerializeField] LayerMask groundLayer;

    [Header("Debug")]
    [SerializeField] bool isJumpRequested = false;
    [SerializeField] int jumpCounter = 0;

    public InputReader InputReader => inputReader;

    CharacterController controller;
    PlayerStateMachine stateMachine;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        stateMachine = PlayerStateMachine
                        .Create(inputReader, controller)
                        .Settings(walkSpeed, rotationSmooth, jumpForce, maxJumps, groundLayer);
    }

    void OnEnable()
    {
        inputReader.EnableGameplay();
    }

    void Start()
    {
        stateMachine.Start();
    }

    void Update()
    {
        stateMachine.Update();
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    void OnDisable()
    {
        inputReader.Disable();
    }

    void OnDestroy()
    {
        stateMachine.Exit();
    }

    internal class PlayerAnimationHashes
    {
        public readonly int AnimationIdleHash = Animator.StringToHash("Idle");
        public readonly int AnimationWalkHash = Animator.StringToHash("Walk");
        public readonly int AnimationJumpHash = Animator.StringToHash("Jump.JumpStart");
        public readonly int AnimationLandingHash = Animator.StringToHash("Landing");
    }
}