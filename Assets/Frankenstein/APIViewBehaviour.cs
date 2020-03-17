using UnityEngine;

namespace Frankenstein
{
    public class APIViewBehaviour<T_Service> : MonoBehaviour where T_Service : IAPIEntityService
    {
        protected T_Service Service { get; set; }
        
        public virtual void Setup(T_Service service)
        {
            this.Service = service;
        }

        public virtual void PreDestroy()
        {
            
        }
    }
}