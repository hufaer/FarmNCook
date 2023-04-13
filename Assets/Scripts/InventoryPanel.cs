using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPanel : BaseItemPanel
{
    public override void OnClick(int id, PointerEventData eventData)
    {
        GameManager.instance.dragDrop.OnClick(inventory.slots[id], eventData);
        ShowInventory();
    }
}
