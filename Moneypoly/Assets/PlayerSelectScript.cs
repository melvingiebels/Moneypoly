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

        CharacterSelectImage.sprite = PossibleCharacters[ImageInteger];
    }

    public void SelectPlayer()
    {
        AssignedPlayerTab.SelectedPlayer = new Player(PossibleCharacters[ImageInteger], PlayerNameField.text);
        AssignedPlayerTab.InitScreen();
        AssignedPlayerTab.gameObject.SetActive(true);
    }
}
