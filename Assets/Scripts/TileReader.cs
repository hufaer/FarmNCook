using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileReader : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    public PlantsManager plantsManager;

    private void Update()
    {
        
    }

    public Vector3Int GetGridPosition(Vector2 position, bool isWorldPosition = false)
    {
        Vector3 wPosition;
        if (isWorldPosition)
        {
            wPosition = Camera.main.ScreenToWorldPoint(position);
        }
        else
        {
            wPosition = position;
        }

        return tilemap.WorldToCell(wPosition);
    }

    public TileBase GetTileBase(Vector3Int position)
    { 
        return tilemap.GetTile(position);
    }
}
