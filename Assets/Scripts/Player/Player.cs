namespace LobaApps
{
    using UnityEngine;

    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour, IPlayerEntity
    {
        [Header("References")]
        [SerializeField] InputReader inputReader;
        [SerializeField] Animator animator;
        [SerializeField] PlayerAnimation playerAnimation;

        [Header("Movement Settings")]
        [SerializeField] float walkSpeed = 5f;
        [SerializeField, Range(1f, 15f)] float rotationSmooth = 10f;
        [SerializeField, Range(0, 30)] float jumpForce = 7.5f;
        [SerializeField] int maxJumps = 2;
        [SerializeField] LayerMask groundLayer;

        public InputReader InputReader => inputReader;
        public PlayerAnimation Animations => playerAnimation;

        CharacterController controller;
        PlayerStateMachine stateMachine;

        void Awake()
        {
            controller = GetComponent<CharacterController>();

            PlayerStateMachine.Settings playerStateSettings = new(walkSpeed, rotationSmooth, jumpForce, maxJumps, groundLayer);
            stateMachine = new PlayerStateMachine(this, inputReader, controller, animator, playerStateSettings);
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
            // stateMachine.FixedUpdate();
        }

        void OnDestroy()
        {
            inputReader.Disable();
            stateMachine.Exit();
        }

        public void Attack()
        {
        }

        public void Jump()
        {
        }

        public void Move()
        {
        }

    }
}