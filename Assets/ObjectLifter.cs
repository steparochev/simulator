using UnityEngine;

public class ObjectLifter : MonoBehaviour
{
    [Header("Настройки")]
    public float pickupDistance = 3f; // Дистанция подбора
    public float holdDistance = 2f; // Дистанция удержания
    public float liftSpeed = 5f; // Скорость подъема

    [Header("Ссылки")]
    public Transform cameraTransform; // Камера игрока
    public LayerMask pickupLayer; // Слой объектов для подбора

    private GameObject heldObject; // Удерживаемый объект
    private Rigidbody heldObjectRb;
    private Vector3 targetHoldPosition;
    private bool isHolding = false;

    void Update()
    {
        // Проверка нажатия кнопки подбора (E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHolding)
                TryPickupObject();
            else
                DropObject();
        }

        // Если объект удерживается
        if (isHolding)
        {
            // Плавное перемещение к точке удержания
            if (heldObjectRb != null)
            {
                targetHoldPosition = cameraTransform.position + cameraTransform.forward * holdDistance;
                Vector3 moveDirection = targetHoldPosition - heldObjectRb.position;
                heldObjectRb.velocity = moveDirection * liftSpeed;
            }
        }
    }

    void TryPickupObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, pickupDistance, pickupLayer))
        {
            // Проверяем, что объект - капсула
            if (hit.collider.gameObject.CompareTag("Pickable"))
            {
                heldObject = hit.collider.gameObject;
                heldObjectRb = heldObject.GetComponent<Rigidbody>();

                if (heldObjectRb != null)
                {
                    // Настраиваем физику для удержания
                    heldObjectRb.useGravity = false;
                    heldObjectRb.drag = 10f;
                    heldObjectRb.angularDrag = 10f;
                    isHolding = true;
                }
            }
        }
    }

    void DropObject()
    {
        if (heldObjectRb != null)
        {
            // Возвращаем физические свойства
            heldObjectRb.useGravity = true;
            heldObjectRb.drag = 1f;
            heldObjectRb.angularDrag = 0.5f;
            heldObjectRb.velocity = Vector3.zero;
        }

        isHolding = false;
        heldObject = null;
        heldObjectRb = null;
    }
}