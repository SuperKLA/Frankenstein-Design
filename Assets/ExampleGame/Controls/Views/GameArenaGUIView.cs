using ExampleGame.Entities;
using Frankenstein;

namespace ExampleGame.Views
{
    public class GameArenaGUIView : APIViewBehaviour<IGameArenaGUIService>, IGameArenaGUIView
    {
        public void Show()
        {
            this.gameObject.SetActive(true);
        }
    }
}