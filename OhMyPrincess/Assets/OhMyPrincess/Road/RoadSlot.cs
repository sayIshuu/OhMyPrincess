using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoadSlot : MonoBehaviour//, IDropHandler
{
    public bool occupied = false;
    private int index;
    //자신의 콜라이더 가져오기
    public BoxCollider2D boxCollider2D;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    /*
    // RoadTile은 유닛들의 배치를 위한 타일. OnDrop으로 유닛객체의 부모가 되어 배치되도록 함.
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
