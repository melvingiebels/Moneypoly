using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stock
{
    public string stockName;
    public string tickerAbbreviation;
    public string branchName;
    public float currentPrice;
    public float previousPrice;

    public Stock(string name, string abbreviation, string branch, float price)
    {
        stockName = name;
        tickerAbbreviation = abbreviation;
        branchName = branch;
        currentPrice = price;
        previousPrice = price;
    }

    public bool IsGoingUp()
    {
        return currentPrice > previousPrice;
    }
}
