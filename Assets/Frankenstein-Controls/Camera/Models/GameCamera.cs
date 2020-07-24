using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frankenstein;
using Frankenstein.Controls.Entities;
using Unity.Entities;
using UnityEngine;

namespace Frankenstein.Controls.Camera.Models
{
    public class GameCamera : APIModel, ICamera, IMainCamera, ITouch2DRay, IGestureInput, IQueryable, ICameraSize
    {
        #region Interface Accessors

        public ICamera       ICamera       => this;
        public IMainCamera   IMainCamera   => this;
        public ITouch2DRay   ITouch2DRay   => this;
        public IGestureInput IGestureInput => this;
        public IQueryable    IQueryable    => this;
        public ICameraSize ICameraSize => this;

        #endregion



        #region Locals

        public Entity CameraEntity { get; set; }

        #endregion


        #region APIModel

        public GameCamera()
        {
            
        }

        public override  void Boot(params object[] any)
        {
            this.IQueryable.Service    =  this.SetupServices<IQueryableService>();
            
            this.IMainCamera.Service   =  this.SetupServices<IMainCameraService>();
            this.IGestureInput.Service =  this.SetupServices<IGestureInputService>();
            this.ITouch2DRay.Service   =  this.SetupServices<ITouch2DRayService>();
            this.ICameraSize.Service =  this.SetupServices<ICameraSizeService>();
            
            this.ICamera.Service =  this.SetupServices<ICameraService>();

            this.ITouch2DRay.Service.Bind();
            this.IMainCamera.Service.AddMainCamera(this.ICamera.Service);
            this.ICamera.Service.SetOrthoSize(this.ICameraSize.Service.GetOrthSize());
        }
        
        #endregion


        #region ICamera

        ICameraService IAPIEntity<ICameraService, ICameraView>.Service { get; set; }

        ICameraView IAPIEntity<ICameraService, ICameraView>.View { get; set; }

        GameObject ICamera.Source => null;

        #endregion


        #region IMainCamera

        IMainCameraService IAPIEntity<IMainCameraService>.Service { get; set; }

        #endregion


        #region ITouch2DRay

        ITouch2DRayService IAPIEntity<ITouch2DRayService>.Service { get; set; }

        ICameraService ITouch2DRay.CameraService => this.ICamera.Service;

        IGestureInputService ITouch2DRay.InputService => this.IGestureInput.Service;

        #endregion


        #region IGestureInput

        IGestureInputService IAPIEntity<IGestureInputService>.Service { get; set; }

        #endregion


        #region IQueryable

        IQueryableService IAPIEntity<IQueryableService>.Service { get; set; }

        List<Guid> IQueryable.Layers => new List<Guid>() {};

        bool IQueryable.Matches<TQuery>()
        {
            return typeof(TQuery).IsInstanceOfType(this);
        }

        TService IQueryable.Provide<TService>()
        {
            if (typeof(TService) == typeof(IMainCameraQuery))
                return (TService) this.IMainCamera.Service;
//            if (typeof(TService) == typeof(ICrewPoolQueryResult))
//                return (TService) this.ICrewPool.Service;
//            else
            return default(TService);
        }

        #endregion


        #region ICameraSize

        ICameraSizeService IAPIEntity<ICameraSizeService, ICameraSizeView>.Service { get; set; }

        ICameraSizeView IAPIEntity<ICameraSizeService, ICameraSizeView>.View { get; set; }

        #endregion
    }
}