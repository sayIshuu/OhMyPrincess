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
        originalPosition = transform.position; // 드래그 전 부모 오브젝트 저장
    }

    private void Awake()
    {
        unitType = GetComponent<Unit>().unitType;
    }

    private void OnMouseDown()
    {
        isDragging = true;
        //previousParent = transform.parent; // 드래그 전 부모 오브젝트 저장
        // 마우스 클릭 위치와 오브젝트 위치의 차이를 저장
        offset = transform.position - GetMouseWorldPosition();

        // 드래그 시작 시 가장 위로 올리기
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
            transform.position = roadSlot.transform.position + new Vector3(30,-50,0); // RoadSlot 위치로 이동
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
            transform.position = originalPosition; // 원래 위치로 복귀
        }
    }

    private RoadSlot GetRoadSlotAtMousePosition()
    {
        Vector2 mousePosition = GetMouseWorldPosition(); // offset 제거

        // Debug: Ray를 그려서 제대로 쏘는지 확인
        Debug.DrawRay(mousePosition, Vector2.zero, Color.red, 2f);

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
}
