using Frankenstein;
using Unity.Entities;
using UnityEngine;

namespace Frankenstein.Controls.Entities
{
    public interface ICamera : IAPIEntity<ICameraService, ICameraView>
    {
        Entity CameraEntity { get; set; }
        GameObject Source { get; }
    }

    public interface ICameraService : IAPIEntityService, ICameraQuery
    {
        event System.Action<ICameraService> OnCameraEnabled;
        event System.Action<ICameraService> OnCameraDisabled;
        
        LayerMask        Culling    { get; set; }
        CameraClearFlags ClearFlags { get; set; }
        
        UnityEngine.Camera Cam           { get; }
        float  CameraMaxView { get; }

        void RenderOnTexture(RenderTexture texture);
        void SwitchOnOff(bool onOff);
        void SetOrthoSize(float val);
        
    }

    public interface ICameraView : IAPIEntityView
    {
        Transform CameraTransform { get; }
        UnityEngine.Camera    Cam             { get; }
    }

    public interface ICameraQuery : IQueryService
    {
        Entity CameraEntity { get; }
        Vector3 ScreenToWorldPoint(Vector3 point);
        Vector3 WorldToScreenPoint(Vector3 point);
        
        Vector3 Position { get; set; }
        Vector3 Forward { get; }
        Vector3 Right { get; }
        Vector3 Up { get; }
        
        Quaternion Rotation { get; set; }
        
        Ray ScreenToRay(Vector3 point);
        void AddChild(Transform trans);
    }
}