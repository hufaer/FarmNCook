using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Actions/Seed Action")]
public class SeedGround : ToolAction
{
    public override bool OnApplyToTilemap(Vector3Int position, TileReader tileReader, Item item)
    {
        if (!tileReader.plantsManager.Check(position)) return false;
        if (!CheckGround(position)) return false;

        tileReader.plantsManager.Seed(position, item.plant);
        return true;
    }

    public override void OnItemUsed(Item item, ItemContainer itemContainer)
    {
        itemContainer.Remove(item);
        GameManager.instance.toolbar.ShowInventory();
    }
}
