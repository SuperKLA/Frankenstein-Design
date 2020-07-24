using Frankenstein;

namespace Frankenstein.Controls.Entities
{
    public interface ITouch2DRay : IAPIEntity<ITouch2DRayService>
    {
        ICameraService       CameraService { get; }
        IGestureInputService InputService  { get; }
    }

    public interface ITouch2DRayService : IAPIEntityService
    {
        void Unbind();
        void Bind();
    }
}