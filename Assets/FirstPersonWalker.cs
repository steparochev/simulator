using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonWalker : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f; // Скорость ходьбы
    public float gravity = -9.81f; // Гравитация
    public Transform cameraHolder; // Ссылка на объект с камерой
    public float mouseSensitivity = 2f; // Чувствительность мыши

    private CharacterController controller;
    private float xRotation;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Курсор в центре экрана
    }

    void Update()
    {
        // Вращение камеры (мышь)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ограничение угла наклона

        Quaternion quaternion = Quaternion.Euler(xRotation, 0f, 0f);
        cameraHolder.localRotation = quaternion;
        transform.Rotate(Vector3.up * mouseX);

        // Ходьба (WASD)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * walkSpeed * Time.deltaTime);

        // Гравитация
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}