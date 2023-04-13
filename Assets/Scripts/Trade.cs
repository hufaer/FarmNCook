using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trade : MonoBehaviour
{
    [SerializeField] GameObject merchantPanel;
    [SerializeField] GameObject playerPanel;
    public static bool isCurrentlyTrading = false;
    [SerializeField] GameObject hintPanel;
    PlayerMoney money;

    [SerializeField] Merchant merchant;

    ItemStorePanel storePanel;
    ItemContainer playerInventory;

    private void Awake()
    {
        money = GetComponent<PlayerMoney>();
        storePanel = merchantPanel.GetComponent<ItemStorePanel>();
        playerInventory = GameManager.instance.inventory;
        storePanel.SetInventory(merchant.merchantInventory);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isCurrentlyTrading && !PauseGame.isPaused)
        {
            StopTrade();
            merchant.isInteracting = false;
            isCurrentlyTrading = false;
        }
    }

    public void BeginTrade()
    {
        isCurrentlyTrading = true;

        merchantPanel.SetActive(true);
        playerPanel.SetActive(true);
    }

    public void StopTrade()
    {
        hintPanel.SetActive(false);
        merchantPanel.SetActive(false);
        playerPanel.SetActive(false);
        GameManager.instance.toolbar.gameObject.SetActive(true);

        if (GameManager.instance.dragDrop.draggedSlot.item != null)
        {
            GameManager.instance.dragDrop.draggedIcon.SetActive(false);
            ItemSlot itemSlot = GameManager.instance.inventory.slots.Find(x => x.item == null);
            itemSlot.Set(GameManager.instance.dragDrop.draggedSlot.item, GameManager.instance.dragDrop.draggedSlot.count);
            GameManager.instance.dragDrop.draggedSlot = new ItemSlot();
        }
    }

    public void SellItem()
    {
        if (GameManager.instance.dragDrop.CheckSale())
        {
            ItemSlot slot = GameManager.instance.dragDrop.draggedSlot;
            int received = slot.item.isStackable ? slot.count * slot.item.priceForSell : slot.item.priceForSell;

            money.AddMoney(received);
            slot.Clear();
            GameManager.instance.dragDrop.UpdateIcon();
            GameManager.instance.toolbar.ShowInventory();
        }
    }

    internal void BuyItem(int id)
    {
        if (merchant.merchantInventory.slots[id].item)
        {
            Item item = merchant.merchantInventory.slots[id].item;
            int itemPrice = item.priceForBuy;

            if (money.CheckBalance(itemPrice))
            {
                playerInventory.Add(item, 1);
                money.RemoveMoney(itemPrice);
                playerPanel.GetComponent<InventoryPanel>().ShowInventory();
            }
        }
    }
}
