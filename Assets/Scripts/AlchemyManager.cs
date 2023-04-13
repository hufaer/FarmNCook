using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class AlchemyManager : MonoBehaviour
{
    [SerializeField] GameObject playerPanel;
    [SerializeField] GameObject alchemyPanel;
    public static bool isCurrentlyInAlchemy = false;
    [SerializeField] GameObject hintPanel;


    private Item currItem;

    public AlchemySlot[] alchemySlots;
    public List<string> recipies;
    [SerializeField] List<GameObject> recipePanels;
    public List<Item> results;

    [SerializeField] ResultAlchemySlot resultSlot;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isCurrentlyInAlchemy && !PauseGame.isPaused)
        {
            StopAlchemy();
        }

        string result = "";
        foreach (AlchemySlot slot in alchemySlots)
        {
            if (slot.item)
            {
                result += slot.item.itemName;
            } else
            {
                result += "null";
            }
        }

        bool resultFound = false;
        for (int i = 0; i < recipies.Count; ++i)
        {
            if (result.ToLower() == recipies[i])
            {
                resultFound = true;
                resultSlot.icon.gameObject.SetActive(true);
                resultSlot.item = results[i];
                resultSlot.icon.sprite = results[i].icon;
                recipePanels[i].GetComponent<RecipePanel>().Unlock();
            }
        }

        if (!resultFound)
        {
            resultSlot.Clear();
        }
    }

    public void StartAlchemy()
    {
        isCurrentlyInAlchemy = true;
        playerPanel.SetActive(true);
        alchemyPanel.SetActive(true);
    }

    public void StopAlchemy()
    {
        if (GameManager.instance.dragDrop.draggedSlot.item != null)
        {
            GameManager.instance.dragDrop.draggedIcon.SetActive(false);
            ItemSlot itemSlot = GameManager.instance.inventory.slots.Find(x => x.item == null);
            itemSlot.Set(GameManager.instance.dragDrop.draggedSlot.item, GameManager.instance.dragDrop.draggedSlot.count);
            GameManager.instance.dragDrop.draggedSlot = new ItemSlot();
        }

        if (ResultAlchemySlot.isDraggingResultSlot)
        {
            RestoreItems();
            ClearResult();
        }


        if (!CheckIfSlotsAreClear())
        {
            RestoreItems();
        }

        if (resultSlot.item != null)
        {
            ClearResult();
        }

        hintPanel.SetActive(false);
        isCurrentlyInAlchemy = false;
        playerPanel.SetActive(false);
        alchemyPanel.SetActive(false);
        GameManager.instance.toolbar.ShowInventory();
    }

    public void ClearResult()
    {
        resultSlot.Clear();
    }

    public void ClearSlots()
    {
        for (int i = 0; i < alchemySlots.Length; ++i)
        {
            alchemySlots[i].Clear();
        }
    }

    public void RestoreItems()
    {
        for (int i = 0; i < alchemySlots.Length; ++i)
        {
            Debug.Log("Adding:" + alchemySlots[i].item.itemName);
            GameManager.instance.inventory.Add(alchemySlots[i].item, 1);
            alchemySlots[i].Clear();
        }
    }

    public bool CheckIfSlotsAreClear()
    {
        foreach (AlchemySlot slot in alchemySlots)
        {
            if (slot.item != null)
            {
                return false;
            }
        }

        return true;
    }
}
