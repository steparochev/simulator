using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class HumanController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float rotationSpeed = 120f;

    private CharacterController controller;
    private float verticalVelocity;
    private float gravity = -9.81f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        ApplyGravity();
    }

    void HandleMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = transform.forward * verticalInput;
        controller.Move(moveDirection * walkSpeed * Time.deltaTime);
    }

    void HandleRotation()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotationAmount, 0);
    }

    void ApplyGravity()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        controller.Move(new Vector3(0, verticalVelocity * Time.deltaTime, 0));
    }
}