using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardController : MonoBehaviour
{
    public List<PlayerController> players;
    public List<WaypointComponent> Waypoints;
    public TMP_Text currentPlayerText;
    public StockMarket StockMarket;
    public GameObject waypointPrefab;
    public int rounds;
    public TMP_Text roundText;
    // TODO:: DIT VERANDEREN NAAR EEN INTERFACE VAN CARDS NIET ALGEMEEN CARD
    private List<GenericTile> cards = new List<GenericTile>();

    // Reference to the Scoreboard component
    public Scoreboard scoreboard;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void StartGame()
    {
        InitDeckOfCards();
        scoreboard.setScoreBoard();

        float yValue = -140f;
        float xValue = -150f;
        Vector3 position = new Vector2(-150f, yValue);
        int waypointIndex = 0;
        int xIncrement = 0;
        int yIncrement = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            // Create a waypoint prefab instance for each stock
            GameObject waypoint = Instantiate(waypointPrefab, position, Quaternion.identity);

            WaypointComponent waypointComponent = waypoint.GetComponent<WaypointComponent>();
            // if the waypoint index is devisable by 3, it is not a stock. if a waypoint index is devisable by 10, it is also not a stock.
            if (cards[waypointIndex].Stock != null)
            {
                // Customize the waypoint properties based on the stock data
                waypointComponent.SetStockData(cards[waypointIndex].Stock);
            } 
            // else, init the other types of waypoints

            // Set the parent of the waypoint to keep the hierarchy organized 
            waypoint.transform.SetParent(GetComponent<Transform>());

            // Add the waypoint to the waypoint list
            Waypoints.Add(waypointComponent);

            if (waypointIndex >= 30)
            {
                xIncrement = -25;
                yIncrement = 0;
            }
            else if (waypointIndex >= 20)
            {
                yIncrement = -25;
                xIncrement = 0;

            }
            else if (waypointIndex >= 10)
            {
                xIncrement = 25;
                yIncrement = 0;
            }
            else if (waypointIndex >= 0)
            {
                xIncrement = 0;
                yIncrement = 28;
            }
            position = new Vector2(xValue += xIncrement, yValue += yIncrement);
            waypointIndex += 1;
        }

        StartCoroutine(PlayRounds());
    }

    private IEnumerator PlayRounds()
    {
        // Update the scoreboard at the end of the game
        scoreboard.updateScoreboard();
        rounds += 1;
        roundText.text = "Ronde: " + rounds.ToString() + "/10";

        foreach (PlayerController player in players)
        {
            currentPlayerText.text = "Current player: " + player.name;

            yield return StartCoroutine(player.PlayRound(Waypoints));
        }

        if (UnityEngine.Random.value <= 0.2f) 
        {
            SceneManager.LoadScene("NewsFlash", LoadSceneMode.Additive);
        }

        if (rounds <= 10)
        {
            StartCoroutine(PlayRounds());
        }
    }

    private void InitDeckOfCards()
    {

        List<Stock> stocks = StockMarket.GetStocks();

        for (int i = 0; i < stocks.Count + 16; i++)
        {
            if (i % 10 == 0)
            {
                // special node
                cards.Add(new GenericTile(null, null, "Speciale node"));
            }
            else if (i % 3 == 0)
            {
                // kans node    
                cards.Add(new GenericTile(new AlgemeenCard(), null, null));
            }
            else
            {
                // insert an empty (stock will go here later)
                cards.Add(new GenericTile(null,null,null));
            }
        }
        foreach (var item in stocks)
        {
            // search for the empty places
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].card == null && cards[i].specialWaypoint == null && cards[i].Stock == null)
                {
                    cards[i] = new GenericTile(null, item, null);
                }
            }
        }
    }
}
