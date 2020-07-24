using System;
using System.Threading.Tasks;
using Frankenstein;
using Frankenstein.Controls.Entities;
using Frankenstein.Controls.Views;
using UnityEngine;

namespace Frankenstein.Controls.Controller
{
    internal class CoroutineController : APIController<ICoroutine>, ICoroutineService
    {
        private CoroutineView _view;
        private ICoroutineService ICoroutineService => this;
        
        protected override void OnEntityCreated(ICoroutine entity)
        {
            
        }

        public override  void CreateView()
        {
            var go   = this.Owner.AnyGameObject;
            var view = go.GetComponent<CoroutineView>();

            if (view == null || view.Equals(null))
            {
                view = go.AddComponent<CoroutineView>();
            }
            
            view.Setup(this);
            _view = view;
        }

        protected override  void OnEntityDestroy(ICoroutine entity)
        {
             base.OnEntityDestroy(entity);
            this.ICoroutineService.ClearAllJobs();
            MonoBehaviour.Destroy(this._view.gameObject);
            this._view = null;
        }

        #region ICoroutineService

        public float TickTime => this.Owner.TickTime;
        
        public void RegisterJob(float t, Func<float, bool> callback, Action completed)
        {
            this._view.RegisterJob(t, callback, completed);
        }

        void ICoroutineService.RegisterJob(Func<float, bool> callback, Action completed)
        {
            this._view.RegisterJob(float.MaxValue, callback, completed);
        }
        
        void ICoroutineService.ClearAllJobs()
        {
            this._view.ClearAllJobs();
        }

        #endregion
    }
}
