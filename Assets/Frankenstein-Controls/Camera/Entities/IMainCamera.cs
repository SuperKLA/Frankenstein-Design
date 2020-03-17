using Frankenstein;
using UnityEngine;

namespace Frankenstein.Controls.Entities
{
    public interface IMainCamera : IAPIEntity<IMainCameraService>
    {
        
    }

    public interface IMainCameraService : IAPIEntityService, IMainCameraQuery
    {
        void AddMainCamera(ICameraService svc);
    }
    
    public interface IMainCameraQuery : IQueryService
    {
        Vector3 ScreenToWorldPoint(Vector3 point);
        Vector3 WorldToScreenPoint(Vector3 point);
        Vector3 Position { get; }
        Ray ScreenToRay(Vector3 point);
        
        UnityEngine.Camera Cam { get; }
    }
}