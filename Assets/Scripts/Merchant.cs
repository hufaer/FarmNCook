using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : Interactable
{
    public ItemContainer merchantInventory;
    [SerializeField] DialogueSystem dialogue;
    public bool isInteracting = false;

    public override void Interact(PlayerMovement player)
    {
        if (!isInteracting)
        {
            dialogue.Init();
        }
    }

    public void StartTrade()
    {
        if (!Trade.isCurrentlyTrading)
        {
            Trade trade = GameManager.instance.player.GetComponent<Trade>();
            if (!trade) return;

            Debug.Log("Interact merchant");
            trade.BeginTrade();
        }
    }
}
