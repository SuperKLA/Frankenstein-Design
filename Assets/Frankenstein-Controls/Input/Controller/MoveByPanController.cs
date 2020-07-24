using System.Threading.Tasks;
using DigitalRubyShared;
using Frankenstein;
using Frankenstein.Controls.Entities;
using Frankenstein.Controls.Views;
using UnityEngine;

namespace Frankenstein.Controls.Controller
{
    public class MoveByPanController : APIController<IMoveByPan>, IMoveByPanService
    {
        private IMoveByPanService IMoveByTapService => this;

        #region IAPIDataController

        protected override void OnEntityCreated(IMoveByPan entity)
        {
        }

        protected override  void OnEntityDestroy(IMoveByPan entity)
        {
            this.IMoveByTapService.Unbind();
             base.OnEntityDestroy(entity);
        }

        private void OnPanGesture(Vector2 delta)
        {
            this._Move(delta);
        }

        void _Move(Vector2 delta)
        {
            var pos         = this.Owner.Position;
            var forward     = this.Owner.Forward;
            var right       = this.Owner.Right;
            var bounds      = this.Owner.Bounds;
            var normalSpeed = this.Owner.Speed;

            var y = pos.y;
            
            pos = pos + right   * (-delta.x * normalSpeed);
            pos = pos + forward * (-delta.y * normalSpeed);

            pos.y = 1;
            
//            pos.z = posZ.z;
//            pos.x = posX.x;

            if (!bounds.Contains(pos)) return;
            
            pos.y = y;
            
            this.Owner.Position = pos;
        }

        #endregion


        #region ITouchRay2DService

        void IMoveByPanService.Unbind()
        {
            this.Owner.InputService.OnPanGesture -= this.OnPanGesture;
        }

        void IMoveByPanService.Bind()
        {
            this.Owner.InputService.OnPanGesture += this.OnPanGesture;
        }

        #endregion
    }
}