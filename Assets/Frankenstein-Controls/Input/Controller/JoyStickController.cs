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

        public override  void CreateView()
        {
            JoyStickView view;
            
            if (this.Owner.Source != null)
            {
                view = this.Owner.Source.GetComponentInChildren<JoyStickView>(true);
                this._needsViewKill = false;
            }
            else
            {
                var viewGo =  SyncAddressables.Instantiate("JoyStickView");
                view = viewGo.GetComponent<JoyStickView>();
                this._needsViewKill = true;
            }

            view.Setup(this);

            this._view = view;
        }

        protected override  void OnEntityDestroy(IJoyStick entity)
        {
             base.OnEntityDestroy(entity);
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