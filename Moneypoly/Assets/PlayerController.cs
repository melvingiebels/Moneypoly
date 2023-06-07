using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    public TMP_Text die1Text;
    public TMP_Text die2Text;
    public int startPoint;
    public float movementSpeed = 5f;
    public TMP_Text dialogueText;
    private bool waitForInput = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator PlayRound(Transform[] waypoints)
    {
        dialogueText.gameObject.SetActive(true);
        dialogueText.text = "Press spacebar to start your turn, and roll the die";

        waitForInput = true;

        while (waitForInput)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueText.gameObject.SetActive(false);
                waitForInput = false;
                yield return StartCoroutine(RollDie());
            }
        }

        dialogueText.gameObject.SetActive(true);
        dialogueText.text = "Moving to the destination";

        // move the amount of waypoints of the dice that are rolled
        yield return StartCoroutine(MovePlayer(waypoints));

        // after this is over, show a button and wait for it to be pressed;
        waitForInput = true;
        while (waitForInput)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueText.gameObject.SetActive(false);
                yield return StartCoroutine(RollDie());
            }
        }
        // then the onclick of the button handles the rest
    }
    internal IEnumerator MovePlayer(Transform[] waypoints)
    {
        for (int i = startPoint; i < waypoints.Length; i++)
        {
            // moves to the desired waypoint
            StartCoroutine(MoveToWaypoint(waypoints[i]));
        }
        yield return null;
    }
    internal IEnumerator MoveToWaypoint(Transform waypoint)
    {
        while (Vector3.Distance(transform.position, waypoint.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoint.position, movementSpeed * Time.deltaTime);
            yield return null;
        }
    }
    internal IEnumerator RollDie()
    {
        // roll the dice
        int die1;
        int die2;

        Random rnd = new Random();
        die1 = rnd.Next(1, 7);
        die2 = rnd.Next(1, 7);

        die1Text.text = die1.ToString();
        die2Text.text = die2.ToString();

        yield return null;
    }
}
