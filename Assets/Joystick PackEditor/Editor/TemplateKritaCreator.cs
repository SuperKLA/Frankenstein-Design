using System.IO;
using Prototypes.EditorExtensions.Utils;
using UnityEditor;
using UnityEngine;

namespace Prototypes.EditorExtensions
{
    public static class TemplateKritaCreator
    {
        [MenuItem("Assets/Create/New Krita File")]
        public static void CreateTextureArray()
        {
            var path = Application.dataPath;
            var blendFile = "/Models/Template.kra";
            var fullPath = path + blendFile;

            if (!File.Exists(fullPath))
            {
                Debug.LogError("Krita File not found, creating new File not possible aborting");
                return;
            }

            var targetObjectPath = AssetCreator.GetAssetPath(Selection.activeObject);
            var targetPath = targetObjectPath + "/NewKrita.kra";

            File.Copy(fullPath, targetPath);

            AssetDatabase.Refresh();
            EditorUtility.SetDirty(Selection.activeObject);
        }
    }
}