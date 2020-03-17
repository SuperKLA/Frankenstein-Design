using System;
using UnityEngine;

namespace Frankenstein
{
    public class APISetup : MonoBehaviour
    {
        protected APIContext context;

        protected void Setup()
        {
            this.context = new APIContext();
        }
        
        private void OnDestroy()
        {
            this.context.Destroy();
        }
    }
}