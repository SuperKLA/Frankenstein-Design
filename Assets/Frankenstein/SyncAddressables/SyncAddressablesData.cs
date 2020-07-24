using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Frankenstein
{
    [Serializable]
    public struct SyncAddressablesItem
    {
        public string Key;
        public Object ObjectData;
    }
    
    [CreateAssetMenu(menuName = "Create SyncAddressablesData")]
    public class SyncAddressablesData : APIScriptable
    {
        public AssetLabelReference LabelReference;
        public SyncAddressablesItem[] Items;
        
        public void Setup()
        {
            SyncAddressables.Init(this);
        }
    }
    
    public static class SyncAddressables
    {
        private static SyncAddressablesData data;
        
        public static void Init(SyncAddressablesData syncAddressablesData)
        {
            data = syncAddressablesData;
        }
        
        public static GameObject Instantiate(string key)
        {
            for (int c = 0; c < data.Items.Length; c++)
            {
                var item = data.Items[c];
                if (item.Key.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    return MonoBehaviour.Instantiate((GameObject) item.ObjectData);
                }
            }
            
            return null;
        }
    }
}