using ExampleGame.Controller;
using ExampleGame.Entities;
using Frankenstein;

namespace ExampleGame
{
    public class GameBoot : IAPIBoot
    {
        public static IAPIBoot Create()
        {
            return new GameBoot();
        }

        void IAPIBoot.Boot(IoCContainer container)
        {
            // That's how entities are linked with controller
            // you could also use different controller per platform
            container.Register<IExampleEntityService>(() => new ExampleEntityController());
            
            container.Register<IGameArenaService>(() => new GameArenaController());
            container.Register<IGameArenaGUIService>(() => new GameArenaGUIController());
            
            container.Register<ICharacterFigureService>(() => new CharacterFigureController());
            container.Register<ICharacterFigureMovementService>(() => new CharacterFigureMovementController());
            
            container.Register<IGameDataService>(() => new GameDataController()).AsSingleton();
        }
    }
}