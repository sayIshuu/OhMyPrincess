using UnityEngine;
using UnityEngine.EventSystems;

public class UnitDraggableForUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Transform canvas;
    private CanvasGroup canvasGroup;
    private UnitDraggableForUI unitDraggable;
    //private RectTransform rectTransform;
    private Transform previosParent;
    private UnitType unitType;

    private void Awake()
    {
        canvas = FindFirstObjectByType<Canvas>().transform;
        canvasGroup = GetComponent<CanvasGroup>();
        unitDraggable = GetComponent<UnitDraggableForUI>();
        //rectTransform = GetComponent<RectTransform>();
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
        //rectTransform.position = eventData.position;
        //transform.position = eventData.position;
        Vector3 mousePos = eventData.position; // ���콺�� ��ũ�� ��ǥ
        mousePos.z = Camera.main.WorldToScreenPoint(transform.position).z; // ���� ������Ʈ�� ���� z�� ����

        // ��ũ�� ��ǥ �� ���� ��ǥ ��ȯ
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    // RoadSlot�� OnDrop���� ������ ��ġ�ϸ�, ������ �θ� RoadSlot���� ����ɰ���. ���� ����Ǵ� OnEndDrag
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        //1�̻��� ������ �ڱ⵵ ���ٰ� üũ�ϴ°��̱� ����.
        if( !transform.parent.CompareTag(nameof(TagType.RoadSlot)) || transform.parent.childCount > 1)
        {
            transform.SetParent(previosParent);
            //rectTransform.position = previosParent.GetComponent<RectTransform>().position + new Vector3(25, 0, 0);
            transform.position = previosParent.position;
        }
        else
        {
            //rectTransform.position = transform.parent.GetComponent<RectTransform>().position;
            transform.position = transform.parent.position;
            //���� �巡���̸� �巡�׷� ��ġ�� ��, ����â�� ���ο� ������ �����������.
            if (previosParent.CompareTag(nameof(TagType.UnitSlot)))
            {
                GameObject unit = ObjectPoolManager.Instance.GetUnitObject(unitType);
                unit.transform.SetParent(previosParent);
                //unit.transform.GetComponent<RectTransform>().position = previosParent.GetComponent<RectTransform>().position;
                unit.transform.position = previosParent.position;
            }
        }

    }
}
