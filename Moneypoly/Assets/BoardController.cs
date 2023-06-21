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
    public GameObject newsFlashPrefab;
    public bool newsFlashHappend = false;
    public float newsFlashChance = 0.3f;
    public bool hold = false;
    public Grid grid;
    public Canvas canvas;
    // TODO:: DIT VERANDEREN NAAR EEN INTERFACE VAN CARDS NIET ALGEMEEN CARD
    private List<GenericTile> cards = new List<GenericTile>();

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
        rounds += 1;
        roundText.text = "Ronde: " + rounds.ToString() + "/10";
        //increase the chance of a newsflash
        newsFlashChance += 0.1f;

        if(hold == false)
        {
            foreach (PlayerController player in players)
            {
                currentPlayerText.text = "Current player: " + player.name;

                yield return StartCoroutine(player.PlayRound(Waypoints));
            }
        }

        if (UnityEngine.Random.value <= 1f && newsFlashHappend == false && hold == false )
        {

            hold = true;
            if (newsFlashPrefab != null)
            {
               StartNewsFlash();
            }
            else
            {
                Debug.LogWarning("NewsFlashScreen prefab reference is null. Assign the prefab in the Inspector.");
            }
        }
        if (newsFlashHappend == true)
        {
            newsFlashHappend = false;
        }
        



        if (rounds <= 10)
        {
            StartCoroutine(PlayRounds());
        }
    }

    public void StartNewsFlash() {
        
        newsFlashHappend = true;
        GameObject newsFlashScreen = Instantiate(newsFlashPrefab, canvas.transform, true);
        // Activate the instantiated NewsFlashScreen
        newsFlashScreen.SetActive(true);
        Debug.Log("Newsflash prefab instantiated.");
        HideGame();

    }
    public void HideGame()
    {
        //hide all game objects
        foreach (var item in players)
        {   
            item.gameObject.SetActive(false);
        }
        foreach (var item in Waypoints)
        {
            item.gameObject.SetActive(false);
        }
        currentPlayerText.gameObject.SetActive(false);
        roundText.gameObject.SetActive(false);
        // close grid
        grid.gameObject.SetActive(false);
        

    }
    public void OpenGame()
    {
        //show al items agian
        foreach (var item in players)
        {
            item.gameObject.SetActive(true);
        }
        foreach (var item in Waypoints)
        {
            item.gameObject.SetActive(true);
        }
        currentPlayerText.gameObject.SetActive(true);
        roundText.gameObject.SetActive(true);
        grid.gameObject.SetActive(true);
        hold = false;
        StartCoroutine(PlayRounds());
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
