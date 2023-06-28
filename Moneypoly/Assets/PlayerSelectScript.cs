using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectScript : MonoBehaviour
{
    public Button SelectButton;
    public TMP_InputField PlayerNameField;
    public Image CharacterSelectImage;
    public Button NextImageButton;
    public Sprite[] PossibleCharacters;
    private int ImageInteger;
    public PlayerSelectTab AssignedPlayerTab;

    // Start is called before the first frame update
    void Start()
    {
        ImageInteger = 0;
        CharacterSelectImage.sprite = PossibleCharacters[ImageInteger];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextCharacterImage()
    {
        ImageInteger += 1;

        if (ImageInteger >= PossibleCharacters.Length)
        {
            ImageInteger = 0;
        }
        if (PossibleCharacters[ImageInteger] != null)
        {
            CharacterSelectImage.sprite = PossibleCharacters[ImageInteger];
        }
        else
        {
            NextCharacterImage();
        }
       
    }

    public void SelectPlayer()
    {
        AssignedPlayerTab.SelectedPlayer = new Player(PossibleCharacters[ImageInteger], PlayerNameField.text);
        this.RemovePossibleCharactersForAllPlayers();
        AssignedPlayerTab.InitScreen();
        PlayerNameField.interactable = false;
        SelectButton.gameObject.SetActive(false);
        NextImageButton.gameObject.SetActive(false);

        GameObject playerSelectedScreen = GameObject.Find("PlayersSelectedScreen");
        playerSelectedScreen.GetComponent<PlayerSelectedScreen>().Add(AssignedPlayerTab.SelectedPlayer);
        
        AssignedPlayerTab.gameObject.SetActive(true);
    }

    public void RemovePossibleCharactersForAllPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("PlayerSelectObject");

        foreach (GameObject p in players)
        {
            PlayerSelectScript ps = p.GetComponent<PlayerSelectScript>();
            if(ps.CharacterSelectImage.sprite == ps.PossibleCharacters[ImageInteger] && p != gameObject)
            {
                ps.NextCharacterImage();
            }
            ps.PossibleCharacters[ImageInteger] = null;
        }
    }

    public void AddPossibleCharacter(Sprite s)
    {

        for (int i = 0; i < PossibleCharacters.Length; i++)
        {
            if (PossibleCharacters[i] == null)
            {
                PossibleCharacters[i] = s;
                break; 
            }
        }
    }
}
