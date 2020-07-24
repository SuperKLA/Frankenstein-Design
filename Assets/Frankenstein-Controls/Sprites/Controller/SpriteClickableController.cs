using System.Collections.Generic;
using System.Threading.Tasks;
using Frankenstein;
using Frankenstein.Controls.Entities;
using Frankenstein.Controls.Views;
using UnityEngine;

namespace Frankenstein.Controls.Controller
{
    public class SpriteClickableController : APIController<ISpriteClickable>, ISpriteClickableService
    {
        private IList<ISpriteClickableView> _view;


        #region IAPIDataController

        protected override void OnEntityCreated(ISpriteClickable entity)
        {
            
        }

        public override  void CreateView()
        {
            var clickables = this.Owner.SpriteContainer;
            this._view = new List<ISpriteClickableView>();

            if (clickables == null)
                return;

            for (int c = 0; c < clickables.Count; c++)
            {
                var view = this._BindClick(clickables[c]);
                this._view.Add(view);
            }
        }

        SpriteClickableView _BindClick(GameObject attachOn)
        {
            var view = attachOn.GetComponent<SpriteClickableView>();

            if (view == null)
                view = attachOn.AddComponent<SpriteClickableView>();

            attachOn.layer = FrankensteinConstants.ClickableLayer;

            var boxCollider = attachOn.GetComponent<Collider2D>();
            if (boxCollider == null || boxCollider.Equals(null))
            {
                boxCollider = attachOn.AddComponent<BoxCollider2D>();
            }

            boxCollider.isTrigger = true;

            view.Setup(this);

            return view;
        }

        #endregion


        #region IClickableService

        ClickBubble IClickableService.OnClicked(int instanceID)
        {
            var position = this.Owner.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            var data     = new ClickData();

            data.Position             = position;
            data.Screen               = Input.mousePosition;
            data.GameObjectInstanceID = instanceID;

            return this.Owner.OnClicked(data);
        }

        void IClickableService.SwitchOnOff(bool onOff)
        {
            for (int c = 0; c < this._view.Count; c++)
            {
                this._view[c].SetOnOff(onOff);
            }
        }

        #endregion


        #region ISpriteClickableService

        void ISpriteClickableService.Rebind()
        {
            this.CreateView();
        }

        #endregion
    }
}