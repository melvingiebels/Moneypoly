using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocationCardScript : MonoBehaviour
{
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private TMP_Text LocationHeaderText;
    [SerializeField] private TMP_Text PreviousPrice;
    [SerializeField] private TMP_Text CurrentPrice;
    [SerializeField] private TMP_Text Percentage;
    [SerializeField] private Button InvestButton;
    [SerializeField] private Button CloseButton;

    private WaypointComponent currentWaypoint;
    private PlayerInventory currentPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Invest()
    {
        //1 add the stock to the player's inventory
        currentPlayer.cardSpawner.transform.parent.gameObject.SetActive(true);

        currentPlayer.BuyStock(currentWaypoint.Stock, 1);
        //2 call the close function 
        Close();
    }
    public void Open()
    {
        canvasObject.SetActive(true);
    }
    public void Close()
    {
        canvasObject.SetActive(false);
    }
    public void FillDataFromWaypoint(WaypointComponent waypoint,PlayerInventory player)
    {
        if (waypoint.CardScript != null)  
        {
            Open();
            ShowCard(waypoint);
        }
        else if (waypoint.Stock.stockName != "")
        {
            ShowStock(waypoint,player);
        }
    
    }
    public void ShowCard(WaypointComponent waypoint) 
    {
        // Laat het nog kijken of het algemeen of kans is :)
        waypoint.CardScript.ShowCardAlgemeen();
    }
    public void ShowStock(WaypointComponent waypoint, PlayerInventory player)
    {
        currentWaypoint = waypoint;
        currentPlayer = player;

        LocationHeaderText.text = $"{waypoint.Stock.stockName} ({waypoint.Stock.tickerAbbreviation})";
        PreviousPrice.text = "Vorige prijs: € " + waypoint.Stock.previousPrice.ToString("0.00");
        CurrentPrice.text = "Huidige prijs: € " + waypoint.Stock.currentPrice.ToString("0.00");

        if (waypoint.Stock.GetPercentageChange() == 0)
        {
            Percentage.text = "<color=white>0.00%</color>";
        }
        else
        {
            string indicator = waypoint.Stock.IsGoingUp() ? "▲" : "▼";
            Percentage.text = $"{indicator} <color={(waypoint.Stock.IsGoingUp() ? "green" : "red")}>{waypoint.Stock.GetPercentageChange().ToString("0.00")}%</color>";
        }
    }
}
