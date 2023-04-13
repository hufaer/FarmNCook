using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Tree,
    Rock,
    Unknown
}


[CreateAssetMenu(menuName = "Data/Tool Actions/Gather Resource Node")]
public class GatherResourceNode : ToolAction
{
    [SerializeField] float sizeOfArea = 0.65f;
    [SerializeField] List<ResourceType> resourceTypes;

    public override bool OnApply(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfArea);

        foreach (Collider2D collider in colliders)
        {
            ToolHit toolHit = collider.GetComponent<ToolHit>();
            if (toolHit != null)
            {
                if (toolHit.CheckCanBeHitByTool(resourceTypes))
                {
                    toolHit.Hit();
                    return true;
                }
            }
        }

        return false;
    }
}
