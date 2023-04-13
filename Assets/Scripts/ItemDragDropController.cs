using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragDropController : MonoBehaviour
{
    [SerializeField] public ItemSlot draggedSlot;

    public ItemSlot tempSlot;

    internal bool CheckSale()
    {
        if (!draggedSlot.item) return false;
        return draggedSlot.item.isSellable;
    }

    internal bool CheckBuy()
    {
        if (!draggedSlot.item) return false;
        return draggedSlot.item.isBuyable;
    }

    public Item tempSlotItem;
    public int tempSlotCount;

    [SerializeField] public GameObject draggedIcon;
    RectTransform iconTransform;

    private void Start()
    {
        draggedSlot = new ItemSlot();
        iconTransform = draggedIcon.GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (draggedIcon.activeInHierarchy)
        {
            iconTransform.position = Input.mousePosition;


            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    Vector3 wPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    wPosition.z = 0;
                    SpawnItem.instance.Spawn(wPosition, draggedSlot.item, draggedSlot.count);

                    draggedSlot.Clear();
                    draggedIcon.SetActive(false);
                }
            }
        }
    }

    internal void OnClick(ItemSlot slot, PointerEventData eventData)
    {
        tempSlot = slot;
        tempSlotItem = slot.item;
        bool isLeftClick = eventData.button == PointerEventData.InputButton.Left;
        tempSlotCount = slot.count;

        if (draggedSlot.item != null && ResultAlchemySlot.isDraggingResultSlot)
        {
            if (slot.item != null && slot.item.itemName == draggedSlot.item.itemName)
            {
                slot.Set(slot.item, slot.count + draggedSlot.count);
                draggedSlot.Clear();
            } else
            {
                Item item = slot.item;
                int count = isLeftClick ? slot.count : 1;

                slot.Copy(this.draggedSlot);
                this.draggedSlot.Set(item, count);
            }
            ResultAlchemySlot.isDraggingResultSlot = false;
            GameManager.instance.alchemyManager.ClearSlots();
            GameManager.instance.alchemyManager.ClearResult();
        }

        if (this.draggedSlot.item == null)
        {
            if (isLeftClick || slot.count - 1 < 0)
            {
                this.draggedSlot.Copy(slot);
                slot.Clear();
            } else if (slot.count - 1 > 0)
            {
                draggedSlot.Set(slot.item, 1);
                slot.count--;
            }
        } else if (slot.item != null && slot.item.itemName == draggedSlot.item.itemName)
        {
            slot.Set(slot.item, slot.count + draggedSlot.count);
            draggedSlot.Clear();
        } else
        {
            Item item = slot.item;
            int count = isLeftClick ? slot.count : 1;

            slot.Copy(this.draggedSlot);
            this.draggedSlot.Set(item, count);
        }
        UpdateIcon();
    }

    public void UpdateIcon()
    {
        if (draggedSlot.item == null)
        {
            draggedIcon.SetActive(false);
        } else
        {
            draggedIcon.SetActive(true);
            Image currImage = draggedIcon.GetComponent<Image>();
            currImage.sprite = draggedSlot.item.icon;
        }
    }
}
