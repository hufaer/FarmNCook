using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Data/Tool Actions/Shovel Action")]
public class DigGround : ToolAction
{
    public override bool OnApplyToTilemap(Vector3Int position, TileReader tileReader, Item item)
    {
        if (!CheckGround(position)) return false;

        TileBase tile = tileReader.GetTileBase(position);

        if (GameManager.instance.hoeableTiles.Contains(tile))
        {
            return false;
        }
        
        GameManager.instance.tilemap.SetTile(position, GameManager.instance.hoeableTiles[(int)UnityEngine.Random.Range(0, GameManager.instance.hoeableTiles.Count)]);
        GameManager.instance.plantsTilemap.SetTile(position, null);

        return true;
    }
}
