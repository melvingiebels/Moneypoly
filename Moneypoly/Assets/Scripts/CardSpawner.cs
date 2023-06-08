using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public string playerName;
    public GameObject cardPrefab;
    private List<Stock> wallet = new List<Stock>();
    public float cardSpacing = 20.0f; // Spacing between each card

    // Start is called before the first frame update
    void Start()
    {
        SpawnCards();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetStocks(List<Stock> stocks)
    {
        wallet = stocks;
        
    }


    void SpawnCards()
    {
        Vector3 startPosition = transform.position; // Starting position for the first card
        Vector3 currentPosition = Vector3.zero; // Current position relative to the card spawner object

        StartCoroutine(SpawnCardsWithDelay(startPosition, currentPosition));
    }

    private IEnumerator SpawnCardsWithDelay(Vector3 startPosition, Vector3 currentPosition)
    {
        foreach (Stock cardData in wallet)
        {
            GameObject cardInstance = Instantiate(cardPrefab, startPosition + currentPosition, Quaternion.identity);
            Canvas cardCanvas = cardInstance.GetComponentInChildren<Canvas>();
            cardCanvas.worldCamera = Camera.main;

            cardInstance.GetComponent<CardController>().SetCardData(cardData);

            currentPosition += new Vector3(cardSpacing + 60, 0f, 0f);

            yield return new WaitForSeconds(0.5f); // Delay of 0.5 seconds between each card spawn
        }
    }






}
