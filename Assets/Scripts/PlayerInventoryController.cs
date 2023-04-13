using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject toolbarPanel;
    [SerializeField] GameObject hintPanel;
    private bool isActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !PauseGame.isPaused)
        {
            if (Trade.isCurrentlyTrading) return;
            if (!isActive)
            {
                isActive = true;
                inventoryPanel.SetActive(!inventoryPanel.activeInHierarchy);
                inventoryPanel.GetComponent<InventoryPanel>().ShowInventory();
                toolbarPanel.SetActive(!toolbarPanel.activeInHierarchy);
            }
            else
            {
                isActive = false;

                if (GameManager.instance.dragDrop.draggedSlot.item != null)
                {
                    GameManager.instance.dragDrop.draggedIcon.SetActive(false);
                    ItemSlot itemSlot =  GameManager.instance.inventory.slots.Find(x => x.item == null);
                    itemSlot.Set(GameManager.instance.dragDrop.draggedSlot.item, GameManager.instance.dragDrop.draggedSlot.count);
                    GameManager.instance.dragDrop.draggedSlot = new ItemSlot();
                }
                toolbarPanel.SetActive(true);
                inventoryPanel.SetActive(false);
                hintPanel.SetActive(false);
            }
        }
    }

}
