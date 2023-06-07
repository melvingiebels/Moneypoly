using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StockMarket : MonoBehaviour
{
    public TMP_Text stock1;
    public TMP_Text stock2;
    public Stock[] stocks;

    private void Start()
    {
        // Initialize the stocks
        InitializeStocks();
    }

    private void InitializeStocks()
    {
        // Create instances of stocks and assign values
        stocks = new Stock[]
        {
            new Stock("ABC Company", "ABC", 10.0f),
            new Stock("XYZ Corporation", "XYZ", 20.0f),
        };
    }

   
    public void UpdateStockPrices()
    {
        foreach (Stock stock in stocks)
        {
            // Simulate price changes (replace with your own logic)
            float randomChange = Random.Range(-5.0f, 5.0f);
            stock.previousPrice = stock.currentPrice;
            stock.currentPrice += randomChange;
            UpdateStockUI(stock);
        }
    }

    private void UpdateStockUI(Stock stock)
    {
        string stockInfo = $"{stock.stockName} ({stock.tickerSymbol}): {stock.currentPrice}";
        if (stock.stockName == "ABC Company")
        {
            stock1.text = stockInfo;
        }
        else if (stock.stockName == "XYZ Corporation")
        {
            stock2.text = stockInfo;
        }
    }
}
