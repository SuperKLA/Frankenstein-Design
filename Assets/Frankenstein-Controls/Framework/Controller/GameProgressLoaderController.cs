using System;
using System.Threading.Tasks;
using Frankenstein;
using Frankenstein.Controls.Entities;

namespace Frankenstein.Controls.Controller
{
    internal class GameProgressLoaderController : APIController<IGameProgressLoader>, IGameProgressLoaderService
    {
        private event Action _onLoadCompleted;
        private event Action _onUserLaunch;
        private event Action _onLoadStarted;
        private event Action _onLoginSuccess;
        private event Action _onEnterLoadingScreen;
        private event Action _onLeavingLoadingScreen;

        protected override void OnEntityCreated(IGameProgressLoader entity)
        {
            this._Bind(entity);
        }

        protected override  void OnEntityDestroy(IGameProgressLoader entity)
        {
            this._Unbind(entity);
        }

        void _Bind(IGameProgressLoader entity)
        {
            this._onLoadCompleted        += entity.OnLoadCompleted;
            this._onUserLaunch           += entity.OnUserLaunch;
            this._onLoadStarted          += entity.OnLoadStarted;
            this._onLoginSuccess         += entity.OnLoginSuccess;
            this._onEnterLoadingScreen   += entity.OnEnterLoadingScreen;
            this._onLeavingLoadingScreen += entity.OnLeavingLoadingScreen;
        }

        void _Unbind(IGameProgressLoader entity)
        {
            this._onLoadCompleted        -= entity.OnLoadCompleted;
            this._onUserLaunch           -= entity.OnUserLaunch;
            this._onLoadStarted          -= entity.OnLoadStarted;
            this._onLoginSuccess         -= entity.OnLoginSuccess;
            this._onEnterLoadingScreen   -= entity.OnEnterLoadingScreen;
            this._onLeavingLoadingScreen -= entity.OnLeavingLoadingScreen;
        }


        #region IGameProgressLoaderService

        public bool IsGameLaunched { get; set; }

        void IGameProgressLoaderService.TriggerLoadComplete()
        {
            if (this._onLoadCompleted != null)
            {
                this._onLoadCompleted();
            }
        }

        void IGameProgressLoaderService.TriggerUserLaunch()
        {
            this.IsGameLaunched = true;
            if (this._onUserLaunch != null)
            {
                this._onUserLaunch();
            }
        }

        void IGameProgressLoaderService.TriggerLoadStarted()
        {
            if (this._onLoadStarted != null)
            {
                this._onLoadStarted();
            }
        }

        void IGameProgressLoaderService.TriggerLoginSuccess()
        {
            if (this._onLoginSuccess != null)
            {
                this._onLoginSuccess();
            }
        }

        void IGameProgressLoaderService.TriggerEnterLoadingScreen()
        {
            if (this._onEnterLoadingScreen != null)
            {
                this._onEnterLoadingScreen();
            }
        }

        void IGameProgressLoaderService.TriggerLeavingLoadingScreen()
        {
            if (this._onLeavingLoadingScreen != null)
            {
                this._onLeavingLoadingScreen();
            }
        }

        void IGameProgressLoaderService.ReportProgressLoading(float t)
        {
            this.Owner.OnReportProgressLoading(t);
        }

        #endregion
    }
}