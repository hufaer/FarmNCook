using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResultAlchemySlot : MonoBehaviour
{
    public Item item;
    public Image icon;
    public static bool isDraggingResultSlot = false;

    public void Clear()
    {
        item = null;
        icon.sprite = null;
        icon.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        if (GameManager.instance.dragDrop.draggedSlot.item == null)
        {
            Image currImage = GameManager.instance.dragDrop.draggedIcon.GetComponent<Image>();
            currImage.gameObject.SetActive(true);
            currImage.sprite = icon.sprite;

            GameManager.instance.dragDrop.draggedSlot.Set(item, 1);
            isDraggingResultSlot = true;
        }
    }
}
