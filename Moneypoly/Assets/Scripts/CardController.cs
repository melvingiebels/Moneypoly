using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardController : MonoBehaviour
{
    public Stock cardData;
    private PlayerInventory playerInventory;
    //declare the text ui to display the stock name
    public TextMeshProUGUI stockName;
    public TextMeshProUGUI stockCurrentPrice;
    public TextMeshProUGUI stockPreviousPrice;
    public TextMeshProUGUI stockPercentage;
    public TextMeshProUGUI totalOwned;
    public TextMeshProUGUI totalInvestment;
    public Button sellButton;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        sellButton = GetComponentInChildren<Button>();
        sellButton.onClick.AddListener(sellStock);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void sellStock()
    {
        playerInventory.SellStock(cardData);
    }

    public void SetCardData(Stock cardData, int amount)
    {
        this.cardData = cardData;
        stockName.text = cardData.stockName;
        stockPreviousPrice.text = "€ "+ cardData.previousPrice.ToString("0.00");
        stockCurrentPrice.text ="€ "+ cardData.currentPrice.ToString("0.00");

        string indicator = cardData.IsGoingUp() ? "▲" : "▼";
        stockPercentage.text = $"{indicator} <color={(cardData.IsGoingUp() ? "green" : "red")}>{cardData.GetPercentageChange().ToString("0.00")}%</color>";

        totalOwned.text = "x" + amount.ToString();
        totalInvestment.text = "€ " + (cardData.currentPrice * amount).ToString("0.00");
    }
}
