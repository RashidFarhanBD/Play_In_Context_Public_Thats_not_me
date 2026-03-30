using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Localization/LocalizationSettings")]
public class LocalizationSettings : ScriptableObject
{
    public bool UseLocalization;
    public AppLanguage AppLanguage;
}
