using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Data/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public bool isStackable;
    public Sprite icon;
    public ToolAction onAction;
    public ToolAction onTilemapAction;
    public ToolAction onItemUsage;
    public Plant plant;
    public bool isSellable;
    public int priceForSell;
    public bool isBuyable;
    public int priceForBuy;

    public string nameForDisplay;
}
