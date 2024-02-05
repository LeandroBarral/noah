namespace LobaApps
{
    using LobaApps.Architecture.Core;
    using UnityEngine;

    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour, IPlayer
    {
        [Header("References")]
        [SerializeField] InputReader inputReader;
        [SerializeField] PlayerAnimation playerAnimation;

        [Header("Grounded Settings")]
        [SerializeField] float walkSpeed = 5f;
        [SerializeField, Range(1f, 15f)] float rotationSmooth = 10f;
        [SerializeField] LayerMask groundLayer;

        [Header("Air Settings")]
        [SerializeField, Range(0, 30)] float maxJumpHeight = 2f;
        [SerializeField, Range(0, 30)] float maxJumpTime = 1f;
        [SerializeField, Min(0)] int maxJumps = 2;
        [SerializeField, Min(0)] float fallMultiplier = 2f;

        public InputReader InputReader => inputReader;

        CharacterController controller;
        PlayerStateMachine stateMachine;
        Health playerHealth;

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            playerHealth = GetComponent<Health>();

            PlayerStateMachine.Settings playerStateSettings = new(walkSpeed, rotationSmooth, maxJumpHeight, maxJumpTime, maxJumps, fallMultiplier, groundLayer);
            stateMachine = new PlayerStateMachine(this, inputReader, controller, playerAnimation, playerStateSettings);
        }

        void Start()
        {
            stateMachine.Start();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                playerHealth.Damage(playerHealth.Max / 10);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                playerHealth.Heal(playerHealth.Max / 10);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                playerHealth.FullHeal();
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                playerHealth.SetMaxHealth(1000);
            }

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
    }

}