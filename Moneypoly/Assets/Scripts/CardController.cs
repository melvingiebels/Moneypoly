using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public Stock cardData;
    //declare the text ui to display the stock name
    public TextMeshProUGUI stockName;
    public TextMeshProUGUI stockPrice;
    public TextMeshProUGUI stockPercentage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCardData(Stock cardData)
    {
        stockName.text = cardData.stockName;

        stockPrice.text ="€ "+ cardData.currentPrice.ToString("0.00");

        string indicator = cardData.IsGoingUp() ? "▲ " : "▼ ";

        stockPercentage.text = $"{indicator} <color={(cardData.IsGoingUp() ? "green" : "red")}>{cardData.GetPercentageChange().ToString("0.00")}%</color>";

        // stockPercentage.text = indicator + cardData.GetPercentageChange().ToString("0.00") + "%";
    }
}
