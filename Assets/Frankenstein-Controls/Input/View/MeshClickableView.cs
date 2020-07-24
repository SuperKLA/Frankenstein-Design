using Frankenstein.Controls.Entities;
using UnityEngine;

namespace Frankenstein.Controls.Views
{
    public class MeshClickableView : ClickableView, IMeshClickableView
    {
        public Collider OwnCollider;
        
        public override void Setup(IClickableService service)
        {
            this.OwnCollider = gameObject.GetComponent<Collider>();
            base.Setup(service);
        }

        public override void SetOnOff(bool onOff)
        {
            this.OwnCollider.enabled = onOff;
        }
    }
}

