using System.Threading.Tasks;
using ExampleGame.Entities;
using ExampleGame.Views;
using Frankenstein;

namespace ExampleGame.Controller
{
    public class GameArenaController : APIController<IGameArena>, IGameArenaService
    {
        private IGameArenaService IGameArenaService => this;
        private GameArenaView     _view;

        #region Controller

        protected override void OnEntityCreated(IGameArena entity)
        {
        }

        public override  void CreateView()
        {
             base.CreateView();
            var view = this.Owner.SceneService.GetView<GameArenaView>();
            view.Setup(this);

            this._view      = view;
            this.Owner.View = view;
        }

        #endregion


        #region IGameArenaService

        void IGameArenaService.OnWinTriggerActivate()
        {
            this.Owner.OnWin();
        }

        #endregion
    }
}