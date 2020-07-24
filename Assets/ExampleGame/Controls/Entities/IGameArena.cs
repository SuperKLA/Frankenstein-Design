using Frankenstein;
using Frankenstein.Controls.Entities;

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