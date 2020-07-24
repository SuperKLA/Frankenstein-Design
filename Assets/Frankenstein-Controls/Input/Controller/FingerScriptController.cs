using System.Threading.Tasks;
using Frankenstein;
using Frankenstein.Controls.Entities;
using Frankenstein.Controls.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Frankenstein.Controls.Controller
{
    public class FingerScriptController : APIController<IFingerScript>, IFingerScriptService
    {
        private GameObject _fingerScriptContainer;
        private FingerScriptClient _view;
        
        
        #region APIController

        protected override void OnEntityCreated(IFingerScript entity)
        {
            
        }

        public override  void CreateView()
        {
            var asset =  SyncAddressables.Instantiate("FingerScriptClient");
            var view = asset.GetComponent<FingerScriptClient>();
            view.Setup(this);

            this._view = view;
        }
        
        #endregion
    }
}
