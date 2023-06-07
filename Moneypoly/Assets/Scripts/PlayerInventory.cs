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
        netWorthText.text = netWorth.ToString();

    }

    void Update()
    {

    }

    public void CalculateWallet()
    {
        netWorth = 0;
        foreach(Stock stock in wallet)
        {
            netWorth =+ stock.currentPrice;
        }
    }


    public void BuyStock(Stock stock)
    {
            buget =-stock.currentPrice;
            wallet.Add(stock);
        
    }
}

public class Stock
{
    public float currentPrice;

}
