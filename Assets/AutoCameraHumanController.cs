using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AutoCameraHumanController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 2f;
    public float rotationSpeed = 80f;

    [Header("Camera Settings")]
    public float cameraHeight = 0.9f;
    public float mouseSensitivity = 1f;
    public float maxVerticalAngle = 80f;

    private CharacterController controller;
    private GameObject cameraPivot;
    private float verticalRotation;
    private float verticalVelocity;
    private const float gravity = -9.81f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        SetupCamera();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void SetupCamera()
    {
        // Автоматическое создание камеры
        cameraPivot = new GameObject("CameraPivot");
        cameraPivot.transform.SetParent(transform);
        cameraPivot.transform.localPosition = new Vector3(0, cameraHeight, 0);

        // Найти или создать камеру
        Camera playerCamera = cameraPivot.GetComponentInChildren<Camera>();
        if (playerCamera == null)
        {
            GameObject cameraObj = new GameObject("PlayerCamera");
            cameraObj.AddComponent<Camera>();
            cameraObj.transform.SetParent(cameraPivot.transform);
            cameraObj.transform.localPosition = Vector3.zero;
        }
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleCamera();
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

    void HandleCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Горизонтальное вращение персонажа
        transform.Rotate(0, mouseX, 0);

        // Вертикальное вращение камеры
        verticalRotation = Mathf.Clamp(verticalRotation - mouseY, -maxVerticalAngle, maxVerticalAngle);
        cameraPivot.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    void ApplyGravity()
    {
        verticalVelocity = controller.isGrounded ? -1f : verticalVelocity + gravity * Time.deltaTime;
        controller.Move(new Vector3(0, verticalVelocity * Time.deltaTime, 0));
    }
}