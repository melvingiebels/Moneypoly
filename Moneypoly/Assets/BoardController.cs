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
    public float newsFlashChance = 0.0f;
    public bool hold = false;
    public Grid grid;
    public Canvas canvas;
    [SerializeField] private CardScript cardSettingScript;
    private NewsFlash newsFlash;
    private int newsEffectRound = 9;
    public GameObject newsPopup;
 
    // TODO:: DIT VERANDEREN NAAR EEN INTERFACE VAN CARDS NIET ALGEMEEN CARD
    private List<GenericTile> cards = new List<GenericTile>();

    // Reference to the Scoreboard component
    public Scoreboard scoreboard;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
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
            } else if (cards[waypointIndex].specialWaypoint != null)
            {

            }
            else
            {
                waypointComponent.CardScript = cardSettingScript;
            }
            // else, init the other types of waypoints

            // Set the parent of the waypoint to keep the hierarchy organized 
            waypoint.transform.SetParent(GetComponent<Transform>());

            // Add the waypoint to the waypoint list
            Waypoints.Add(waypointComponent);

            if (waypointIndex >= 30)
            {
                xIncrement = -28;
                yIncrement = 0;
            }
            else if (waypointIndex >= 20)
            {
                yIncrement = -29;
                xIncrement = 0;
            }
            else if (waypointIndex >= 10)
            {
              
                xIncrement = 26;

                if (waypointIndex >= 15)
                {
                    xIncrement = 28;
                }
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
        scoreboard.updateScoreboard();
        //increase the chance of a newsflash

        if (UnityEngine.Random.value <= newsFlashChance && newsFlashHappend == false && hold == false && rounds > 1)
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

        if (hold == false)
        {
            StockMarket.UpdateStockPrices();
            scoreboard.updateScoreboard();
            rounds += 1;
            newsFlashChance += 0.15f;
            roundText.text = "Ronde: " + rounds.ToString() + "/10";
            
            foreach (PlayerController player in players)
            {
                currentPlayerText.text = "Current player: " + player.name;

                yield return StartCoroutine(player.PlayRound(Waypoints));
            }
        }

        //check if newsEffectRound is not null and equal to the current round
        if (newsEffectRound == rounds)
        {
            Debug.Log("NewsEffectRound is equal to rounds");
            //update the stock price
            Debug.Log(newsFlash);
            StockMarket.UpdateBranchPriceByName(newsFlash.branchName, newsFlash.effectOnStockPrice, newsFlash.isPositive);
            //show the newsPopup
            newsPopup.SetActive(true);
            NewsEffect newsEffect = FindObjectOfType<NewsEffect>();
            newsEffect.ShowNews(newsFlash);

        }

        
        if (rounds <= 10)
        {
            StartCoroutine(PlayRounds());
        }
        
    }

    public void StartNewsFlash()
    {
        newsEffectRound = rounds;
        newsEffectRound += 2; // Increment the value by 1
        Debug.Log("NewsEffectRound is set to: " + newsEffectRound);
        newsFlashHappend = true;
        GameObject newsFlashScreen = Instantiate(newsFlashPrefab, canvas.transform, false);
        // Set the parent of the instantiated NewsFlashScreen to canvas and preserve the world position
        RectTransform newsFlashRectTransform = newsFlashScreen.GetComponent<RectTransform>();
        newsFlashRectTransform.SetParent(canvas.transform, false);
        // Calculate the size and position of the NewsFlashScreen relative to the canvas size
        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;
        newsFlashRectTransform.sizeDelta = canvasSize;
        newsFlashRectTransform.anchoredPosition = Vector2.zero;
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
    public void OpenGame(NewsFlash newsFlash)
    {
        this.newsFlash = newsFlash;
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
                    break;
                }
            }
        }
    }
    public List<PlayerInventory> GetPlayers()
    {
        List<PlayerInventory> playerInventories = new List<PlayerInventory>();
        foreach (var item in players)
        {
            playerInventories.Add(item.playerInventory);
        }
        return playerInventories;
    }
}
