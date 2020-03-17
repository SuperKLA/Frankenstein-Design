using ExampleGame.Entities;
using Frankenstein;

namespace ExampleGame.Views
{
    public class GameArenaView : APIViewBehaviour<IGameArenaService>, IGameArenaView
    {
        public GameArenaWinTrigger WinTrigger;

        public override void Setup(IGameArenaService service)
        {
            this.WinTrigger.Setup(service);
            base.Setup(service);
        }
    }
}