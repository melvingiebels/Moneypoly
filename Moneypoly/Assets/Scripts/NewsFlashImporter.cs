using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NewsFlashImporter : MonoBehaviour
{
    public string filePath = "Assets/NewsFlashes.cvs"; // Path to your CSV file
    public List<NewsFlash> newsFlashes = new();

    // Start is called before the first frame update
    void Start()
    {
        ImportNewsFlashes();
    }

    public void ImportNewsFlashes()
    {
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string headerLine = reader.ReadLine(); // Skip the header line

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    if (values.Length >= 7)
                    {
                        int id = int.Parse(values[0]);
                        string subject = values[1];
                        string text = values[2];
                        string negativeEffectText = values[3];
                        string positiveEffectText = values[4];
                        bool isPositive = bool.Parse(values[5]);
                        int effectOnStockPrice = int.Parse(values[6]);

                        NewsFlash newsFlash = new NewsFlash(id, subject, text, negativeEffectText, positiveEffectText, isPositive, effectOnStockPrice);
                        newsFlashes.Add(newsFlash);
                    }
                    else
                    {
                        Debug.LogWarning("Invalid data in CSV line: " + line);
                    }
                }
            }
        }
        else
        {
            Debug.LogError("CSV file not found at path: " + filePath);
        }
    }

    public List<NewsFlash> GetNewsFlashes()
    {
        return newsFlashes;
    }
}
