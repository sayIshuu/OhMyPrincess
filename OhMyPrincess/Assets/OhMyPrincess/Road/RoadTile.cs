using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoadTile : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;
    private Image image;
    private int index;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }


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
                eventData.pointerDrag.transform.SetParent(transform.parent);
            }
        }
    }


    public void SetRoadTileIdx(int x)
    {
        index = x;
    }

    public int GetRoadTileIdx()
    {
        return index;
    }
}
