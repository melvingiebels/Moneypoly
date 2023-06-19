using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using static System.Runtime.CompilerServices.RuntimeHelpers;
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
    [SerializeField] private LocationCardScript locationCard;
    public PlayerInventory playerInventory;
    private bool isOpen= false;
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
    internal IEnumerator WaitForInput()
    {
        bool waitForSpacebar = false;

        while (!waitForSpacebar)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
            {
                playerInventory.transform.parent.gameObject.SetActive(!playerInventory.transform.parent.gameObject.activeInHierarchy);
                // Handle specific input keys here (if needed)
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                waitForSpacebar = true;
            }
        }

        // Spacebar has been pressed
        // Proceed to the next function or action
    }

    public IEnumerator PlayRound(List<WaypointComponent> waypoints)
    {
        dialogueText.text = "Press spacebar to start your turn and roll the dice";
        yield return StartCoroutine(WaitForInput());

        if (!isMoving)
        {
            int sum = RollDie();
            dialogueText.text = "Dice Roll: " + sum.ToString();

            dialogueText.text = "Moving to the destination";

            isMoving = true;
            yield return MovePlayer(waypoints, sum);
            isMoving = false;
            dialogueText.text = "Press spacebar to end your turn";
            yield return StartCoroutine(WaitForInput());
        }
        yield break;
    }

    internal IEnumerator MovePlayer(List<WaypointComponent> waypoints, int diceRoll)
    {
        int targetWaypointIndex = (startPoint + diceRoll) % waypoints.Count;

        for (int i = startPoint; i != targetWaypointIndex; i = (i + 1) % waypoints.Count)
        {
            yield return MoveToWaypoint(waypoints[i]);
        }

        startPoint = targetWaypointIndex;

        locationCard.Open();
        locationCard.FillDataFromWaypoint(waypoints[targetWaypointIndex], playerInventory);
    }

    internal IEnumerator MoveToWaypoint(WaypointComponent waypoint)
    {
        isMoving = true;

        while (Vector2.Distance(transform.position, waypoint.transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoint.transform.position, movementSpeed * Time.deltaTime);
            yield return null;
        }

        // The movement is completed, so set isMoving to false after the while loop
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
