using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Frankenstein.API.EditorExtensions.Utils
{
    public static class AssetCreator
    {
        public static Object CreateAsset(Type t, string path = "")
        {
            Object asset = ScriptableObject.CreateInstance(t);
            return CreateAsset<Object>(asset, path);
        }
        public static T CreateAsset<T>(string path = "") where T : ScriptableObject
        {
            var asset = ScriptableObject.CreateInstance<T>();
            return CreateAsset<T>(asset, path);
        }

        public static T CreateAsset<T>(T asset, string path = "") where T : Object
        {
            if(path.Length == 0)
                path = AssetDatabase.GetAssetPath(Selection.activeObject)+"/"+asset.name+".asset";

            var dirPath = Path.GetDirectoryName(path);
            if (!AssetDatabase.IsValidFolder(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            var oldAsset = AssetDatabase.LoadAssetAtPath(path, typeof(T)) as T;
            if (oldAsset != null)
            {
                return oldAsset;
            }

            var assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path);
            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.Refresh();
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            return AssetDatabase.LoadAssetAtPath<T>(assetPathAndName);
        }

        public static T UpdateAsset<T>(T oldItem, T newItem) where T : ScriptableObject
        {
            string path = AssetDatabase.GetAssetPath(oldItem);

            T asset = AssetDatabase.LoadAssetAtPath(path, typeof(T)) as T;

            asset = newItem;
            asset.name = newItem.name;

            AssetDatabase.Refresh();

            EditorUtility.SetDirty(asset);

            return asset;
        }

        public static string GetAssetPath(Object obj)
        {
            return AssetDatabase.GetAssetPath(obj);
        }

        public static Object GetAsset(string path)
        {
            return AssetDatabase.LoadAssetAtPath(path, typeof(Object));
        }
        
        /// <summary>
        /// Transforms AssetPath to FolderPath
        /// </summary>
        /// <param name="assetPath">asset path with *.asset at the end</param>
        /// <returns>returns local folder path</returns>
        /// <see cref="SettlersOfSpace.Tests.EditorExtensions.Utils.AssetCreatorTests"/>
        public static string AssetPathToFolderPath(string assetPath)
        {
            var splits = assetPath.Split('/');
            var last = splits[splits.Length - 1];

            if (last.Contains("."))
            {
                splits[splits.Length - 1] = "";
            }

            return String.Join("/", splits);
        }
    }
}
