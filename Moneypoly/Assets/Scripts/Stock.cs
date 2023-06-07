using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stock
{
    public string stockName;
    public string tickerSymbol;
    public float currentPrice;
    public float previousPrice;

    public Stock(string name, string symbol, float price)
    {
        stockName = name;
        tickerSymbol = symbol;
        currentPrice = price;
        previousPrice = price;
    }

    public bool IsGoingUp()
    {
        return currentPrice > previousPrice;
    }
}
