using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsFlashImporter : MonoBehaviour
{
    public TextAsset csvFile; // Reference to the CSV file as a TextAsset
    public List<NewsFlash> newsFlashes = new List<NewsFlash>();

    // Start is called before the first frame update
    void Start()
    {
        ImportNewsFlashes();
    }

    public void ImportNewsFlashes()
    {
        if (csvFile != null)
        {
            string[] lines = csvFile.text.Split('\n');

            for (int i = 1; i < lines.Length; i++) // Start from index 1 to skip the header line
            {
                string[] values = lines[i].Split(';'); // Split using semicolons (;)

                if (values.Length >= 7)
                {
                    int id = int.Parse(values[0]);
                    string subject = values[1];
                    string text = values[2];
                    string negativeEffectText = values[3];
                    string positiveEffectText = values[4];
                    bool isPositive = bool.Parse(values[5]);
                    int effectOnStockPrice = int.Parse(values[6]);
                    string branchName = values[7];

                    NewsFlash newsFlash = new NewsFlash(id, subject, text, negativeEffectText, positiveEffectText, isPositive, effectOnStockPrice, branchName);
                    newsFlashes.Add(newsFlash);
                }
                else
                {
                    Debug.LogWarning("Invalid data in CSV line: " + lines[i]);
                }
            }
        }
        else
        {
            Debug.LogError("No TextAsset assigned for CSV file.");
        }
    }

    public List<NewsFlash> GetNewsFlashes()
    {
        return newsFlashes;
    }
}
