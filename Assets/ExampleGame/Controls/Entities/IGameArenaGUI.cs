using ExampleGame.DTO;
using Frankenstein;
using Frankenstein.Controls.Entities;
using Frankenstein.DTO;

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