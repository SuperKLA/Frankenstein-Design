using System.Threading.Tasks;
using DigitalRubyShared;
using Frankenstein;
using Frankenstein.Controls.Entities;
using Frankenstein.Controls.Views;
using UnityEngine;

namespace Frankenstein.Controls.Controller
{
    public class TouchRay3DController : APIController<ITouch3DRay>, ITouch3DRayService
    {
        private TapGestureRecognizer tapGesture;
        private ITouch3DRayService ITouch3DRayService => this;

        protected override void OnEntityCreated(ITouch3DRay entity)
        {
            
        }

        #region IAPIDataController

        protected override  void OnEntityDestroy(ITouch3DRay entity)
        {
            this.ITouch3DRayService.Unbind();
             base.OnEntityDestroy(entity);
        }

        private void OnTapGesture(Vector2 point)
        {
            this._PerspectiveRay(point);
        }

        void _PerspectiveRay(Vector2 point)
        {
            var ray = this.Owner.CameraService.ScreenToRay(point);
            RaycastHit info;
            
            var hit = Physics.Raycast(ray.origin, ray.direction, out info, FrankensteinConstants.Constants.TouchMaxRay,
                                      FrankensteinConstants.ClickableLayer);

            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 5, false);

            if (info.collider == null)
                return;

            var hitView = info.collider.GetComponent<ClickableView>();

            if (hitView == null)
                return;

            hitView.OnClickIncoming();
        }

        #endregion


        #region ITouch3DRayService

        void ITouch3DRayService.Unbind()
        {
            this.Owner.InputService.OnTapGesture -= this.OnTapGesture;
        }

        void ITouch3DRayService.Bind()
        {
            this.Owner.InputService.OnTapGesture += this.OnTapGesture;
        }

        #endregion
    }
}