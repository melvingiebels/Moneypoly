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
    public TMP_Text stock3;
    public Stock[] stocks;
    public string[] branchNames;

    public int currentRound = 1;
    public int totalRounds = 10;

    private void Start()
    {
        // Initialize the stocks
        InitializeStocks();
    }

    private void InitializeStocks()
    {
        branchNames = new string[]
        {
            "Media en communicatie",
            "Vastgoed",
            "Gezondheidszorg en -welzijn",
            "Milieu en Agrarische sector",
            "ICT / Technisch",
            "Handel en dienstverlening"
        };
        // Create instances of stocks and assign values
        stocks = new Stock[]
        {
            new Stock("ABC Company", "ABC", branchNames[0], 10.0f),
            new Stock("DEF Company", "DEF", branchNames[0], 10.0f),
            new Stock("XYZ Corporation", "XYZ", branchNames[1], 20.0f),
        };
    }

    public void UpdateStockPrices()
    {
        // Calculate the weight for price changes based on the current round
        float weight = 1.0f - ((float)currentRound / totalRounds);

        Dictionary<string, float> branchPriceChanges = new Dictionary<string, float>();

        // Calculate price changes for each branch
        foreach (string branch in branchNames)
        {
            // Calculate the minimum and maximum price change based on the weight
            float minChange = -5.0f * weight;
            float maxChange = 5.0f * weight;

            // Calculate a random change for the branch
            float branchChange = Random.Range(minChange, maxChange);
            branchPriceChanges.Add(branch, branchChange);
        }

        // Update stock prices for each stock within each branch
        foreach (string branch in branchNames)
        {
            List<Stock> branchStocks = GetStocksByBranch(branch);

            // Calculate the average price change for the branch
            float branchChange = branchPriceChanges[branch];
            float averageChange = branchChange / branchStocks.Count;

            // Update stock prices within the branch
            foreach (Stock stock in branchStocks)
            {
                // Calculate a random deviation from the average change
                float randomDeviation = Random.Range(-1.0f, 1.0f) * Mathf.Abs(averageChange);
                float stockChange = averageChange + randomDeviation;

                // Apply the branch-specific and stock-specific price change to the stock's current price
                stock.previousPrice = stock.currentPrice;
                stock.currentPrice += stockChange;

                // Ensure the current price is at least $1
                if (stock.currentPrice <= 0.0f)
                {
                    stock.currentPrice = 1.0f;
                }

                UpdateStockUI(stock);
            }
        }
    }

    private List<Stock> GetStocksByBranch(string branchName)
    {
        List<Stock> stocksInBranch = new List<Stock>();

        foreach (Stock stock in stocks)
        {
            if (stock.branchName == branchName)
            {
                stocksInBranch.Add(stock);
            }
        }

        return stocksInBranch;
    }


    private void UpdateStockUI(Stock stock)
    {
        string indicator = stock.IsGoingUp() ? "▲" : "▼";
        string stockInfo = $"{stock.stockName} ({stock.tickerAbbreviation})\n Branch: {stock.branchName}\n Prev: {stock.previousPrice}\nCurr: {indicator} <color={(stock.IsGoingUp() ? "green" : "red")}>{stock.currentPrice}</color>";

        if (stock.stockName == "ABC Company")
        {
            stock1.text = stockInfo;
        } 
        else if (stock.stockName == "DEF Company")
        {
            stock2.text = stockInfo;
        }
        else if (stock.stockName == "XYZ Corporation")
        {
            stock3.text = stockInfo;
        }
    }

}
