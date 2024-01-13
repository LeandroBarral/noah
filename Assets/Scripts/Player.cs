using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField] float walkSpeed = 5f;
    [SerializeField, Range(0.01f, 1)] float rotationSmooth = .25f;

    Camera mainCamera;
    Vector3 inputMovement;
    Rigidbody rb;
    Animator animator;

    readonly int AnimationIdleHash = Animator.StringToHash("Idle");
    readonly int AnimationWalkHash = Animator.StringToHash("Walk");

    bool isIdle = true;
    bool isMoving = false;

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
        CameraFollow();
        ControlAnimation();
    }

    void ReadPlayerInput()
    {
        inputMovement.x = Input.GetAxis("Horizontal");
        inputMovement.z = Input.GetAxis("Vertical");
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

        if(inputMovement == Vector3.zero && !isIdle)
        {
            isIdle = true;
            isMoving = false;
            animator.CrossFade(AnimationIdleHash, .1f);
        }
    }
}