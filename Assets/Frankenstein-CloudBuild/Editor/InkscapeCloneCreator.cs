using System.IO;
using Frankenstein.API.EditorExtensions.Utils;
using UnityEngine;
using UnityEditor;

namespace Frankenstein.API.EditorExtensions
{
    public static class InkscapeCloneCreator
    {
        [MenuItem("Assets/Create/New Inkscape File")]
        public static void CreateTextureArray()
        {
            var path = Application.dataPath;
            var svgFile = "/Models/CloneInkscapeTemplate.svg";
            var fullPath = path + svgFile;

            if (!File.Exists(fullPath))
            {
                Debug.LogError("Template Inkscape File not found, creating new File not possible aborting, looking for /Models/CloneInkscapeTemplate.svg");
                return;
            }

            var targetObjectPath = AssetCreator.GetAssetPath(Selection.activeObject);
            var targetPath = targetObjectPath + "/InkscapeTemplate.svg";

            File.Copy(fullPath, targetPath);

            AssetDatabase.Refresh();
            EditorUtility.SetDirty(Selection.activeObject);
        }
    }
}