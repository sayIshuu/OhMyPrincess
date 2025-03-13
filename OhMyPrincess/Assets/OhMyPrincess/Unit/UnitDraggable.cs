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

    // RoadSlot�� OnDrop���� ������ ��ġ�ϸ�, ������ �θ� RoadSlot���� ����ɰ���. ���� ����Ǵ� OnEndDrag
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        //1�̻��� ������ �ڱ⵵ ���ٰ� üũ�ϴ°��̱� ����.
        if( !transform.parent.CompareTag(nameof(TagType.RoadSlot)) || transform.parent.childCount > 1)
        {
            transform.SetParent(previosParent);
            rectTransform.position = previosParent.GetComponent<RectTransform>().position;
        }
        else
        {
            rectTransform.position = transform.parent.GetComponent<RectTransform>().position;

            //���� �巡���̸� �巡�׷� ��ġ�� ��, ����â�� ���ο� ������ �����������.
            if(previosParent.CompareTag(nameof(TagType.UnitSlot)))
            {
                GameObject unit = ObjectPoolManager.Instance.GetUnitObject(unitType);
                unit.transform.SetParent(previosParent);
                unit.transform.GetComponent<RectTransform>().position = previosParent.GetComponent<RectTransform>().position;
            }
        }

    }
}
