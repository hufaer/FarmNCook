using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] GameObject pickUpItem;
    public static SpawnItem instance;

    private void Awake()
    {
        instance = this;
    }

    public void Spawn(Vector3 position, Item item, int count)
    {
        GameObject spawned = Instantiate(pickUpItem, position, Quaternion.identity);
        spawned.GetComponent<PickUpItem>().Set(item, count);
    }

    public void SpawnWorldObject(Vector3 position, GameObject worldObject)
    {
        GameObject spawned = Instantiate(worldObject, position, Quaternion.identity);
    }

}
