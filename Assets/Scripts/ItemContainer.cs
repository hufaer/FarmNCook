using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemSlot
{
    public Item item;
    public int count;

    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    public void Copy(ItemSlot slot)
    {
        item = slot.item;
        count = slot.count;
    }

    public void Clear()
    {
        item = null;
        count = 0;
    }
}

[CreateAssetMenu(menuName = "Data/Item Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots;

    public void Add(Item item, int count)
    {
        if (item.isStackable)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if (itemSlot != null)
            {
                itemSlot.count += count;
            } else
            {
                itemSlot = slots.Find(x => x.item == null);
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        } else
        {
            ItemSlot itemSlot = slots.Find(x => x.item == null);
            if (itemSlot != null)
            {
                itemSlot.item = item;
            }
        }
    }

    public void Remove(Item item, int count = 1)
    {
        ItemSlot slot = slots.Find(x => x.item == item);
        if (slot == null) return;

        if (item.isStackable)
        {
            slot.count -= count;
            if (slot.count <= 0) slot.Clear();
        } else
        {
            while (count > 0)
            {
                count--;
                slot.Clear();
            }
        }
        GameManager.instance.toolbar.ShowInventory();
    }

}
