using System.Threading.Tasks;
using ExampleGame.Entities;
using ExampleGame.Views;
using Frankenstein;
using Frankenstein.Controls.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ExampleGame.Controller
{
    public class CharacterFigureController : APIController<ICharacterFigure>, ICharacterFigureService
    {
        private CharacterFigureView _view;

        #region Controller

        protected override void OnEntityCreated(ICharacterFigure entity)
        {
        }

        public override  void CreateView()
        {
            var asset =  SyncAddressables.Instantiate("ExampleCharacter1");
            var view  = asset.GetComponent<CharacterFigureView>();
            view.Setup(this);

            this._view      = view;
            this.Owner.View = view;
        }

        #endregion


        #region ICharacterFigureService

        void ICharacterFigureService.Move(Vector3 planeDir)
        {
            this._view.Move(planeDir);
        }

        #endregion
    }
}