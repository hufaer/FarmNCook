using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropResource : ToolHit
{
    [SerializeField] Item dropItem;

    [SerializeField] int dropCount = 4;
    [SerializeField] float spread = 0.6f;
    [SerializeField] int countOfHits = 3;
    [SerializeField] ResourceType resourceType;

    public override void Hit()
    {
        StartCoroutine(WaitBeforeDestroying());
    }

    public IEnumerator WaitBeforeDestroying()
    {
        countOfHits--;
        if (countOfHits <= 0)
        {
            yield return new WaitForSeconds(0.6f);

            while (dropCount > 0)
            {
                dropCount--;

                Vector3 position = transform.position;
                position.x += spread * UnityEngine.Random.value - spread / 2;
                position.y += spread * UnityEngine.Random.value - spread / 2;

                SpawnItem.instance.Spawn(position, dropItem, 1);
            }
            GameManager.instance.savePosition.deleted.Add(gameObject.transform.position);
            Destroy(gameObject);
        }
    }

    

    public override bool CheckCanBeHitByTool(List<ResourceType> resourceTypes)
    {
        return resourceTypes.Contains(resourceType);
    }
}
