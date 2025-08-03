using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    PlayerHealth playerHealth;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        playerHealth = GetComponentInParent<PlayerHealth>();
    }

    private void Update()
    {
        if (!playerHealth.dead)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -40f, 40f);

            //GameObject.FindGameObjectWithTag("Player").transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            orientation.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            transform.parent.parent.Rotate(Vector3.up, mouseX);
        }

    }

}
