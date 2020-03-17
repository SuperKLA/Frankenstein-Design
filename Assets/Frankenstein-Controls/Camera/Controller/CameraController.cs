using System;
using System.Threading.Tasks;
using Frankenstein.Controls.Entities;
using Frankenstein.Controls.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Frankenstein.Controls.Controller
{
    internal class CameraController : APIController<ICamera>, ICameraService
    {
        private CameraView _view;


        #region APIController

        protected override void OnEntityCreated(ICamera entity)
        {
        }

        public override async Task CreateView()
        {
            CameraView view = null;

            if (this.Owner.Source != null)
            {
                view = this.Owner.Source.GetComponent<CameraView>();
            }
            else
            {
                var asset = await Addressables.InstantiateAsync("CameraView").Task;
                view = asset.GetComponent<CameraView>();
            }

            view.Setup(this);

            this._view = view;
        }

        protected override void OnControllerFinished(ICamera entity)
        {
        }

        protected override async Task OnEntityDestroy(ICamera entity)
        {
            MonoBehaviour.Destroy(this._view);
            this._view = null;
        }

        #endregion


        #region ICameraService

        Vector3 ICameraQuery.Position
        {
            get => this._view.OwnTransform.position;
            set => this._view.OwnTransform.position = value;
        }

        Vector3 ICameraQuery.Forward => _view.transform.forward;

        Vector3 ICameraQuery.Right => _view.transform.right;

        Vector3 ICameraQuery.Up => _view.transform.up;


        UnityEngine.Camera ICameraService.Cam => this._view.OwnCamera;

        public event Action<ICameraService> OnCameraEnabled;
        public event Action<ICameraService> OnCameraDisabled;

        LayerMask ICameraService.Culling
        {
            get => this._view.OwnCamera.cullingMask;
            set => this._view.OwnCamera.cullingMask = value;
        }

        CameraClearFlags ICameraService.ClearFlags
        {
            get => this._view.OwnCamera.clearFlags;
            set => this._view.OwnCamera.clearFlags = value;
        }

        Vector3 ICameraQuery.ScreenToWorldPoint(Vector3 point)
        {
            return this._view.OwnCamera.ScreenToWorldPoint(point);
        }

        Vector3 ICameraQuery.WorldToScreenPoint(Vector3 point)
        {
            return this._view.OwnCamera.WorldToScreenPoint(point);
        }

        Ray ICameraQuery.ScreenToRay(Vector3 point)
        {
            return this._view.OwnCamera.ScreenPointToRay(point);
        }

        float ICameraService.CameraMaxView
        {
            get
            {
                var n = this._view.OwnCamera.nearClipPlane;
                var f = this._view.OwnCamera.farClipPlane;
                return f - n;
            }
        }

        void ICameraService.RenderOnTexture(RenderTexture texture)
        {
            this._view.OwnCamera.targetTexture = texture;
        }

        void ICameraService.SwitchOnOff(bool onOff)
        {
            this._view.OwnCamera.enabled = onOff;
        }

        void ICameraQuery.AddChild(Transform trans)
        {
            trans.SetParent(this._view.OwnTransform);
        }

        void ICameraService.SetOrthoSize(float val)
        {
            this._view.OwnCamera.orthographicSize = val;
        }

        #endregion
    }
}