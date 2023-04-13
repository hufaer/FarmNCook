using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] Text text;
    [SerializeField] Image highlightedIcon;

    GameObject hintPanel;
    TMPro.TextMeshProUGUI nameText;
    TMPro.TextMeshProUGUI priceText;

    private void Awake()
    {
        hintPanel = GameManager.instance.hintPanel;
        nameText = GameManager.instance.nameText;
        priceText = GameManager.instance.priceText;
    }

    public int currIndex;

    public void SetIndex(int index)
    {
        currIndex = index;
    }

    public void Set(ItemSlot slot)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = slot.item.icon;
        if (slot.item.isStackable)
        {
            text.gameObject.SetActive(true);
            text.text = slot.count == 0 ? "" : slot.count.ToString();
        } else
        {
            text.gameObject.SetActive(false);
        }
    }

    public void Clean()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BaseItemPanel baseItemPanel = transform.parent.GetComponent<BaseItemPanel>();
        baseItemPanel.OnClick(currIndex, eventData);

        //ItemContainer inventory = GameManager.instance.inventory;
        //GameManager.instance.dragDrop.OnClick(inventory.slots[currIndex]);
        //transform.parent.GetComponent<InventoryPanel>().ShowInventory();
    }

    public void Highlight(bool isSelected)
    {
        highlightedIcon.gameObject.SetActive(isSelected);
    }

    public void OnHoverEntry()
    {
        SpawnHintAndDelete();
    }

    private void SpawnHintAndDelete()
    {
        BaseItemPanel baseItemPanel = transform.parent.GetComponent<BaseItemPanel>();

        Item item = baseItemPanel.inventory.slots[currIndex].item;
        if (item)
        {
            hintPanel.SetActive(true);
            Vector3 mousePos = Input.mousePosition;
            RectTransform rectTransform = hintPanel.GetComponent<RectTransform>();

            hintPanel.transform.position = new Vector2(mousePos.x + 129, mousePos.y);

            nameText.SetText(item.nameForDisplay);

            if (item.isBuyable)
            {
                priceText.SetText("Цена: " + item.priceForBuy);
            } else if (item.isSellable)
            {
                priceText.SetText("Можно продать за: " + item.priceForSell);
            }
            else
            {
                priceText.SetText("Нельзя продать");
            }
        }
    }

    public void OnHoverExit()
    {
        hintPanel.SetActive(false);
    }
}
