using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Penguins/Penguin")]
public class PenguinData : ScriptableObject
{
    public string DisplayName;
    public Sprite PenguinIcon;
    [TextArea] public string VisualDescription;

    public string ID => DisplayName.ToLower().ToLower();
}
