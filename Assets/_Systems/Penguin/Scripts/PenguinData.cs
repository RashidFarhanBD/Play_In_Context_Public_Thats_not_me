using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Penguins/Penguin")]
public class PenguinData : ScriptableObject
{
    public int FollowerCount;
    public string DisplayName;
    public string Username;
    [TextArea]
    public string Status;
    [TextArea] public string VisualDescription;
    public Sprite PenguinIcon;

    public string ID => Username.ToIdentifier();
}
