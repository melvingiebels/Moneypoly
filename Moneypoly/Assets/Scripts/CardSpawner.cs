using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab;
    private List<Stock> wallet = new List<Stock>();
    private Dictionary<Stock, int> stocks = new();
    public float cardSpacing = 20.0f; // Spacing between each card

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetStocks(Dictionary<Stock,int> stocks)
    {
        this.stocks = stocks;
        SpawnCards();
        
    }


    void SpawnCards()
    {
        DestroyExistingCards();
        Vector3 startPosition = transform.position; // Starting position for the first card
        Vector3 currentPosition = Vector3.zero; // Current position relative to the card spawner object

        StartCoroutine(SpawnCardsWithDelay(startPosition, currentPosition));
    }

    private IEnumerator SpawnCardsWithDelay(Vector3 startPosition, Vector3 currentPosition)
    {
        Dictionary<Stock, int> stocksCopy = new Dictionary<Stock, int>(stocks); // Create a copy of the stocks dictionary
        Canvas canvas = GetComponentInParent<Canvas>(); // Get the Canvas component of the parent

        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        Vector2 canvasSize = canvasRectTransform.sizeDelta;

        foreach (KeyValuePair<Stock, int> stockPair in stocksCopy)
        {
            Stock stock = stockPair.Key;
            int quantity = stockPair.Value;

            GameObject cardInstance = Instantiate(cardPrefab, canvas.transform);

            RectTransform cardRectTransform = cardInstance.GetComponent<RectTransform>();

            // Calculate the anchored position based on the Canvas's size
            Vector2 anchoredPosition = new Vector2(
                (currentPosition.x / canvasSize.x), // Adjust the x-value to shift slightly to the left
                0.075f // Set y-value to 0.5f for centering vertically
            );

            cardRectTransform.anchorMin = anchoredPosition;
            cardRectTransform.anchorMax = anchoredPosition;

            cardRectTransform.anchoredPosition = Vector2.zero; // Set anchored position to zero to center horizontally

            Canvas cardCanvas = cardInstance.GetComponentInChildren<Canvas>();
            cardCanvas.worldCamera = Camera.main;
            cardInstance.GetComponent<CardController>().SetCardData(stock, quantity);

            currentPosition += new Vector3(cardSpacing + cardRectTransform.rect.width, 0f, 0f);

            yield return new WaitForSeconds(0.5f); // Delay of 0.5 seconds between each card spawn
        }
    }



    void DestroyExistingCards()
    {
        // Find all GameObjects with the CardController component and destroy them
        CardController[] cardControllers = FindObjectsOfType<CardController>();
        foreach (CardController cardController in cardControllers)
        {
            Destroy(cardController.gameObject);
        }
    }






}
