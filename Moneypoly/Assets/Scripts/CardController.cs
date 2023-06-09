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
        stockPrice.text ="€ "+ cardData.currentPrice.ToString();
    }
}
