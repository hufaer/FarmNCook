using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHit : MonoBehaviour
{
    public virtual void Hit()
    {

    }

    public virtual bool CheckCanBeHitByTool(List<ResourceType> resourceTypes)
    {
        return true;
    }
}
