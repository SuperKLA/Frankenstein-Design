using Frankenstein.Controls.Entities;
using UnityEngine;

namespace Frankenstein.Controls.Views
{
    public class SpriteClickableView : ClickableView, ISpriteClickableView
    {
        public Collider2D OwnCollider;
        
        public override void Setup(IClickableService service)
        {
            this.OwnCollider = gameObject.GetComponent<Collider2D>();
            base.Setup(service);
        }

        public override void SetOnOff(bool onOff)
        {
            this.OwnCollider.enabled = onOff;
        }
    }
}

