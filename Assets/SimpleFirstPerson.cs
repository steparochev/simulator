using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleFirstPerson : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Camera")]
    public Transform cameraHolder;
    public float minVerticalAngle = -90f;
    public float maxVerticalAngle = 90f;

    [Header("Object Interaction")]
    public float pickUpDistance = 3f;
    public float throwForce = 10f;
    public Transform holdPosition; // Позиция, где будет удерживаться предмет

    private CharacterController controller;
    private float xRotation = 0f;
    private Vector3 velocity;
    private bool isGrounded;
    private GameObject heldObject; // Текущий удерживаемый предмет
    private Rigidbody heldObjectRb; // Rigidbody удерживаемого предмета
 

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cameraHolder == null)
        {
            cameraHolder = transform.Find("CameraHolder");
            if (cameraHolder == null)
            {
                Debug.LogError("CameraHolder not found! Make sure it's a child object named 'CameraHolder'.");
                enabled = false;
                return;
            }
        }

        // Создаем позицию для удержания предмета, если не назначена
        if (holdPosition == null)
        {
            GameObject holdPosObj = new GameObject("HoldPosition");
            holdPosObj.transform.SetParent(cameraHolder);
            holdPosObj.transform.localPosition = new Vector3(0, 0, 1f); // 1 метр перед камерой
            holdPosition = holdPosObj.transform;
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Проверка нахождения на земле
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Вращение камеры (мышь)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);
        cameraHolder.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Прыжок
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Гравитация
        velocity.y += gravity * Time.deltaTime;

        // Ходьба (WASD)
        Vector3 move =
            transform.right * Input.GetAxis("Horizontal") +
            transform.forward * Input.GetAxis("Vertical");

        // Применяем движение с учетом гравитации
        controller.Move((move * walkSpeed + velocity) * Time.deltaTime);

        // Взаимодействие с предметами
        HandleObjectInteraction();
    }

    private void HandleObjectInteraction()
    {
        // Подобрать/отпустить предмет
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryPickUpObject();
            }
            else
            {
                DropObject();
            }
        }

        // Бросить предмет
        if (Input.GetMouseButtonDown(0) && heldObject != null)
        {
            ThrowObject();
        }

        // Удерживание предмета перед собой
        if (heldObject != null)
        {
            heldObject.transform.position = holdPosition.position;
            heldObject.transform.rotation = holdPosition.rotation;
        }
    }

    private void TryPickUpObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraHolder.position, cameraHolder.forward, out hit, pickUpDistance))
        {
            if (hit.collider.CompareTag("Pickable") || hit.rigidbody != null)
            {
                heldObject = hit.collider.gameObject;
                heldObjectRb = heldObject.GetComponent<Rigidbody>();

                if (heldObjectRb != null)
                {
                    heldObjectRb.isKinematic = true;
                    heldObjectRb.useGravity = false;
                }

                // Отключаем коллайдеры, чтобы предмет не мешал движению
                Collider[] colliders = heldObject.GetComponents<Collider>();
                foreach (Collider col in colliders)
                {
                    col.enabled = false;
                }
            }
        }
    }

    private void DropObject()
    {
        if (heldObjectRb != null)
        {
            heldObjectRb.isKinematic = false;
            heldObjectRb.useGravity = true;
        }

        // Включаем коллайдеры обратно
        Collider[] colliders = heldObject.GetComponents<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }

        heldObject = null;
        heldObjectRb = null;
    }

    private void ThrowObject()
    {
        if (heldObjectRb != null)
        {
            DropObject();
            heldObjectRb.AddForce(cameraHolder.forward * throwForce, ForceMode.Impulse);
        }
    }
}