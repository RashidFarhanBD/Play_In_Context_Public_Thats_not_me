using UnityEngine;

public class LocalizationManager : Singleton<LocalizationManager>
{
    [SerializeField] private CSVReader _csvReader;
    [SerializeField] private LocalizationSettings _localizationSettings;

    public LocalizationSettings LocalizationSettings => _localizationSettings;

    public bool TryGetText(string id, out string text)
    {
        if (_localizationSettings.UseLocalization)
        {
            text = _csvReader.GetText(id, _localizationSettings.AppLanguage);
        }
        else
        {
            text = id;
        }

        if (text == string.Empty)
        {
            Debug.LogError($"Error with ID {id}");
        }

        return text != string.Empty;
    }
}
