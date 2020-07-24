using Frankenstein;
using Frankenstein.Controls.Entities;

namespace ExampleGame.Entities
{
    public interface IGameArenaGUI : IAPIEntity<IGameArenaGUIService, IGameArenaGUIView>
    {
        ISceneService SceneService { get; }
    }

    public interface IGameArenaGUIService : IAPIEntityService
    {
        void ShowWinScreen();
    }

    public interface IGameArenaGUIView : IAPIEntityView
    {
        
    }
}