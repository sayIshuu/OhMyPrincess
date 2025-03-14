using UnityEngine;

public class UnitDraggable : MonoBehaviour
{
    private Vector3 offset;
    //private Transform previousParent;
    private bool unBuied = true;
    private Vector3 originalPosition;
    private RoadSlot originalRoadSlot; // 기존 RoadSlot을 직접 저장
    private Camera mainCamera;
    private bool isDragging = false;
    private UnitType unitType;

    private void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position; // 드래그 전 부모 오브젝트 저장
    }

    private void Awake()
    {
        unitType = GetComponent<Unit>().unitType;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
        Debug.Log("OnMouseDown() 호출됨: " + gameObject.name);

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
            if (originalRoadSlot != null)
            {
                originalRoadSlot.occupied = false;
                originalRoadSlot.boxCollider2D.enabled = true;
            }

            roadSlot.occupied = true;
            transform.position = roadSlot.transform.position + new Vector3(30,-50,0); // RoadSlot 위치로 이동
            roadSlot.boxCollider2D.enabled = false;
            if (unBuied)
            {
                GameObject unit = ObjectPoolManager.Instance.GetUnitObject(unitType);
                unit.transform.position = originalPosition;
                unBuied = false;
            }
            originalRoadSlot = roadSlot;
            originalPosition = transform.position;
        }
        else
        {
            transform.position = originalPosition; // 원래 위치로 복귀
        }
    }

    private RoadSlot GetRoadSlotAtMousePosition()
    {
        Vector2 mousePosition = GetMouseWorldPosition(); // offset 제거

        // 여러 개의 충돌체를 감지하기 위해 RaycastAll 사용
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
        mousePosition.z = -mainCamera.transform.position.z; // 카메라 위치 보정
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
