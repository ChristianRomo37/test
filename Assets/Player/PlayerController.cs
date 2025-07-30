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

    private void Awake()
    {
        playerInputController = GetComponent<PlayerInputController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerMouseInput = new Vector2(Mouse.current.delta.x.ReadValue(), Mouse.current.delta.y.ReadValue());

        MovePlayerCamera();
    }

    private void MovePlayerCamera()
    {
        xRot -= playerMouseInput.y * sensitivity;
        //xRot = Mathf.Clamp(xRot, 90, 90);

        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);

    }

    private void FixedUpdate()
    {
        Vector3 velocity = new Vector3(playerInputController.movementInputVector.x, 0f, playerInputController.movementInputVector.y) * speed;

        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }
}
