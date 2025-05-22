using UnityEngine;

public class ObjectLifter : MonoBehaviour
{
    [Header("���������")]
    public float pickupDistance = 3f; // ��������� �������
    public float holdDistance = 2f; // ��������� ���������
    public float liftSpeed = 5f; // �������� �������

    [Header("������")]
    public Transform cameraTransform; // ������ ������
    public LayerMask pickupLayer; // ���� �������� ��� �������

    private GameObject heldObject; // ������������ ������
    private Rigidbody heldObjectRb;
    private Vector3 targetHoldPosition;
    private bool isHolding = false;

    void Update()
    {
        // �������� ������� ������ ������� (E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isHolding)
                TryPickupObject();
            else
                DropObject();
        }

        // ���� ������ ������������
        if (isHolding)
        {
            // ������� ����������� � ����� ���������
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
            // ���������, ��� ������ - �������
            if (hit.collider.gameObject.CompareTag("Pickable"))
            {
                heldObject = hit.collider.gameObject;
                heldObjectRb = heldObject.GetComponent<Rigidbody>();

                if (heldObjectRb != null)
                {
                    // ����������� ������ ��� ���������
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
            // ���������� ���������� ��������
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