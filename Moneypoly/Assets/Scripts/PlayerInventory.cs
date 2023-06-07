using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    private float buget;
    public TextMeshProUGUI bugetText;
    public TextMeshProUGUI netWorthText;
    public TextMeshProUGUI stockOverviewText;
    private List<Stock> wallet = new List<Stock>();
    private float netWorth;

    void Start()
    {
       
        buget = 2000;
        bugetText.text = buget.ToString();
        netWorthText.text = "€ " + netWorth.ToString();
        //create stock
        Stock stock1 = new Stock();
        stock1.currentPrice = 100;
        stock1.name = "stock1";
        BuyStock(stock1);
        PrintStock();
        CalculateWallet();

    }

    void Update()
    {
        
        bugetText.text = buget.ToString();
        netWorthText.text = "€ "+ netWorth.ToString();
        
    }

    public void CalculateWallet()
    {
        
        foreach(Stock stock in wallet)
        {
            netWorth += stock.currentPrice;
        }
    }
    public void PrintStock()
    {
        foreach(Stock stock in wallet)
        {
            stockOverviewText.text = stockOverviewText.text + stock.name + " € " + stock.currentPrice + "\n";
        }
    }


    public void BuyStock(Stock stock)
    {
            buget -= stock.currentPrice;
            wallet.Add(stock);
        
    }
}

public class Stock
{
    public float currentPrice;
    public string name;

}
