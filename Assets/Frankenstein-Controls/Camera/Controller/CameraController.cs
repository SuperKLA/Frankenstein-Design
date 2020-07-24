using System;
using System.Threading.Tasks;
using Frankenstein.Controls.Components;
using Frankenstein.Controls.Entities;
using Frankenstein.Controls.Views;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
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

        public override async void CreateView()
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
            this._SetupEntity();
        }

        void _SetupEntity()
        {
            //var positionData = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<CameraMovementData>(this.Owner.CameraEntity);
            var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, APIContext.Current.MainBlob);
            var cameraEntity = GameObjectConversionUtility.ConvertGameObjectHierarchy(this._view.gameObject, settings);
#if UNITY_EDITOR
            World.DefaultGameObjectInjectionWorld.EntityManager.SetName(cameraEntity, "Camera");
#endif

            var position = this._view.OwnTransform.position;
            World.DefaultGameObjectInjectionWorld.EntityManager.AddComponentData(cameraEntity, new CameraMovementData()
            {
                Position = position,
                SpawnPosition = position,
                Offset   = this._view.OwnTransform.localPosition
            });
            
            World.DefaultGameObjectInjectionWorld.EntityManager.AddComponentObject(cameraEntity, this._view);
            this.Owner.CameraEntity = cameraEntity;
        }

        protected override void OnEntityDestroy(ICamera entity)
        {
            base.OnEntityDestroy(entity);
            World.DefaultGameObjectInjectionWorld.EntityManager.DestroyEntity(this.Owner.CameraEntity);
            this._view = null;
        }

        #endregion
        

        #region ICameraService

        Entity ICameraQuery.CameraEntity => this.Owner.CameraEntity;
        //        Vector3 ICameraQuery.Position
//        {
//            get => this._view.OwnTransform.position;
//            set => this._view.OwnTransform.position = value;
//        }

        Vector3 ICameraQuery.Position
        {
            get => World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<Translation>(this.Owner.CameraEntity).Value;
            set
            {
                World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(this.Owner.CameraEntity, new Translation()
                {
                    Value = value
                });
            }
        }

        Vector3 ICameraQuery.Forward => World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<LocalToWorld>(this.Owner.CameraEntity).Forward;

        Vector3 ICameraQuery.Right => _view.OwnTransform.right;

        Vector3 ICameraQuery.Up => _view.OwnTransform.up;

        Quaternion ICameraQuery.Rotation
        {
            get => World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<Rotation>(this.Owner.CameraEntity).Value;
            set
            {
                World.DefaultGameObjectInjectionWorld.EntityManager.SetComponentData(this.Owner.CameraEntity, new Rotation()
                {
                    Value = value
                });
            }
        }

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