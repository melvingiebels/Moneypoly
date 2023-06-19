using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTile : MonoBehaviour
{
    public AlgemeenCard card;
    public Stock Stock;
    public string specialWaypoint;

    public GenericTile(AlgemeenCard card, Stock stock, string specialWaypoint)
    {
        this.card = card;
        Stock = stock;
        this.specialWaypoint = specialWaypoint;
    }
}
