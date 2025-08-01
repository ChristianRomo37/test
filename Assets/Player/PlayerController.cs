using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    private PlayerInputController playerInputController;
    private Rigidbody rb;
    private float xRot;
    private Vector2 playerMouseInput;
    [SerializeField] private float sensitivity;
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float jumpForce;

    private void Awake()
    {
        playerInputController = GetComponent<PlayerInputController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerMouseInput = new Vector2(Mouse.current.delta.x.ReadValue(), Mouse.current.delta.y.ReadValue());

        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 moveVector = transform.TransformDirection(playerMouseInput) * speed;
        rb.linearVelocity = new Vector3(moveVector.x, rb.linearVelocity.y, moveVector.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    private void FixedUpdate()
    {
        Vector3 velocity = new Vector3(playerInputController.movementInputVector.x, 0f, playerInputController.movementInputVector.y) * speed;

        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }

}
