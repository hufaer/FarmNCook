using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour, IDataPersistence
{
    [SerializeField] int amount;
    [SerializeField] TMPro.TextMeshProUGUI text;

    private void Start()
    {
        UpdateText();   
    }

    private void UpdateText()
    {
        text.text = amount.ToString();
    }

    public void AddMoney(int money)
    {
        amount += money;
        UpdateText();
    }

    public void RemoveMoney(int money)
    {
        amount -= money;
        UpdateText();
    }

    internal bool CheckBalance(int price)
    {
        return amount >= price;
    }

    public void LoadData(GameData gameData)
    {
        amount = gameData.playerMoney;
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.playerMoney = amount;
    }
}
