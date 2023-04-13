using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlchemySlot : MonoBehaviour
{
    public Item item;
    public int index;
    public Image icon;

    public void OnMouseDownItem()
    {
        Item draggedItem = GameManager.instance.dragDrop.draggedSlot.item;
        if (draggedItem)
        {
            if (item)
            {
                GameManager.instance.inventory.Add(item, 1);
            }

            icon.gameObject.SetActive(true);
            icon.sprite = draggedItem.icon;
            item = draggedItem;

            GameManager.instance.dragDrop.draggedSlot.count -= 1;

            if (GameManager.instance.dragDrop.draggedSlot.item.isStackable && GameManager.instance.dragDrop.draggedSlot.count <= 0)
            {
                GameManager.instance.dragDrop.draggedSlot.Clear();
                GameManager.instance.dragDrop.UpdateIcon();
            }

        }
    }

    public void Clear()
    {
        item = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
    }
}
