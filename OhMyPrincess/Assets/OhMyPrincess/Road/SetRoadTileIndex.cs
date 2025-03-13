using UnityEngine;

public class SetRoadTileIndex : MonoBehaviour
{
    private RoadTile[] roadTileArr;
    private void Awake()
    {
        roadTileArr = GetComponentsInChildren<RoadTile>();
        for (int i = 0; i < roadTileArr.Length; i++)
        {
            roadTileArr[i].SetRoadTileIdx(i);
        }
    }
}
