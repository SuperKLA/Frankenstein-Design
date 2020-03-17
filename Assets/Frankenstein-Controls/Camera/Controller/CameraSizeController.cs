using System.Threading.Tasks;
using Frankenstein;
using Frankenstein.Controls.Entities;
using Frankenstein.Controls.Views;
using UnityEngine.AddressableAssets;

namespace Frankenstein.Controls.Controller
{
    internal class CameraSizeController : APIController<ICameraSize>, ICameraSizeService
    {
        private CameraSizeView _view;
        
        protected override void OnEntityCreated(ICameraSize entity)
        {
            
        }

        public override async Task CreateView()
        {
            var asset = await Addressables.InstantiateAsync("CameraSizeView").Task;
            var view = asset.GetComponent<CameraSizeView>();
                
            view.Setup(this);

            this._view = view;
        }

        #region ICameraSizeService

        float ICameraSizeService.GetOrthSize()
        {
            return this._view.GetSize();
        }

        #endregion
    }
}
