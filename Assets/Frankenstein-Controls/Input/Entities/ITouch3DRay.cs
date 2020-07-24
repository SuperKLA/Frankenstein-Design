using Frankenstein;

namespace Frankenstein.Controls.Entities
{
    public interface ITouch3DRay : IAPIEntity<ITouch3DRayService>
    {
        ICameraService       CameraService { get; }
        IGestureInputService InputService  { get; }
    }

    public interface ITouch3DRayService : IAPIEntityService
    {
        void Unbind();
        void Bind();
    }
}