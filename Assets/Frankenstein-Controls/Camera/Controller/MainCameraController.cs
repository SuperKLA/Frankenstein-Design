using Frankenstein;
using Frankenstein.Controls.Entities;
using UnityEngine;

namespace Frankenstein.Controls.Controller
{
    internal class MainCameraController : APIController<IMainCamera>, IMainCameraService
    {
        private ICameraService _camera;
        internal static UnityEngine.Camera CurrentCamera;

        protected override void OnEntityCreated(IMainCamera entity)
        {
            
        }


        #region IMainCameraService

        void IMainCameraService.AddMainCamera(ICameraService svc)
        {
            CurrentCamera = svc.Cam;
            this._camera = svc;
        }

        #endregion
        
        
        #region IMainCameraQuery

        UnityEngine.Camera IMainCameraQuery.Cam => this._camera.Cam;

        Vector3 IMainCameraQuery.ScreenToWorldPoint(Vector3 point)
        {
            return this._camera.ScreenToWorldPoint(point);
        }

        Vector3 IMainCameraQuery.WorldToScreenPoint(Vector3 point)
        {
            return this._camera.WorldToScreenPoint(point);
        }

        Vector3 IMainCameraQuery.Position
        {
            get { return this._camera.Position; }
        }

        Ray IMainCameraQuery.ScreenToRay(Vector3 point)
        {
            return this._camera.ScreenToRay(point);
        }

        #endregion
    }
}