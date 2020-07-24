using UnityEngine;

namespace Frankenstein.DTO
{
    public interface IDTOConfig
    {
        
    }
    
    public class BootDTO : APIBootable, IDTOConfig
    {
        public override void Boot(IoCContainer container)
        {
//            container.Register<IDTOConfig>(() => this);
//            
//            container.Register<PlayerSaveGame>(() => PlayerSaveGame.ToDTO()).AsSingleton();
//            container.Register<GameConfig>(() => GameConfig.ToDTO()).AsSingleton();
        }
    }
}