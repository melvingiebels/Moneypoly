using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    private float buget;
//    public TextMeshProUGUI bugetText;
//    public TextMeshProUGUI netWorthText;
//    public TextMeshProUGUI stockOverviewText;
    private List<Stock> wallet = new();
    private float netWorth;
    public GameObject cardPrefab;

    void Start()
    {
       
        buget = 2000;
//        bugetText.text = buget.ToString();
  //      netWorthText.text = "€ " + netWorth.ToString();
        //create stock
        Stock stock1 = new Stock();
        stock1.currentPrice = 100;
        stock1.name = "EasyToys";
        Stock stock2 = new Stock();
        stock2.currentPrice = 400;
        stock2.name = "Shell";
        Stock stock3 = new Stock();
        stock3.currentPrice = 300;
        stock3.name = "ASML";

        BuyStock(stock2);
        BuyStock(stock3);
        BuyStock(stock1);
        CardSpawner cardSpawner = GetComponent<CardSpawner>();
        cardSpawner.SetStocks(wallet);

    }

    void Update()
    {

        
    }

    //public void CalculateWallet()
    //{
    //    float NewNetWorth= 0;
    //    foreach (Stock stock in wallet)
    //    {
    //        NewNetWorth += stock.currentPrice;
    //    }
    //    netWorth = NewNetWorth;
    //}
    //public void PrintStock()
    //{
    //    foreach(Stock stock in wallet)
    //    {

    //    }
    //}


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
