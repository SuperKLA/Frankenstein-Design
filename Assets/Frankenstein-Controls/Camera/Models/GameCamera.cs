using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frankenstein.Controls.Entities;
using UnityEngine;

namespace Frankenstein.Controls.Camera.Models
{
    public class GameCamera : APIModel, ICamera, IMainCamera, IQueryable, ICameraSize
    {
        #region Interface Accessors

        public ICamera     ICamera     => this;
        public IMainCamera IMainCamera => this;
        public IQueryable  IQueryable  => this;
        public ICameraSize ICameraSize => this;

        #endregion


        #region Locals

        

        #endregion


        #region APIModel

        public GameCamera()
        {
        }

        public override async Task Boot(params object[] any)
        {
            this.IQueryable.Service = await this.SetupServices<IQueryableService>();

            this.IMainCamera.Service = await this.SetupServices<IMainCameraService>();
            this.ICameraSize.Service = await this.SetupServices<ICameraSizeService>();

            this.ICamera.Service = await this.SetupServices<ICameraService>();

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


        #region IQueryable

        IQueryableService IAPIEntity<IQueryableService>.Service { get; set; }

        List<Guid> IQueryable.Layers => new List<Guid>() { };

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