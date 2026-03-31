using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueSystem/Node")]
public class DialogueNode : ScriptableObject
{

    public PenguinData PenguinData;
    [TextArea]
    public string Text;

    public List<DialogueChoice> Choices;

    public DialogueNode NextNode;

    [Button("Jiggle")]
    public void QuickId()
    {
#if UNITY_EDITOR
        Text = name.ToIdentifier();
        EditorUtility.SetDirty(this);
#endif
    }
}
