using System;
using UnityEngine;

namespace Frankenstein
{
    public class APISetup : MonoBehaviour
    {
        protected APIContext context;
        public SyncAddressablesData SyncData;

        protected void Setup()
        {
            this.context = new APIContext();
            this.SyncData.Setup();
        }
        
        private void OnDestroy()
        {
            this.context.Destroy();
        }
    }
}