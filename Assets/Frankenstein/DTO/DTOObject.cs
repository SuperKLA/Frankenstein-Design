using UnityEngine;

namespace Frankenstein.DTO
{
    public class DTOObject<T_DTO_Out> : ScriptableObject
    {
        public virtual T_DTO_Out ToDTO()
        {
            return default(T_DTO_Out);
        }
        
        public virtual void Setup()
        {
            
        }
    }
}
