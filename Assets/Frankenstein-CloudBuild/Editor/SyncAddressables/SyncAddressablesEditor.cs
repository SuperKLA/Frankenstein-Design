using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.PlayerLoop;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Frankenstein
{
    [InitializeOnLoad]
    public static class SyncAddressablesEditor
    {
        private static TaskAwaiter<IList<IResourceLocation>>? waiter;
        
        static SyncAddressablesEditor ()
        {
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            if (EditorApplication.isPlaying) return;
            
            var databaseName = AssetDatabase.FindAssets("t:SyncAddressablesData");
            if (databaseName == null || databaseName.Length == 0) return;

            var path      = AssetDatabase.GUIDToAssetPath(databaseName[0]);
            var database  = AssetDatabase.LoadAssetAtPath<SyncAddressablesData>(path);
            var settings  = AddressableAssetSettingsDefaultObject.Settings;
            var assetList = new List<AddressableAssetEntry>();
                
            settings.GetAllAssets(assetList, true);
            database.Items = new SyncAddressablesItem[assetList.Count];

            for (int c = 0; c < assetList.Count; c++)
            {
                var asset = assetList[c];
                database.Items[c] = new SyncAddressablesItem()
                {
                    Key = asset.address,
                    ObjectData = asset.MainAsset
                };
            }
        }
    }
}