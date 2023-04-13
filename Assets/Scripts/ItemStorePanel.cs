using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemStorePanel : BaseItemPanel
{
    [SerializeField] Trade trade;
    public override void OnClick(int id, PointerEventData eventData)
    {
        if (GameManager.instance.dragDrop.draggedSlot.item == null)
        {
            trade.BuyItem(id);
        }
        else
        {
            trade.SellItem();
            ShowInventory();
        }
    }
}
