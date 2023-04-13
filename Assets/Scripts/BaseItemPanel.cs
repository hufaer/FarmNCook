using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseItemPanel : MonoBehaviour
{
    public ItemContainer inventory;
    public List<InventoryButton> slots;
    public PointerEventData eventData;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SetIndex();
        ShowInventory();
    }

    private void OnEnable()
    {
        ShowInventory();
    }
    private void SetIndex()
    {
        if (!inventory) return;
        for (int i = 0; i < inventory.slots.Count && i < slots.Count; ++i)
        {
            slots[i].SetIndex(i);
        }
    }

    public void ShowInventory()
    {
        if (!inventory) return;
        for (int i = 0; i < inventory.slots.Count && i < slots.Count; ++i)
        {
            if (inventory.slots[i].item == null)
            {
                slots[i].Clean();
            }
            else
            {
                slots[i].Set(inventory.slots[i]);
            }
        }
    }


    public virtual void OnClick(int id, PointerEventData eventData) {}

    public void SetInventory(ItemContainer inventory)
    {
        this.inventory = inventory;
        ClearUnused();
    }

    public void ClearUnused()
    {
        for (int i = 0; i < slots.Count; ++i)
        {
            slots[i].Clean();
        }
    }
}
