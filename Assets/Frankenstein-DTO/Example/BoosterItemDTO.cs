using UnityEngine;

namespace Frankenstein.DTO
{
    public interface IBoosterItem
    {
        
    }
    
    public class BoosterItem : IBoosterItem
    {
        
    }
    
    public class BoosterItemDTO : DTOConfig<BoosterItem>, IBoosterItem
    {
        public override BoosterItem ToDTO()
        {
            var result = new BoosterItem();

            return result;
        }

        [ContextMenu("Setup")]
        public override void Setup()
        {
            
        }
    }
}