using System.Threading.Tasks;
using ExampleGame.Entities;
using ExampleGame.Views;
using Frankenstein;
using Frankenstein.Controls.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ExampleGame.Controller
{
    /*
     * There is only one controller per Entity
     */
    public class ExampleEntityController : APIController<IExampleEntity>, IExampleEntityService
    {
        private ExampleEntityView _view;

        #region Controller

        protected override void OnEntityCreated(IExampleEntity entity)
        {
            //A controller has different States, use them to boot properly
        }

        public override async Task CreateView()
        {
            //optional, demonstration with addressables
            var asset = await Addressables.InstantiateAsync("Example").Task;
            var view  = asset.GetComponent<ExampleEntityView>();
            view.Setup(this);

            this._view      = view;
            this.Owner.View = view;
        }

        #endregion


        #region IExampleEntityService

        float IExampleEntityService.Sum()
        {
            return this.Owner.A + this.Owner.B;
        }

        #endregion
    }
}