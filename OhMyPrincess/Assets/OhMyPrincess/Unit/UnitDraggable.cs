using UnityEngine;
using UnityEngine.EventSystems;

public class UnitDraggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Transform canvas;
    private CanvasGroup canvasGroup;
    private UnitDraggable unitDraggable;
    private RectTransform rectTransform;
    private Transform previosParent;
    private UnitType unitType;

    private void Awake()
    {
        canvas = FindFirstObjectByType<Canvas>().transform;
        canvasGroup = GetComponent<CanvasGroup>();
        unitDraggable = GetComponent<UnitDraggable>();
        rectTransform = GetComponent<RectTransform>();
        unitType = GetComponent<Unit>().unitType;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        previosParent = transform.parent;

        transform.SetParent(canvas);
        transform.SetAsLastSibling();

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    // RoadSlot의 OnDrop에서 유닛을 배치하면, 유닛의 부모를 RoadSlot으로 변경될거임. 이후 실행되는 OnEndDrag
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        //1이상인 이유는 자기도 들어갔다가 체크하는것이기 때문.
        if( !transform.parent.CompareTag(nameof(TagType.RoadSlot)) || transform.parent.childCount > 1)
        {
            transform.SetParent(previosParent);
            rectTransform.position = previosParent.GetComponent<RectTransform>().position;
        }
        else
        {
            rectTransform.position = transform.parent.GetComponent<RectTransform>().position;

            //구입 드래그이면 드래그로 배치된 후, 구입창에 새로운 유닛을 생성해줘야함.
            if(previosParent.CompareTag(nameof(TagType.UnitSlot)))
            {
                GameObject unit = ObjectPoolManager.Instance.GetUnitObject(unitType);
                unit.transform.SetParent(previosParent);
                unit.transform.GetComponent<RectTransform>().position = previosParent.GetComponent<RectTransform>().position;
            }
        }

    }
}
