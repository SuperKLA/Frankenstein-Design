using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Custom inspector for Object including derived classes.
/// </summary>
[CanEditMultipleObjects]
[CustomEditor(typeof(UnityEngine.Object), true)]
public class ObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        this.DrawEasyButtons();
    }
    
    public void DrawEasyButtons()
    {
        GUILayout.Space(10);
        // Loop through all methods with no parameters
        var methods = this.target.GetType()
                            .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                            .Where(m => m.GetParameters().Length == 0);
        foreach (var method in methods)
        {
            // Get the ButtonAttribute on the method (if any)
            var ba = (EasyButtonAttribute)Attribute.GetCustomAttribute(method, typeof(EasyButtonAttribute));

            if (ba != null)
            {
                // Determine whether the button should be enabled based on its mode
                var wasEnabled = GUI.enabled;
                GUI.enabled = ba.Mode == ButtonMode.AlwaysEnabled
                              || (EditorApplication.isPlaying ? ba.Mode == ButtonMode.EnabledInPlayMode : ba.Mode == ButtonMode.DisabledInPlayMode);


                if (((int)ba.Spacing & (int)ButtonSpacing.Before) != 0) GUILayout.Space(10);
                    
                // Draw a button which invokes the method
                var buttonName = String.IsNullOrEmpty(ba.Name) ? ObjectNames.NicifyVariableName(method.Name) : ba.Name;
                if (GUILayout.Button(buttonName))
                {
                    foreach (var t in this.targets)
                    {
                        method.Invoke(t, null);
                    }
                }

                if (((int)ba.Spacing & (int)ButtonSpacing.After) != 0) GUILayout.Space(10);
                    
                GUI.enabled = wasEnabled;
            }
        }
    }
}