using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsFlash
{
    public int id;
    public string subject;
    public string text;
    public string negativeEffectText;
    public string positiveEffectText;
    public bool isPositive;
    public int effectOnStockPrice;
    public string branchName;

    public NewsFlash(int id, string subject, string text, string negativeEffectText, string positiveEffectText, bool isPositive, int effectOnStockPrice, string branchName)
    {
        this.id = id;
        this.subject = subject;
        this.text = text;
        this.negativeEffectText = negativeEffectText;
        this.positiveEffectText = positiveEffectText;
        this.isPositive = isPositive;
        this.effectOnStockPrice = effectOnStockPrice;
        this.branchName = branchName;
    }
}

