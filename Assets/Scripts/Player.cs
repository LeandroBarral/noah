using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5f;
    [SerializeField, Range(0.01f, 1)] float rotationSmooth = .25f;
    [SerializeField, Range(0, 30)] float jumpForce = 7.5f;
    [SerializeField] int maxJumps = 2;
    [SerializeField] LayerMask groundLayer;

    Camera mainCamera;
    Vector3 inputMovement;
    Rigidbody rb;
    Animator animator;

    readonly int AnimationIdleHash = Animator.StringToHash("Idle");
    readonly int AnimationWalkHash = Animator.StringToHash("Walk");

    readonly int AnimationJumpHash = Animator.StringToHash("Jump.JumpStart");
    readonly int AnimationLandingHash = Animator.StringToHash("Landing");

    bool isIdle = true;
    bool isMoving;
    [SerializeField] bool isJumping;
    [SerializeField] bool isGrounded;
    int jumpCounter = 0;

    void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        ReadPlayerInput();
        MovePlayer();
        Jump();
        CheckIsGrounded();
        CameraFollow();
        ControlAnimation();
    }

    void ReadPlayerInput()
    {
        inputMovement.x = Input.GetAxis("Horizontal");
        inputMovement.z = Input.GetAxis("Vertical");

        isJumping = Input.GetButtonDown("Jump");
    }

    void MovePlayer()
    {
        Vector3 movement = walkSpeed * Time.deltaTime * inputMovement;
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSmooth);
        }
        rb.MovePosition(rb.position + movement);
    }

    void CameraFollow()
    {
        Vector3 cameraPosition = transform.position + new Vector3(0, 3.5f, -3.75f);
        mainCamera.transform.position = cameraPosition;
        mainCamera.transform.LookAt(transform);
    }

    void ControlAnimation()
    {
        if (inputMovement != Vector3.zero && !isMoving)
        {
            isIdle = false;
            isMoving = true;
            animator.CrossFade(AnimationWalkHash, .1f);
        }

        if (inputMovement == Vector3.zero && !isIdle)
        {
            isIdle = true;
            isMoving = false;
            animator.CrossFade(AnimationIdleHash, .1f);
        }

        if (isJumping)
        {
            animator.CrossFade(AnimationJumpHash, .1f);
        }
    }

    void Jump()
    {
        if (maxJumps > 0 && jumpCounter < maxJumps && isJumping)
        {
            jumpCounter++;

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;

            if (jumpCounter == maxJumps)
            {
                jumpCounter = 0;
            }
        }
    }

    void CheckIsGrounded()
    {
        if (!isGrounded && Physics.Raycast(transform.position, Vector3.down, 1.5f, groundLayer))
        {
            Debug.Log("Landing");
            isJumping = false;
            isGrounded = true;
            animator.CrossFade(AnimationLandingHash, .1f);
        }
    }
}