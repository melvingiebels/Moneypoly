using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerInventory : MonoBehaviour
{
    public float budget;

    private List<Stock> wallet = new();
    public float netWorth;
    public GameObject cardPrefab;
    public CardSpawner cardSpawner;
    private Dictionary<Stock, int> stocks = new();
    private PlayerController playerController;
    void Start()
    {
       
        budget = 2000;
        cardSpawner = GetComponent<CardSpawner>();
        playerController = GetComponentInParent<PlayerController>();
        playerController.playerInventory = this;

    }

    void Update()
    {

    }

    public void CalculateWallet()
    {
        float NewNetWorth = 0;
        foreach (KeyValuePair<Stock, int> stock in stocks)
        {
            Stock stock1 = stock.Key;
            float amount = stock.Value;
            float stockValue = stock1.currentPrice * amount;
            NewNetWorth += stockValue;
        }
        netWorth = NewNetWorth;
    }



    public void BuyStock(Stock stock, int amount)
    {
        if(amount * stock.currentPrice < budget)
        {
            budget -= (stock.currentPrice * amount);
            stocks.Add(stock, amount);
            wallet.Add(stock);
            CalculateWallet();
            SetStock();
        }
    }

    public List<Stock> GetWallet()
    {
        return wallet;
    }
    public Dictionary<Stock, int> GetStocks()
    {
        return stocks;
    }

    public void SellStock(Stock stock)
    {
        float sellingAmount = stocks[stock] * stock.currentPrice;
        budget += sellingAmount;
        wallet.Remove(stock);
        stocks.Remove(stock);
        CalculateWallet();
        SetStock();
    }
    public void DecreaseBudget(float amount)
    {
        
        budget -= amount;
        if(budget < 0)
        {
            budget = 0;
        }   
    }
    public void SetStock()
    {
        cardSpawner.SetStocks(stocks);
    }
    public void SellAllStock()
    {
        foreach (KeyValuePair<Stock, int> stock in stocks)
        {
            SellStock(stock.Key);
        }
    }
}



