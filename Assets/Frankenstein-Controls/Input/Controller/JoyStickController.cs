using System.Threading.Tasks;
using Frankenstein.Controls.Components;
using Frankenstein.Controls.Entities;
using Frankenstein.Controls.Views;
using Unity.Entities;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Frankenstein.Controls.Controller
{
    public class JoyStickController : APIController<IJoyStick>, IJoyStickService
    {
        private JoyStickView _view;
        private bool _needsViewKill;
        
        
        #region IAPIDataController
        
        protected override void OnEntityCreated(IJoyStick entity)
        {
            
        }

        public override async Task CreateView()
        {
            JoyStickView view;
            
            if (this.Owner.Source != null)
            {
                view = this.Owner.Source.GetComponentInChildren<JoyStickView>(true);
                this._needsViewKill = false;
            }
            else
            {
                var viewGo = await Addressables.InstantiateAsync("JoyStickView");
                view = viewGo.GetComponent<JoyStickView>();
                this._needsViewKill = true;
            }

            view.Setup(this);

            this._view = view;
        }

        protected override async Task OnEntityDestroy(IJoyStick entity)
        {
            await base.OnEntityDestroy(entity);
            if (this._needsViewKill)
            {
                Object.Destroy(this._view);
            }

            this._view = null;
        }

        #endregion


        #region IJoyStickQuery

        void IJoyStickQuery.AddToEntity(Entity entity)
        {
            World.DefaultGameObjectInjectionWorld.EntityManager.AddComponentData(entity, new JoyStickData()
            {
                
            });
            
            World.DefaultGameObjectInjectionWorld.EntityManager.AddComponentObject(entity, this._view);
        }

        #endregion
    }
}