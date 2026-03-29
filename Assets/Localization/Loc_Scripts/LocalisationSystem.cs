using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocalizationSystem : MonoBehaviour
{
    public enum Language
    {
        English,
        French,
        Japanese,
        Dutch
    }

    public static Language language = Language.English;

    public static Dictionary<string, string> localisedEN;
    public static Dictionary<string, string> localisednl;
    public static bool isInit;
    public TextMeshProUGUI txt;
    public static void Init()
    {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        localisedEN = csvLoader.GetDictionaryValues("en");
        localisednl = csvLoader.GetDictionaryValues("nl");

        foreach (var v in localisedEN)
        {
            Debug.Log(v.Key);
        }
        isInit = true;
    }

    public static string GetLocalisedValue(string key)
    {
        if (!isInit) { Init(); }

        string value = key;

        switch (language)
        {
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;
            case Language.Dutch:
                localisednl.TryGetValue(key, out value);
                break;
        }

        return value;
    }
    public void  OnPressEnglish()
    {
        language = Language.English;
        txt.text = GetLocalisedValue("danny_1");
    }

    public void OnPressNL()
    {
        language = Language.Dutch;
        txt.text = GetLocalisedValue("danny_1");

    }
}