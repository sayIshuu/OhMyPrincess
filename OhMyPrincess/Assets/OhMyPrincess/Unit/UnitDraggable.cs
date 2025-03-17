using UnityEngine;

public class UnitDraggable : MonoBehaviour
{
    private Vector3 offset;
    //private Transform previousParent;
    private bool unBuied = true;
    private Vector3 originalPosition;
    private RoadSlot originalRoadSlot; // ���� RoadSlot�� ���� ����
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
        if(gameObject.tag != nameof(TagType.Unit))
        {
            return;
        }
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
        //transform.SetParent(null);
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

        if (roadSlot != null && !roadSlot.occupied)
        {
            //�����丵 �ʿ�. �ϴ� ��ɱ�������
            if(unBuied && !GoldManager.Instance.checkGold(GetComponent<Unit>().cost))
            {
                transform.position = originalPosition;
                return;
            }

            if(unitType == UnitType.Batch && roadSlot.roadSlotType != RoadSlotType.Heart)
            {
                transform.position = originalPosition;
                return;
            }

            if (originalRoadSlot != null)
            {
                originalRoadSlot.occupied = false;
                originalRoadSlot.boxCollider2D.enabled = true;
            }

            //�����ϴ� �巡���� ���
            if (unBuied)
            {
                GameObject unit = ObjectPoolManager.Instance.GetUnitObject(unitType);
                unit.transform.position = originalPosition;

                GoldManager.Instance.SpendGold(unit.GetComponent<Unit>().cost);
                unBuied = false;
            }

            //�Ҳɿ� �־������
            if (roadSlot.roadSlotType == RoadSlotType.Heart)
            {
                unBuied = true;
                GetComponent<Unit>().Burn();
                return;
            }

            //��ġ��
            roadSlot.occupied = true;
            transform.position = roadSlot.transform.position + new Vector3(30,-50,0); // RoadSlot ��ġ�� �̵�
            roadSlot.boxCollider2D.enabled = false;
            if(roadSlot.roadSlotType == RoadSlotType.Church)
            {
                GetComponent<UnitStress>().StartHeal();
            }
            else
            {
                GetComponent<UnitStress>().isHealing = false;
            }

            
            originalRoadSlot = roadSlot;
            originalPosition = transform.position;
        }
        else
        {
            transform.position = originalPosition; // ���� ��ġ�� ����
        }
    }

    private RoadSlot GetRoadSlotAtMousePosition()
    {
        Vector2 mousePosition = GetMouseWorldPosition(); // offset ����

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

    public void UnitDied()
    {
        if (originalRoadSlot != null)
        {
            originalRoadSlot.occupied = false;
            originalRoadSlot.boxCollider2D.enabled = true;
        }
    }
}
