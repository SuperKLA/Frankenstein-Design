using Unity.Entities;
using UnityEngine;

namespace Frankenstein
{
    public abstract class APIComponentSystem<T_Service> : ComponentSystem where T_Service : IAPIEntityService
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