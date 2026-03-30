// Place this file inside any folder named "Editor" in your project.
// It automatically applies to every MonoBehaviour / ScriptableObject that uses [Button].

#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Universal inspector drawer that finds every method tagged with [Button]
/// on the inspected target and renders a clickable button for each one.
///
/// Works with MonoBehaviour and ScriptableObject targets out of the box.
/// </summary>
[CustomEditor(typeof(UnityEngine.Object), true)]   // 'true' = include derived types
[CanEditMultipleObjects]
public class ButtonAttributeEditor : Editor
{
    // Cache reflection results per type to avoid re-scanning every repaint.
    private MethodInfo[] _buttonMethods;

    // -------------------------------------------------------------------------
    // Binding flags: public + non-public instance methods, search entire hierarchy
    // -------------------------------------------------------------------------
    private const BindingFlags Flags =
        BindingFlags.Instance |
        BindingFlags.Public |
        BindingFlags.NonPublic |
        BindingFlags.DeclaredOnly;   // we walk the hierarchy manually so we don't duplicate

    private void OnEnable()
    {
        _buttonMethods = CollectButtonMethods(target.GetType());
    }

    public override void OnInspectorGUI()
    {
        // Draw the default inspector first so normal fields are untouched.
        DrawDefaultInspector();

        if (_buttonMethods == null || _buttonMethods.Length == 0)
            return;

        EditorGUILayout.Space(6);
        EditorGUILayout.LabelField("— Actions —", EditorStyles.centeredGreyMiniLabel);
        EditorGUILayout.Space(2);

        foreach (MethodInfo method in _buttonMethods)
        {
            var attr = method.GetCustomAttribute<ButtonAttribute>();
            string label = string.IsNullOrWhiteSpace(attr.Label) ? ObjectNames.NicifyVariableName(method.Name) : attr.Label;
            string tooltip = attr.Tooltip ?? string.Empty;

            // Disable the button for multi-selection only when it has parameters
            // (calling with unknowns would throw).
            bool hasParams = method.GetParameters().Length > 0;
            bool disable = hasParams && targets.Length > 1;

            using (new EditorGUI.DisabledGroupScope(disable))
            {
                GUIContent content = new GUIContent(label, tooltip);

                if (disable)
                    content.tooltip = "Multi-edit not supported for methods with parameters.";

                if (GUILayout.Button(content, GUILayout.Height(24)))
                {
                    InvokeOnAllTargets(method, hasParams);
                }
            }
        }
    }

    // -------------------------------------------------------------------------
    // Invoke on every selected object so multi-edit works for parameterless methods.
    // -------------------------------------------------------------------------
    private void InvokeOnAllTargets(MethodInfo method, bool hasParams)
    {
        foreach (UnityEngine.Object t in targets)
        {
            Undo.RecordObject(t, $"Button: {method.Name}");

            try
            {
                // Parameterless — just call it.
                if (!hasParams)
                {
                    method.Invoke(t, null);
                }
                else
                {
                    // Build default values for each parameter so the call doesn't crash.
                    ParameterInfo[] parms = method.GetParameters();
                    object[] defaults = parms.Select(p =>
                        p.HasDefaultValue ? p.DefaultValue : GetDefault(p.ParameterType)
                    ).ToArray();

                    method.Invoke(t, defaults);
                }
            }
            catch (TargetInvocationException tie)
            {
                Debug.LogError($"[Button] Exception in '{method.DeclaringType?.Name}.{method.Name}': {tie.InnerException}", t);
            }

            EditorUtility.SetDirty(t);
        }
    }

    // -------------------------------------------------------------------------
    // Walk the full type hierarchy (excluding Unity internals) collecting methods.
    // -------------------------------------------------------------------------
    private static MethodInfo[] CollectButtonMethods(Type type)
    {
        var methods = new System.Collections.Generic.List<MethodInfo>();

        Type current = type;
        while (current != null &&
               current != typeof(MonoBehaviour) &&
               current != typeof(ScriptableObject) &&
               current != typeof(UnityEngine.Object) &&
               current != typeof(object))
        {
            foreach (MethodInfo m in current.GetMethods(Flags))
            {
                if (m.GetCustomAttribute<ButtonAttribute>() != null)
                    methods.Add(m);
            }
            current = current.BaseType;
        }

        return methods.ToArray();
    }

    private static object GetDefault(Type t)
        => t.IsValueType ? Activator.CreateInstance(t) : null;
}
#endif