using UnityEngine;

public class SetRoadSlotIndex : MonoBehaviour
{
    private RoadSlot[] roadTileArr;
    private void Awake()
    {
        roadTileArr = GetComponentsInChildren<RoadSlot>();
        for (int i = 0; i < roadTileArr.Length; i++)
        {
            roadTileArr[i].SetRoadTileIdx(i);
        }
    }
}
