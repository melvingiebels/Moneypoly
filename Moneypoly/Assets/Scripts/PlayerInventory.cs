using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerInventory : MonoBehaviour
{
    private float buget;
    public TextMeshProUGUI bugetValueText;
    public TextMeshProUGUI stockNetWorthText;

    private List<Stock> wallet = new();
    private float netWorth;
    public GameObject cardPrefab;
    public CardSpawner cardSpawner;

    void Start()
    {
       
        buget = 2000;
        bugetValueText.text = "€" + buget.ToString();
        stockNetWorthText.text = "€ " + netWorth.ToString();
        cardSpawner = GetComponent<CardSpawner>();


    }

    void Update()
    {
        bugetValueText.text = "€" + buget.ToString();
        stockNetWorthText.text = "€ " + netWorth.ToString();
        

    }

    public void CalculateWallet()
    {
        float NewNetWorth = 0;
        foreach (Stock stock in wallet)
        {
            NewNetWorth += stock.currentPrice;
        }
        netWorth = NewNetWorth;
    }



    public void BuyStock(Stock stock)
    {
        buget -= stock.currentPrice;
        wallet.Add(stock);
        CalculateWallet();
        SetStock();
        


    }
    public void SetStock()
    {
        cardSpawner.SetStocks(wallet);
    }
}



