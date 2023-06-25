using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;
public class Dice : MonoBehaviour
{

    private SpriteRenderer objectRenderer;
    private Sprite[] diceSides;

    public Sprite dice1;

    public AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        diceSides = Resources.LoadAll<Sprite>("Dice/");
        objectRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public int RollDice()
    {
        Random rnd = new Random();
        int die = 0;
        die = rnd.Next(0, 5);

        // Set sprite to upper face of dice from array according to random value
        dice1 = diceSides[die];

        objectRenderer.sprite = dice1;
        
        Debug.Log("DiceNumber: " + die);
        return die + 1;

    }

    public IEnumerator RollDiceAnimation()
    {
        // Roll the dice
        Random rnd = new Random();
        audio.Play();
        int die = 0;
        for (int i = 0; i <= 20; i++)
        {
            // Pick up random value from 0 to 5 (All inclusive)
            die = rnd.Next(0, 5);

            // Set sprite to upper face of dice from array according to random value
            dice1 = diceSides[die];

            objectRenderer.sprite = dice1;

            // Pause before next itteration
            yield return new WaitForSeconds(0.05f);
        }
        yield return die;
    }
}
