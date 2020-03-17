using System.Threading.Tasks;
using ExampleGame.Entities;
using ExampleGame.Views;
using Frankenstein;

namespace ExampleGame.Controller
{
    public class GameArenaGUIController : APIController<IGameArenaGUI>, IGameArenaGUIService
    {
        private IGameArenaGUIService IGameArenaGUIService => this;
        private GameArenaGUIView _view;

        #region Controller

        protected override void OnEntityCreated(IGameArenaGUI entity)
        {
        }

        public override async Task CreateView()
        {
            await base.CreateView();
            var view = this.Owner.SceneService.GetView<GameArenaGUIView>();
            view.Setup(this);

            this._view      = view;
            this.Owner.View = view;
        }

        #endregion


        #region IGameArenaGUIService

        void IGameArenaGUIService.ShowWinScreen()
        {
            this._view.Show();
        }

        #endregion
    }
}