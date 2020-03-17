using UnityEngine;

namespace Frankenstein.DTO
{
    public class DTOConfig<T_DTO_Out> : MonoBehaviour
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
