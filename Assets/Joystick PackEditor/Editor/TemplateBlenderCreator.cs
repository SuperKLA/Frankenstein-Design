using System.IO;
using Prototypes.EditorExtensions.Utils;
using UnityEditor;
using UnityEngine;

namespace Prototypes.EditorExtensions
{
    public static class TemplateBlenderCreator
    {
        [MenuItem("Assets/Create/New Blend File")]
        public static void CreateTextureArray()
        {
            var path = Application.dataPath;
            var blendFile = "/Models/TemplateBlender.blend.disabled";
            var fullPath = path + blendFile;

            if (!File.Exists(fullPath))
            {
                Debug.LogError("Template Blend File not found, creating new File not possible aborting");
                return;
            }

            var targetObjectPath = AssetCreator.GetAssetPath(Selection.activeObject);
            var targetPath = targetObjectPath + "/NewBlenderFile.blend.disabled";

            File.Copy(fullPath, targetPath);

            AssetDatabase.Refresh();
            EditorUtility.SetDirty(Selection.activeObject);
        }
    }
}