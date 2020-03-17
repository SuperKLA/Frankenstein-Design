using ExampleGame.DTO;
using Frankenstein;
using Frankenstein.Controls.Entities;
using Frankenstein.DTO;

namespace ExampleGame.Entities
{
    public interface IGameArena : IAPIEntity<IGameArenaService, IGameArenaView>
    {
        ISceneService SceneService { get; }
        void OnWin();
    }

    public interface IGameArenaService : IAPIEntityService
    {
        void OnWinTriggerActivate();
    }

    public interface IGameArenaView : IAPIEntityView
    {
        
    }
}