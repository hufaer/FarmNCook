using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToolBarController : MonoBehaviour
{
    [SerializeField] int size = 8;
    int selectedSlot;

    public Item CurrItem
    {
        get
        {
            return GameManager.instance.inventory.slots[selectedSlot].item;
        }
    }

    public Action<int> onChange;

    private void Update()
    {
        float scrollDelta = Input.mouseScrollDelta.y;
        if (scrollDelta != 0)
        {
            if (scrollDelta < 0)
            {
                selectedSlot += 1;
                selectedSlot = selectedSlot >= size ? 0 : selectedSlot;
            } else
            {
                selectedSlot -= 1;
                selectedSlot = selectedSlot < 0 ? size - 1 : selectedSlot;
            }
            onChange?.Invoke(selectedSlot);
        }
    }

    internal void Set(int id)
    {
        selectedSlot = id;
    }
}
