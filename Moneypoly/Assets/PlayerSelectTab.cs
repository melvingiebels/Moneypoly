using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectTab : MonoBehaviour
{
    public Player SelectedPlayer;
    public TMP_Text PlayerNameText;
    public Image PlayerImage;
    public Button DeleteButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitScreen()
    {
        PlayerNameText.text = SelectedPlayer.PlayerName;
        PlayerImage.sprite = SelectedPlayer.PlayerModel;
    }
    public void RemovePlayer()
    {
        GameObject playerSelectedScreen = GameObject.Find("PlayersSelectedScreen");
        playerSelectedScreen.GetComponent<PlayerSelectedScreen>().Remove(SelectedPlayer);
        SelectedPlayer = null;
        GameObject[] playerSelectObject = GameObject.FindGameObjectsWithTag("PlayerSelectObject");
        
        foreach (GameObject gameObject in playerSelectObject)
        {
            PlayerSelectScript ps = gameObject.GetComponent<PlayerSelectScript>();
            if (PlayerNameText.text == ps.PlayerNameField.text)
            {
                ps.SelectButton.gameObject.SetActive(true);
                ps.NextImageButton.gameObject.SetActive(true);
                ps.PlayerNameField.interactable = true;
            }

            ps.AddPossibleCharacter(PlayerImage.sprite);

        }
    }
}
