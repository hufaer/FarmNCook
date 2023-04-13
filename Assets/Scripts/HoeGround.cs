using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName ="Data/Tool Actions/Hoe Action")]
public class HoeGround : ToolAction
{
    [SerializeField] List<TileBase> hoeableTiles;
    public override bool OnApplyToTilemap(Vector3Int position, TileReader tileReader, Item item)
    {
        if (!CheckGround(position)) return false;
        TileBase tile = tileReader.GetTileBase(position);

        if (!hoeableTiles.Contains(tile))
        {
            return false;
        }

        tileReader.plantsManager.Plow(position);
        return true;
    }
}
