using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public PlayerController[] players;
    public Transform[] waypoints;
    public TMP_Text currentPlayerText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayRounds());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PlayRounds()
    {
        foreach (PlayerController player in players)
        {
            currentPlayerText.text = "Current player: " + player.name;

            yield return StartCoroutine(player.PlayRound(waypoints));
        }

        StartCoroutine(PlayRounds());
    }
}
