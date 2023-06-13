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
    private bool stopActions = false;
    private bool isMoving = false;
    private SpriteRenderer objectRenderer;

    void Start()
    {

    }
    private void Awake()
    {
        objectRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }
    internal IEnumerator WaitForInput(KeyCode keyCode)
    {
        waitForInput = true;

        while (waitForInput)
        {
            yield return null;

            if (Input.GetKeyDown(keyCode) && !stopActions)
            {
                waitForInput = false;
            }
        }
    }


    public IEnumerator PlayRound(Transform[] waypoints)
    {
        dialogueText.text = "Press spacebar to start your turn and roll the dice";
        yield return WaitForInput(KeyCode.Space);
        if (!isMoving)
        {
            int sum = RollDie();
            dialogueText.text = "Dice Roll: " + sum.ToString();

            dialogueText.text = "Moving to the destination";

            isMoving = true;
            yield return MovePlayer(waypoints, sum);
            isMoving = false;
            dialogueText.text = "Press spacebar to end your turn";
            yield return WaitForInput(KeyCode.Space);
        }
        yield break;
    }

    internal IEnumerator MovePlayer(Transform[] waypoints, int diceRoll)
    {
        int targetWaypointIndex = (startPoint + diceRoll) % waypoints.Length;

        for (int i = startPoint; i != targetWaypointIndex; i = (i + 1) % waypoints.Length)
        {
            yield return MoveToWaypoint(waypoints[i]);
        }

        startPoint = targetWaypointIndex;
    }

    internal IEnumerator MoveToWaypoint(Transform waypoint)
    {
        isMoving = true;

        while (Vector2.Distance(transform.position, waypoint.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoint.position, movementSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }

    internal int RollDie()
    {
        // Roll the dice
        int die1;
        int die2;

        Random rnd = new Random();
        die1 = rnd.Next(1, 7);
        die2 = rnd.Next(1, 7);

        die1Text.text = die1.ToString();
        die2Text.text = die2.ToString();

        return die1 + die2;
    }

    public void Initialize(Sprite sprite,float yChord)
    {
        objectRenderer.sprite = sprite;
        var transform = new Vector3(-152.3f, yChord);
        objectRenderer.transform.position = transform;
    }

}
