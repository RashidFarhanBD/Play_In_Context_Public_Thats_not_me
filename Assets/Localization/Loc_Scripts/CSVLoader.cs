using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVLoader
{
    private TextAsset csvFile;

    public void LoadCSV()
    {
        csvFile = Resources.Load<TextAsset>("Localization");
    }

    public Dictionary<string, string> GetDictionaryValues(string attributeID)
    {
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        if (csvFile == null)
        {
            Debug.LogError("CSV file not loaded!");
            return dictionary;
        }

        string[] lines = csvFile.text.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.None);

        if (lines.Length <= 1)
        {
            Debug.LogError("CSV file is empty or invalid!");
            return dictionary;
        }

        Regex csvParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        // ✅ Parse header using SAME parser
        string[] headers = csvParser.Split(lines[0]);

        int attributeIndex = -1;

        for (int i = 0; i < headers.Length; i++)
        {
            string header = CleanField(headers[i]);

            if (header == attributeID) // exact match
            {
                attributeIndex = i;
                break;
            }
        }

        if (attributeIndex == -1)
        {
            Debug.LogError($"Column '{attributeID}' not found in CSV!");
            return dictionary;
        }

        // ✅ Start from 1 (skip header)
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] fields = csvParser.Split(lines[i]);

            for (int f = 0; f < fields.Length; f++)
            {
                fields[f] = CleanField(fields[f]);
            }

            if (fields.Length <= attributeIndex) continue;

            string key = fields[0];
            string value = fields[attributeIndex];

            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
        }

        return dictionary;
    }

    private string CleanField(string field)
    {
        return field.Trim().Trim('"');
    }
}