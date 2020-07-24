using System.Threading.Tasks;
using ExampleGame.Entities;
using ExampleGame.Views;
using Frankenstein;
using UnityEngine;

namespace ExampleGame.Controller
{
    public class CharacterFigureMovementController : APIController<ICharacterFigureMovement>, ICharacterFigureMovementService
    {
        private MovementInputView _view;

        #region Controller

        protected override void OnEntityCreated(ICharacterFigureMovement entity)
        {
        }

        public override void CreateView()
        {
            var go   = new GameObject("MovementInputView");
            var view = go.AddComponent<MovementInputView>();

            view.Setup(this);
            this._view = view;
        }

        protected override void OnEntityDestroy(ICharacterFigureMovement entity)
        {
            base.OnEntityDestroy(entity);
        }

        #endregion


        #region ICharacterFigureMovementService

        void ICharacterFigureMovementService.Move(Vector3 dir)
        {
            var speed = this.Owner.GameDataService.GameConfig.Character.MovementSpeed;

            this.Owner.FigureService.Move(dir * speed);
        }

        #endregion
    }
}