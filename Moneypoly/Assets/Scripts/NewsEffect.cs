using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewsEffect : MonoBehaviour
{
    public TMP_Text newsSubject;
    public TMP_Text newsText;
    public TMP_Text newsEffectText;
    public Button continueButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowNews(NewsFlash newsFlash)
    {
        //activate the object
        gameObject.SetActive(true);
        continueButton.onClick.AddListener(() =>
        {
            //deactivate the object
            gameObject.SetActive(false);
        });
        newsSubject.text = newsFlash.subject;
        newsText.text = newsFlash.text;
        if (newsFlash.isPositive)
        {
            
            newsText.text = newsFlash.positiveEffectText;
            newsEffectText.text = "🠉 " + newsFlash.effectOnStockPrice.ToString() + "    voor: "+ newsFlash.branchName;
            //set text color to green
            newsEffectText.color = Color.green;

        }
        else
        {
            newsText.text = newsFlash.negativeEffectText;
            newsEffectText.text = "🠋 " + newsFlash.effectOnStockPrice.ToString() + "    voor: " + newsFlash.branchName;
            //set text color to red
            newsEffectText.color = Color.red;
        }
    }

}
