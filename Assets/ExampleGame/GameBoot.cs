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
            container.Register<IGameArenaService>(() => new GameArenaController());
            container.Register<IGameArenaGUIService>(() => new GameArenaGUIController());
            
            container.Register<ICharacterFigureService>(() => new CharacterFigureController());
            container.Register<ICharacterFigureMovementService>(() => new CharacterFigureMovementController());
            
            container.Register<IGameDataService>(() => new GameDataController()).AsSingleton();
        }
    }
}