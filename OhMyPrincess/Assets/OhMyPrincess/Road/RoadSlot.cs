using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoadSlot : MonoBehaviour//, IDropHandler
{
    public bool occupied = false;
    private int index;
    //�ڽ��� �ݶ��̴� ��������
    public BoxCollider2D boxCollider2D;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    /*
    // RoadTile�� ���ֵ��� ��ġ�� ���� Ÿ��. OnDrop���� ���ְ�ü�� �θ� �Ǿ� ��ġ�ǵ��� ��.
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            //RoadTile roadTile = eventData.pointerDrag.GetComponent<RoadTile>();
            Unit unit = eventData.pointerDrag.GetComponent<Unit>();
            if (unit != null)
            {
                eventData.pointerDrag.transform.position = rectTransform.position;
                eventData.pointerDrag.transform.SetParent(transform);
            }
        }
    }
    */


    public void SetRoadTileIdx(int x)
    {
        index = x;
    }

    public int GetRoadTileIdx()
    {
        return index;
    }
}
