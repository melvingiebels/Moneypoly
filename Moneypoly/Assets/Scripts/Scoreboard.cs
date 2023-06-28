using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public Canvas canvas;
    public GameObject scoreboardCard;
    private BoardController boardController;
    public float cardSpacing = -120.0f; // Spacing between each card
    // Start is called before the first frame update
    void Start()
    {
        boardController = GameObject.Find("BoardController").GetComponent<BoardController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setScoreBoard()
    {
        Debug.Log("Setting scoreboard");
        float yOffset = 450f; // Initial y-offset for the first card

        int f = 0;

        // Loop through the players and create a scoreboard card for each player
        foreach (PlayerController player in boardController.players)
        {
            f++;
            // Instantiate a new scoreboard card GameObject
            GameObject newScoreboardCard = Instantiate(scoreboardCard, canvas.transform);

            RectTransform cardRectTransform = newScoreboardCard.GetComponent<RectTransform>();

            // Calculate the anchored position based on the Canvas's size
            Vector2 anchoredPosition = new Vector2(
                -780f, // Set x-value to 0f for left alignment
                +yOffset // Set y-value to negative yOffset for vertical positioning
            );


            cardRectTransform.anchoredPosition = anchoredPosition;

            // Set the player name and score on the scoreboard card
            Transform playerTransform = newScoreboardCard.transform.Find("Player");
            
            TMP_Text playerScoreText = playerTransform.Find("PlayerScore").GetComponent<TMP_Text>();
            playerScoreText.text = "€ " + (player.playerInventory.netWorth + player.playerInventory.budget).ToString();

            TMP_Text rankingPlayerText = playerTransform.Find("Position").GetComponent<TMP_Text>();
            rankingPlayerText.text = f + "ᵉ";

            // Set the ranking color based on position
            switch (f)
            {
                case 1:
                    rankingPlayerText.color = new Color(255, 248, 0);
                    break;
                case 2:
                    rankingPlayerText.color = new Color(192, 192, 192);
                    break;
                case 3:
                    rankingPlayerText.color = new Color(205, 127, 50); // Bronze color
                    break;
                default:
                    rankingPlayerText.color = new Color(255, 255, 255);
                    break;
            }

            GameObject playerIcon = playerTransform.Find("PlayerIcon").gameObject;
            Image playerIconImage = playerIcon.GetComponent<Image>();

            // Clear the sprite of the Image component
            playerIconImage.sprite = null;
            playerIconImage.sprite = player.GetComponent<SpriteRenderer>().sprite;
            // Destroy(playerIcon.GetComponent<SpriteRenderer>());

            //GameObject newPlayerIcon = new GameObject("PlayerIcon");
            //newPlayerIcon.transform.SetParent(playerTransform, false); 

            // SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();



            // Increase yOffset by the height of the card
            yOffset += cardSpacing;
        }
    }


    public void updateScoreboard()
    {
        Debug.Log("Updating scoreboard");

        RemoveScoreboard();

        // Sort the players based on their scores
        List<PlayerController> sortedPlayers = new List<PlayerController>(boardController.players);
        sortedPlayers.Sort((a, b) => (b.playerInventory.netWorth + b.playerInventory.budget).CompareTo(a.playerInventory.netWorth + a.playerInventory.budget));

        float yOffset = 450f; // Initial y-offset for the first card
;

        // Loop through the players and create a scoreboard card for each player
        for (int i = 0; i < sortedPlayers.Count; i++)
        {

            // Instantiate a new scoreboard card GameObject
            GameObject newScoreboardCard = Instantiate(scoreboardCard, canvas.transform);

            RectTransform cardRectTransform = newScoreboardCard.GetComponent<RectTransform>();

            // Calculate the anchored position based on the Canvas's size
            Vector2 anchoredPosition = new Vector2(
                -780f, // Set x-value to 0f for left alignment
                +yOffset // Set y-value to negative yOffset for vertical positioning
            );

            cardRectTransform.anchoredPosition = anchoredPosition;


            // Set the player name and score on the scoreboard card
            Transform playerTransform = newScoreboardCard.transform.Find("Player");

            TMP_Text playerScoreText = playerTransform.Find("PlayerScore").GetComponent<TMP_Text>();
            playerScoreText.text = "€ " + (sortedPlayers[i].playerInventory.netWorth + sortedPlayers[i].playerInventory.budget).ToString();

            TMP_Text playerScoreBudgetText = playerTransform.Find("PlayerScoreBudget").GetComponent<TMP_Text>();
            playerScoreBudgetText.text = "B: € " + sortedPlayers[i].playerInventory.budget.ToString();


            TMP_Text playerScoreWalletText = playerTransform.Find("PlayerScoreWallet").GetComponent<TMP_Text>();
            playerScoreWalletText.text = "P: € " + sortedPlayers[i].playerInventory.netWorth.ToString();


            TMP_Text rankingPlayerText = playerTransform.Find("Position").GetComponent<TMP_Text>();
            rankingPlayerText.text = (i + 1) + "ᵉ";

            // Set the ranking color based on position
            switch (i)
            {
                case 0:
                    rankingPlayerText.color = new Color(255, 248, 0);
                    break;
                case 1:
                    rankingPlayerText.color = new Color(192, 192, 192);
                    break;
                case 2:
                    rankingPlayerText.color = new Color(205, 127, 50); // Bronze color
                    break;
                default:
                    rankingPlayerText.color = new Color(255, 255, 255);
                    break;
            }

            GameObject playerIcon = playerTransform.Find("PlayerIcon").gameObject;
            Image playerIconImage = playerIcon.GetComponent<Image>();

            // Clear the sprite of the Image component
            playerIconImage.sprite = null;
            playerIconImage.sprite = sortedPlayers[i].GetComponent<SpriteRenderer>().sprite;

            yOffset += cardSpacing;
        }
    }


    public void RemoveScoreboard()
    {
        if (GameObject.FindGameObjectsWithTag("ScoreboardCard").Length > 0)
        {
            // Remove previous scoreboard cards
            GameObject[] previousScoreboardCards = GameObject.FindGameObjectsWithTag("ScoreboardCard");
            foreach (GameObject card in previousScoreboardCards)
            {
                Destroy(card);
            }
        }
    }

   
     
}
