using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlgemeenCard
{
    public int id;
    public string branch;
    public int branchId;
    public string company;
    public int companyId;
    public string description;
    public bool changeBool;
    public int percentage;

    public AlgemeenCard(int id, string branch, string company, string description, bool changeBool, int percentage)
    {
        this.id = id;
        this.branch = branch;
        this.company = company;
        this.description = description;
        this.changeBool = changeBool;
        this.percentage = percentage;
    }

    public AlgemeenCard()
    {

    }
}


public class KansCard
{
    public int id;
    public string description;
    public int geld;
    public bool changeBool;
    public bool action;

    public KansCard(int id, string description,int geld,bool changeBool,bool action)
    {
        this.id = id;
        this.description = description;
        this.geld = geld;
        this.changeBool = changeBool;
        this.action = action;
    }

    public KansCard()
    {

    }
}

public class CardScript : MonoBehaviour
{
    public TextAsset textAssetAlgemeenData;
    public TextAsset textAssetKansData;

    public List<AlgemeenCard> algemeenDeck;
    public List<AlgemeenCard> algemeenDeckEmpty;

    public List<KansCard> kansDeck;
    public List<KansCard> kansDeckEmpty;

    public GameObject algemeenCardDeckGameObject;
    public GameObject kansCardDeckGameObject;

    // Start is called before the first frame update
    void Start()
    {
        ImportAlgemeenDeck();
        ImportKansDeck();
        algemeenCardDeckGameObject = this.gameObject.transform.GetChild(0).gameObject;
        algemeenCardDeckGameObject.SetActive(false);
        kansCardDeckGameObject = this.gameObject.transform.GetChild(1).gameObject;
        kansCardDeckGameObject.SetActive(false);

    }

    public void ShowCardAlgemeen()
    {
        if (!algemeenCardDeckGameObject.activeSelf)
        {
            if(algemeenDeck.Count == 0)
            {
                algemeenDeck = algemeenDeckEmpty;
                algemeenDeckEmpty = new List<AlgemeenCard>();
            }

            Console.Write(algemeenDeck);
            int rnd = Random.Range(0, algemeenDeck.Count);
            TextMeshProUGUI[] txtmeshpro = algemeenCardDeckGameObject.GetComponentsInChildren<TextMeshProUGUI>(true);

            string s = "";
            if (algemeenDeck[rnd].branch != "N/A")
            {
                s = s + algemeenDeck[rnd].branch;
            }
            if (algemeenDeck[rnd].company != "N/A")
            {
                s = s + " " + algemeenDeck[rnd].company;
            }

            txtmeshpro[0].text = s;
            txtmeshpro[1].text = algemeenDeck[rnd].description;
            algemeenCardDeckGameObject.SetActive(true);
            algemeenDeckEmpty.Add(algemeenDeck[rnd]);
            algemeenDeck.Remove(algemeenDeck[rnd]);
        }
    }

    public void ShowCardKans()
    {
        if (!kansCardDeckGameObject.activeSelf)
        {

            if(kansDeck.Count == 0)
            {
                kansDeck = kansDeckEmpty;
                kansDeckEmpty = new List<KansCard>();
            }
            Console.Write(kansDeck);
            int rnd = Random.Range(0, kansDeck.Count);
            TextMeshProUGUI[] txtmeshpro = kansCardDeckGameObject.GetComponentsInChildren<TextMeshProUGUI>(true);
            txtmeshpro[1].text = kansDeck[rnd].description;
            if (!kansDeck[rnd].action)
            {
                txtmeshpro[1].text = txtmeshpro[1].text + " " + kansDeck[rnd].geld + " euro";
            }
            kansCardDeckGameObject.SetActive(true);
            kansDeckEmpty.Add(kansDeck[rnd]);
            kansDeck.Remove(kansDeck[rnd]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.ShowCardAlgemeen();
            this.ShowCardKans();
        }
        if (Input.GetMouseButtonDown(0))
        {
            algemeenCardDeckGameObject.SetActive(false);
            kansCardDeckGameObject.SetActive(false);
        }

    }

    private void ImportAlgemeenDeck()
    {
        string[] lines = textAssetAlgemeenData.text.Split('\n');
        Console.Write(lines);
        algemeenDeck = new List<AlgemeenCard>();
        algemeenDeckEmpty =  new List<AlgemeenCard>();
        for (int i = 0; i < lines.Length - 2; i++)
        {
            string[] fields = lines[i +1].Split(';');
            algemeenDeck.Add(new AlgemeenCard());
            algemeenDeck[i].id = int.Parse(fields[0]);
            algemeenDeck[i].branch = fields[1];
            if (fields[2] != "N/A") algemeenDeck[i].branchId = int.Parse(fields[2]);
            algemeenDeck[i].company = fields[3];
            if(fields[4] != "N/A") algemeenDeck[i].companyId = int.Parse(fields[4]);
            algemeenDeck[i].description = fields[5];
            algemeenDeck[i].changeBool = bool.Parse(fields[6]);
            algemeenDeck[i].percentage = int.Parse(fields[7]);

        }
    }

    private void ImportKansDeck()
    {
        string[] lines = textAssetKansData.text.Split('\n');
        Console.Write(lines);
        kansDeck = new List<KansCard>();
        kansDeckEmpty = new List<KansCard>();
        for (int i = 0; i < lines.Length - 2; i++)
        {
            string[] fields = lines[i + 1].Split(';');
            kansDeck.Add(new KansCard());
            kansDeck[i].id = int.Parse(fields[0]);
            kansDeck[i].description = fields[1];
            kansDeck[i].geld = int.Parse(fields[2]);
            kansDeck[i].changeBool = bool.Parse(fields[3]);
            kansDeck[i].action = bool.Parse(fields[4]);
        }
    }
}
