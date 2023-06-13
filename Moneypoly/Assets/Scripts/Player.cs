using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    // Start is called before the first frame update
    internal Sprite PlayerModel { get; set; }
    internal string PlayerName { get; set; }
    public Player(Sprite playerModel, string playerName)
    {
        PlayerModel = playerModel;
        PlayerName = playerName;
    }
}
