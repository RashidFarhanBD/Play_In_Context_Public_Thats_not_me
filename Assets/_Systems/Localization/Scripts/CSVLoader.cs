using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    [SerializeField] private TextAsset _localizationFile;

    private Dictionary<string, Dictionary<string, string>> _data = new();


    void Awake()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        string[] lines = _localizationFile.text.Split('\n');

        // Get headers (id, en, nl)
        string[] headers = lines[0].Trim().Split(';');

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Trim().Split(';');

            string id = values[0];

            _data[id] = new Dictionary<string, string>();

            for (int j = 1; j < headers.Length; j++)
            {
                _data[id][headers[j]] = values[j];
            }
        }
    }

    public string GetText(string id, AppLanguage language)
    {
        var parsedLanguageCode = language.ToString().ToLower();
        if (_data.ContainsKey(id) && _data[id].ContainsKey(parsedLanguageCode))
        {
            return _data[id][parsedLanguageCode];
        }

        Debug.LogWarning($"Missing translation for ID: {id}, Lang: {language}");
        return string.Empty;
    }

    void DebugLogDictionary()
    {
        foreach (var idPair in _data)
        {
            string id = idPair.Key;
            var translations = idPair.Value;

            string log = $"ID: {id} -> ";

            foreach (var langPair in translations)
            {
                log += $"{langPair.Key}: {langPair.Value}; ";
            }

            Debug.Log(log);
        }
    }
}