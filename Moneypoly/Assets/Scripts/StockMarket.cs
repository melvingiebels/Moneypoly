using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class StockMarket : MonoBehaviour
{

    private List<Stock> stocks = new List<Stock>();
    private string[] branchNames;
    public int currentRound = 1;
    public int totalRounds = 10;

    public float maxBranchPrecentageChange = 5f;
    public float minBranchPrecentageChange = -5f;

    public float maxBusinessPrecentageChange = 2f;
    public float minBusinessPrecentageChange = -2f;

    private PlayerInventory playerInventory;

    private void Start()
    {
        // Initialize the stocks
        InitializeStocks();
    }

    private void InitializeStocks()
    {
        branchNames = new string[]
        {
            "Media en communicatie",
            "Vastgoed",
            "Gezondheidszorg en -welzijn",
            "Milieu en Agrarische sector",
            "ICT / Technisch",
            "Handel en dienstverlening"
        };
        // Create instances of stocks and assign values
        stocks = new List<Stock>
        {
         new Stock(
            "Meta Business",
            "META",
            branchNames[0],
            "Meta is een technologiebedrijf dat zich richt op augmented reality (AR) en virtual reality (VR). Het ontwikkelt geavanceerde AR/VR-producten en platforms om gebruikers onder te dompelen in digitale ervaringen en interactie te bieden met de virtuele wereld.",
            10.0f
        ),
        new Stock(
            "Amazon",
            "AMZN",
            branchNames[0],
            "Amazon is een wereldwijd e-commercebedrijf en marktplaats dat een breed scala aan producten en diensten aanbiedt, waaronder online winkelen, streaming van media, cloud computing en kunstmatige intelligentie. Het staat bekend om zijn snelle levering, uitgebreid productaanbod en innovatieve technologische oplossingen.",
            10.0f
        ),
        new Stock(
            "KPN",
            "KPN",
            branchNames[0],
            "KPN is een toonaangevend Nederlands telecommunicatiebedrijf dat zich specialiseert in vaste en mobiele telefonie, internet en televisiediensten. Het levert betrouwbare communicatieoplossingen en infrastructuur aan zowel particuliere als zakelijke klanten.",
            10.0f
        ),
        new Stock(
            "LinkedIn",
            "LNKD",
            branchNames[0],
            "LinkedIn is een online professioneel netwerkplatform dat wereldwijd gebruikers in staat stelt om professionele connecties te maken, vacatures te zoeken en zakelijke informatie uit te wisselen. Het biedt mogelijkheden voor carrièreontwikkeling, netwerken en zakelijke communicatie op verschillende vakgebieden.",
            10.0f
        ),
        new Stock(
            "Koninklijke BAM",
            "BAM",
            branchNames[1],
            "Koninklijke BAM is een internationaal bouwbedrijf dat actief is in de sectoren bouw, vastgoed en infrastructuur. Het biedt expertise in het realiseren van complexe projecten en levert innovatieve oplossingen voor duurzame en hoogwaardige bouwprojecten.",
            10.0f
        ),
        new Stock(
            "Bouwfonds",
            "BOUW",
            branchNames[1],
            "Bouwfonds is een vastgoedontwikkelingsbedrijf dat gespecialiseerd is in het ontwikkelen en beheren van woningen, commercieel vastgoed en infrastructuurprojecten. Het richt zich op het creëren van duurzame en leefbare omgevingen waar mensen kunnen wonen, werken en ontspannen.",
            10.0f
        ),
        new Stock(
            "Heijmans",
            "HEIJ",
            branchNames[1],
            "Heijmans is een toonaangevend Nederlands bouwbedrijf dat actief is in de sectoren vastgoed, woningbouw, utiliteitsbouw en infrastructuur. Het staat bekend om zijn innovatieve en duurzame bouwprojecten, waarbij het streven naar kwaliteit en maatschappelijke betrokkenheid centraal staat.",
            10.0f
        ),
        new Stock(
            "Exor",
            "EXO",
            branchNames[1],
            "Exor is een Italiaanse investeringsmaatschappij die zich richt op diverse sectoren, waaronder verzekeringen, energie, media en transport. Het bedrijf staat bekend om zijn strategische investeringen en het beheer van een gevarieerde portefeuille van wereldwijde bedrijven.",
            10.0f
        ),
        new Stock(
            "Pfizer",
            "PFZ",
            branchNames[2],
            "Pfizer is een wereldwijd farmaceutisch bedrijf dat zich toelegt op de ontwikkeling, productie en verkoop van geneesmiddelen en vaccins. Het staat bekend om zijn innovatieve benadering van de gezondheidszorg en zijn bijdrage aan medische doorbraken en de bestrijding van ziekten.",
            10.0f
        ),
        new Stock(
            "Aurora Cannabis",
            "ACAN",
            branchNames[2],
            "Aurora Cannabis is een Canadees bedrijf dat zich richt op de productie en distributie van medicinale en recreatieve cannabisproducten. Het is een belangrijke speler in de cannabisindustrie en streeft naar innovatie, kwaliteit en wereldwijde marktleiderschap op het gebied van cannabisproducten.",
            10.0f
        ),
        new Stock(
            "Philips",
            "PHL",
            branchNames[2],
            "Philips is een wereldwijd technologiebedrijf dat zich richt op gezondheidszorg, consumentenlevensstijl en verlichting. Het biedt innovatieve oplossingen en producten op het gebied van medische apparatuur, persoonlijke verzorging, huishoudelijke apparaten en verlichtingstechnologie.",
            10.0f
        ),
        new Stock(
            "GlaxoSmithKline (GSK)",
            "GSK",
            branchNames[2],
            "GlaxoSmithKline (GSK) is een wereldwijd farmaceutisch bedrijf dat zich toelegt op de ontwikkeling, productie en verkoop van geneesmiddelen, vaccins en consumentenproducten. Het heeft een sterke focus op innovatie, onderzoek en ontwikkeling, en streeft naar het verbeteren van de gezondheid en kwaliteit van leven van mensen wereldwijd.",
            10.0f
        ),
        new Stock(
            "Eneco",
            "ENEC",
            branchNames[3],
            "Eneco is een toonaangevend Nederlands energiebedrijf dat zich richt op duurzame energieoplossingen en de transitie naar een CO2-neutrale toekomst. Het levert groene stroom, gas en energiediensten aan zowel particuliere als zakelijke klanten, en investeert actief in hernieuwbare energiebronnen en energiebesparende technologieën.",
            10.0f
        ),
        new Stock(
            "Vattenfall",
            "VATF",
            branchNames[3],
            "Vattenfall is een toonaangevend Zweeds energiebedrijf dat actief is in heel Europa. Het richt zich op duurzame energieopwekking, levering en distributie, met een focus op windenergie, zonne-energie en energieopslag, en speelt een belangrijke rol in de energietransitie naar een koolstofarme samenleving.",
            10.0f
        ),
        new Stock(
            "Shell",
            "SHL",
            branchNames[3],
            "Shell is een wereldwijd opererend Nederlands-Brits energiebedrijf dat zich bezighoudt met de exploratie, productie, raffinage en verkoop van olie, gas en petrochemische producten. Het bedrijf staat bekend om zijn uitgebreide downstream-activiteiten, zoals brandstofdistributie en retailnetwerken, en is ook betrokken bij hernieuwbare energie-initiatieven.",
            10.0f
        ),
        new Stock(
            "BP",
            "BP",
            branchNames[3],
            "BP is een wereldwijd energiebedrijf dat zich bezighoudt met de exploratie, productie, raffinage en distributie van olie, gas en hernieuwbare energiebronnen. Het bedrijf streeft naar duurzame energieoplossingen en investeert in groene technologieën om de overgang naar een koolstofarme toekomst te bevorderen.",
            10.0f
        ),
        new Stock(
            "Tesla",
            "TSLA",
            branchNames[4],
            "Tesla is een innovatief Amerikaans automerk dat zich specialiseert in elektrische voertuigen en duurzame energieoplossingen. Het bedrijf staat bekend om zijn geavanceerde technologie, zoals autonoom rijden, en heeft een missie om de wereldwijde transitie naar schone energie te versnellen.",
            10.0f
        ),
        new Stock(
            "Apple",
            "AAPL",
            branchNames[4],
            "Apple is een toonaangevend Amerikaans technologiebedrijf dat wereldwijd bekend staat om zijn innovatieve producten, waaronder de iPhone, iPad, Mac en Apple Watch. Het bedrijf richt zich op het ontwikkelen van gebruiksvriendelijke en hoogwaardige apparaten, software en diensten die de manier waarop mensen communiceren, werken en entertainment beleven transformeren.",
            10.0f
        ),
        new Stock(
            "Alphabet",
            "GOOGL",
            branchNames[4],
            "Alphabet is het moederbedrijf van Google en is een multinational die zich bezighoudt met technologie, internet en AI-gedreven diensten. Het bedrijf richt zich op innovatie, digitale oplossingen en het verbeteren van toegang tot informatie en services over de hele wereld.",
            10.0f
        ),
        new Stock(
            "Microsoft",
            "MSFT",
            branchNames[4],
            "Microsoft is een wereldwijd technologiebedrijf dat zich richt op de ontwikkeling, productie en verkoop van software, hardware en cloudservices. Het bedrijf staat bekend om zijn besturingssysteem Windows, productiviteitssuites zoals Office, en biedt een breed scala aan technologische oplossingen voor zowel consumenten als bedrijven.",
            10.0f
        ),
        new Stock(
            "Unilever",
            "UNI",
            branchNames[5],
            "Unilever is een wereldwijd opererend Nederlands-Brits multinational bedrijf dat zich toelegt op de productie en verkoop van voedingsmiddelen, dranken, schoonheids- en verzorgingsproducten en huishoudelijke artikelen. Het bedrijf heeft een uitgebreide portefeuille van bekende merken en zet zich in voor duurzaamheid en maatschappelijke verantwoordelijkheid.",
            10.0f
        ),
        new Stock(
            "Heineken",
            "HEI",
            branchNames[5],
            "Heineken is een wereldwijd toonaangevend Nederlands bierbrouwerijbedrijf dat bekend staat om zijn hoogwaardige biermerken en internationale aanwezigheid. Het bedrijf produceert en verkoopt een breed scala aan bierproducten en heeft een sterke focus op kwaliteit, innovatie en het creëren van sociale belevingen.",
            10.0f
        ),
        new Stock(
            "PostNL",
            "POS",
            branchNames[5],
            "PostNL is een vooraanstaand Nederlands post- en logistiek bedrijf dat gespecialiseerd is in het bezorgen van post en pakketten, zowel nationaal als internationaal. Het biedt een breed scala aan bezorgdiensten en logistieke oplossingen, met een sterke focus op betrouwbaarheid, efficiëntie en digitale innovatie.",
            10.0f
        ),
        new Stock(
            "KLM",
            "KLM",
            branchNames[5],
            "KLM is de nationale luchtvaartmaatschappij van Nederland en een toonaangevend bedrijf in de luchtvaartindustrie. Het biedt wereldwijde connectiviteit met een uitgebreid netwerk van vluchten, hoogwaardige service aan boord en een focus op duurzaamheid en innovatie.",
            10.0f
        )
        };
        // Find the PlayerInventory script in the scene
        playerInventory = GameObject.FindObjectOfType<PlayerInventory>();  
        if (playerInventory != null)
        {
            playerInventory.BuyStock(stocks[0],2);
            playerInventory.BuyStock(stocks[1],1);
            playerInventory.BuyStock(stocks[5],10);
            Debug.Log("Stocks bought");
        }
       else
        {
            Debug.LogError("No PlayerInventory script found in the scene!");
        }
       
    }

    public void UpdateStockPrices()
    {
        Debug.Log("Updating stock prices...");
        // Calculate the weight for price changes based on the current round
        float weight = 1.0f + ((float)currentRound / totalRounds);

        Dictionary<string, float> branchPriceChanges = new Dictionary<string, float>();

        // Calculate price changes for each branch
        foreach (string branch in branchNames)
        {
            // Calculate the minimum and maximum price change based on the weight
            float minChange = minBranchPrecentageChange * weight;
            float maxChange = maxBranchPrecentageChange * weight;

            // Calculate a random change for the branch
            float branchChange = UnityEngine.Random.Range(minChange, maxChange);
            branchPriceChanges.Add(branch, branchChange);
        }

        // Update stock prices for each stock within each branch
        foreach (string branch in branchNames)
        {
            List<Stock> branchStocks = GetStocksByBranch(branch);

            // Calculate the average price change for the branch
            float branchChange = branchPriceChanges[branch];
            float averageChange = branchChange / branchStocks.Count;

            // Update stock prices within the branch
            foreach (Stock stock in branchStocks)
            {
                // Calculate a random deviation from the average change
                float randomDeviation = UnityEngine.Random.Range(minBusinessPrecentageChange, maxBusinessPrecentageChange) * Mathf.Abs(averageChange);
                float stockChange = averageChange + randomDeviation;

                // Apply the branch-specific and stock-specific price change to the stock's current price
                stock.previousPrice = stock.currentPrice;
                stock.currentPrice += stockChange;

                // Ensure the current price is at least $1
                if (stock.currentPrice <= 0.0f)
                {
                    stock.currentPrice = 1.0f;
                }
            }
        }

        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        playerInventory.CalculateWallet();
        playerInventory.SetStock();
    }

    private List<Stock> GetStocksByBranch(string branchName)
    {
        List<Stock> stocksInBranch = new List<Stock>();

        foreach (Stock stock in stocks)
        {
            if (stock.branchName == branchName)
            {
                stocksInBranch.Add(stock);
            }
        }

        return stocksInBranch;
    }


    //private void UpdateStockUI(Stock stock)
    //{
    //    string indicator = stock.IsGoingUp() ? "▲" : "▼";
    //    string stockInfo = $"{stock.stockName} ({stock.tickerAbbreviation})\n Branch: {stock.branchName}\n Prev: {stock.previousPrice}\nCurr: {indicator} <color={(stock.IsGoingUp() ? "green" : "red")}>{stock.currentPrice}</color>";

    //    if (stock.stockName == "ABC Company")
    //    {
    //        stock1.text = stockInfo;
    //    } 
    //    else if (stock.stockName == "DEF Company")
    //    {
    //        stock2.text = stockInfo;
    //    }
    //    else if (stock.stockName == "XYZ Corporation")
    //    {
    //        stock3.text = stockInfo;
    //    }
    //}

}
