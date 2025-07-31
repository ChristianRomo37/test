using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float sprintMod;
    public float sprintSpeed;
    float origSpeed;
    public bool isSprinting;

    public float groundDrag;

    [Header("JumpMovement")]
    public float jumpForce;
    public float jumpCoolDown;
    public float jumpLimit;
    float jumps;
    public float airMultiplier;
    public bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded; //= true;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    bool exitingSlope;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    PlayerHealth playerHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        origSpeed = moveSpeed;
        sprintSpeed = moveSpeed * sprintMod;
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, whatIsGround);
        if (!playerHealth.dead)
        {
        MyInput();
        SpeedControl();
        }

        if (IsGrounded())
        {
            rb.linearDamping = groundDrag;
            jumps = 0;
            exitingSlope = false;
        }
        else
            rb.linearDamping = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && IsGrounded() && jumps<=jumpLimit)
        {
            //readyToJump = false;

            Jump();

            //Invoke(nameof(ResetJump), jumpCoolDown);
        }

        if (Input.GetKey(sprintKey))
        {
            isSprinting = true;
            moveSpeed = sprintSpeed;
        }
        else // (!Input.GetKey(sprintKey))
        {
            isSprinting = false;
            moveSpeed = origSpeed;
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

            if (rb.linearVelocity.y > 0)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }

        if (IsGrounded())
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
        else if (!IsGrounded())
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);

        rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        if (OnSlope() && !exitingSlope)
        {
            if (rb.linearVelocity.magnitude > moveSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
            }
        }

        else
        { 
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.y);

            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.y);
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.1f, whatIsGround);
    }

    private void Jump()
    {

        exitingSlope = true;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        jumps++;
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void Sprint()
    {
        while (isSprinting)
        {
            moveSpeed *= sprintMod;
        }
        moveSpeed = origSpeed;
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.1f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
