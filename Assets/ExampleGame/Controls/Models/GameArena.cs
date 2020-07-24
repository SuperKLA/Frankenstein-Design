using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleGame.Entities;
using Frankenstein;
using Frankenstein.Controls.Entities;

namespace ExampleGame
{
    public class GameArena : APIModel, IQueryable, IScene, IGameArena, IGameArenaGUI
    {
        #region Interface Accessors

        private IQueryable    IQueryable    => this;
        private IScene        IScene        => this;
        private IGameArena    IGameArena    => this;
        private IGameArenaGUI IGameArenaGUI => this;

        #endregion


        #region Locals

        public ISceneService SceneService => this.IScene.Service;

        #endregion


        #region APIModel

        public override void Boot(params object[] any)
        {
            this.IQueryable.Service = this.SetupServices<IQueryableService>();
            this.IScene.Service     = this.SetupServices<ISceneService>();

            this.IScene.Service.LoadScene("ExampleGame1", () =>
            {
                this.IScene.Service.SetAsMain();

                this.IGameArena.Service    = this.SetupServices<IGameArenaService>();
                this.IGameArenaGUI.Service = this.SetupServices<IGameArenaGUIService>();

                new Character().Boot();
            });
        }

        public override void Destroy()
        {
            this.DestroyServices(this.IQueryable.Service);
            this.DestroyServices(this.IGameArena.Service);
            this.DestroyServices(this.IGameArenaGUI.Service);
            this.DestroyServices(this.IScene.Service);
            base.Destroy();
        }

        #endregion


        #region IQueryable

        IQueryableService IAPIEntity<IQueryableService>.Service { get; set; }

        List<Guid> IQueryable.Layers => new List<Guid>() { };

        bool IQueryable.Matches<TQuery>()
        {
            return typeof(TQuery).IsInstanceOfType(this);
        }

        TService IQueryable.Provide<TService>()
        {
//            if (typeof(TService) == typeof(ICrewPoolQueryResult))
//                return (TService) this.ICrewPool.Service;
//            else
            return default(TService);
        }

        #endregion


        #region IScene

        ISceneService IAPIEntity<ISceneService>.Service { get; set; }

        #endregion


        #region IGameArena

        IGameArenaService IAPIEntity<IGameArenaService, IGameArenaView>.Service { get; set; }

        IGameArenaView IAPIEntity<IGameArenaService, IGameArenaView>.View { get; set; }

        void IGameArena.OnWin()
        {
            this.IGameArenaGUI.Service.ShowWinScreen();
        }

        #endregion


        #region IGameArenaGUI

        IGameArenaGUIService IAPIEntity<IGameArenaGUIService, IGameArenaGUIView>.Service { get; set; }

        IGameArenaGUIView IAPIEntity<IGameArenaGUIService, IGameArenaGUIView>.View { get; set; }

        #endregion
    }
}