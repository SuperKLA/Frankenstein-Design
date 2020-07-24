using DigitalRubyShared;
using Frankenstein;
using Frankenstein.Controls.Entities;
using Frankenstein.Controls.Views;
using UnityEngine;

namespace Frankenstein.Controls.Controller
{
    public class TouchRay2DController : APIController<ITouch2DRay>, ITouch2DRayService
    {
        private TapGestureRecognizer tapGesture;
        private ITouch2DRayService ITouchRay2DService => this;

        protected override void OnEntityCreated(ITouch2DRay entity)
        {
            
        }

        #region IAPIDataController
        
        public void Dispose()
        {
        }

        private void OnTapGesture(Vector2 point)
        {
            this._OrthoRay(point);
        }

        void _OrthoRay(Vector2 point)
        {
            var ray = this.Owner.CameraService.ScreenToRay(point);
            var hits = Physics2D.RaycastAll(ray.origin, ray.direction, FrankensteinConstants.Constants.TouchMaxRay,
                                            FrankensteinConstants.ClickableLayer);

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5, false);

            for (int c = 0; c < hits.Length; c++)
            {
                var hit = hits[c];
                if (hit.collider == null)
                    return;

                var hitView = hit.collider.GetComponent<ClickableView>();

                if (hitView == null)
                    return;

                var bubbleState = hitView.OnClickIncoming();
                if(bubbleState == ClickBubble.abort)
                    break;
            }
        }
        
        #endregion


        #region ITouchRay2DService

        void ITouch2DRayService.Unbind()
        {
            this.Owner.InputService.OnTapGesture -= this.OnTapGesture;
        }

        void ITouch2DRayService.Bind()
        {
            this.Owner.InputService.OnTapGesture += this.OnTapGesture;
        }

        #endregion
    }
}