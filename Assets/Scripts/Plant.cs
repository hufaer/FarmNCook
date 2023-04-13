using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
[CreateAssetMenu(menuName = "Data/Plant")]
public class Plant : ScriptableObject
{
    public int timeToGrow;

    public Item dropPlant;
    public int count = 1;

    public List<TileBase> growingTiles;
    public List<int> growthPhaseTime;
}
