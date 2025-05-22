using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonWalker : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f; // �������� ������
    public float gravity = -9.81f; // ����������
    public Transform cameraHolder; // ������ �� ������ � �������
    public float mouseSensitivity = 2f; // ���������������� ����

    private CharacterController controller;
    private float xRotation;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // ������ � ������ ������
    }

    void Update()
    {
        // �������� ������ (����)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ����������� ���� �������

        Quaternion quaternion = Quaternion.Euler(xRotation, 0f, 0f);
        cameraHolder.localRotation = quaternion;
        transform.Rotate(Vector3.up * mouseX);

        // ������ (WASD)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        controller.Move(move * walkSpeed * Time.deltaTime);

        // ����������
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}