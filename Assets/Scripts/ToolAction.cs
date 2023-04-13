using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAction : ScriptableObject
{
    public virtual bool OnApply(Vector2 position)
    {
        Debug.LogWarning("OnApply is not implemented");
        return true;
    }

    public bool CheckGround(Vector3Int position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(position.x + 0.5f, position.y + 0.5f), 0.5f);
        int count = 0;
        foreach (Collider2D collider in colliders)
        {
            if (collider.tag != "Player")
            {
                count++;
            }
        }

        if (count > 0) return false;
        return true;
    }

    public virtual bool OnApplyToTilemap(Vector3Int position, TileReader tileReader, Item item)
    {
        Debug.Log("OnApplyToTilemap is not implemented");
        return true;
    }

    public virtual void OnItemUsed(Item item, ItemContainer itemContainer) { }
}
