using UnityEngine;

public class UnitDraggable : MonoBehaviour
{
    private Vector3 offset;
    //private Transform previousParent;
    private bool unBuied = true;
    private Vector3 originalPosition;
    private Camera mainCamera;
    private bool isDragging = false;
    private UnitType unitType;

    private void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position; // �巡�� �� �θ� ������Ʈ ����
    }

    private void Awake()
    {
        unitType = GetComponent<Unit>().unitType;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        //previousParent = transform.parent; // �巡�� �� �θ� ������Ʈ ����
        // ���콺 Ŭ�� ��ġ�� ������Ʈ ��ġ�� ���̸� ����
        offset = transform.position - GetMouseWorldPosition();

        // �巡�� ���� �� ���� ���� �ø���
        transform.SetParent(null);
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }

    }
    private void OnMouseUp()
    {
        isDragging = false;

        RoadSlot roadSlot = GetRoadSlotAtMousePosition();
        Debug.Log(roadSlot);
        if (roadSlot != null)
        {
            transform.position = roadSlot.transform.position + new Vector3(30,-50,0); // RoadSlot ��ġ�� �̵�
            if(unBuied)
            {
                GameObject unit = ObjectPoolManager.Instance.GetUnitObject(unitType);
                unit.transform.position = originalPosition;
            }
            originalPosition = transform.position;
            unBuied = false;
        }
        else
        {
            transform.position = originalPosition; // ���� ��ġ�� ����
        }
    }

    private RoadSlot GetRoadSlotAtMousePosition()
    {
        Vector2 mousePosition = GetMouseWorldPosition(); // offset ����

        // Debug: Ray�� �׷��� ����� ����� Ȯ��
        Debug.DrawRay(mousePosition, Vector2.zero, Color.red, 2f);

        // ���� ���� �浹ü�� �����ϱ� ���� RaycastAll ���
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag(nameof(TagType.RoadSlot)))
            {
                return hit.collider.GetComponent<RoadSlot>();
            }
        }

        return null;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -mainCamera.transform.position.z; // ī�޶� ��ġ ����
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}
