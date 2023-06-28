using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectedScreen : MonoBehaviour
{
    public List<Player> Players = new List<Player>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(Player player)
    {
        Players.Add(player);

        if(Players.Count >= 2 ) 
        {
            ChangeButtonVisiblity(true);
        }
    }
    
    public void Remove(Player player)
    {
        Players.Remove(player);
        if(Players.Count < 2)
        {
            ChangeButtonVisiblity(false);
        }
    }

    public void ChangeButtonVisiblity(bool b)
    {
        GameObject Button = GameObject.Find("PlayButton");
        Button.GetComponent<Button>().interactable = b;
    }
}
