using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleGame.Entities;
using Frankenstein;
using Frankenstein.Controls.Entities;
using UnityEngine;

namespace ExampleGame
{
    public class Character : APIModel, IQueryable, ICharacterFigure, IGameData, ICharacterFigureMovement
    {
        #region Interface Accessors

        private IQueryable               IQueryable               => this;
        private ICharacterFigure         ICharacterFigure         => this;
        private IGameData                IGameData                => this;
        private ICharacterFigureMovement ICharacterFigureMovement => this;

        #endregion


        #region Locals

        public IGameDataService GameDataService => this.IGameData.Service;

        #endregion


        #region APIModel

        public override async Task Boot(params object[] any)
        {
            this.IQueryable.Service               = await this.SetupServices<IQueryableService>();
            this.IGameData.Service                = await this.SetupServices<IGameDataService>();
            this.ICharacterFigure.Service         = await this.SetupServices<ICharacterFigureService>();
            this.ICharacterFigureMovement.Service = await this.SetupServices<ICharacterFigureMovementService>();
        }

        public override async Task Destroy()
        {
            await this.DestroyServices(this.IQueryable.Service);
            await this.DestroyServices(this.IGameData.Service);
            await this.DestroyServices(this.ICharacterFigure.Service);
            await this.DestroyServices(this.ICharacterFigureMovement.Service);
            await base.Destroy();
        }

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
//            if (typeof(TService) == typeof(ICrewPoolQueryResult))
//                return (TService) this.ICrewPool.Service;
//            else
            return default(TService);
        }

        #endregion


        #region ICharacterFigure

        ICharacterFigureService IAPIEntity<ICharacterFigureService, ICharacterFigureView>.Service { get; set; }

        ICharacterFigureView IAPIEntity<ICharacterFigureService, ICharacterFigureView>.View { get; set; }

        #endregion


        #region IGameData

        IGameDataService IAPIEntity<IGameDataService>.Service { get; set; }

        #endregion


        #region ICharacterFigureMovement

        ICharacterFigureMovementService IAPIEntity<ICharacterFigureMovementService>.Service { get; set; }

        ICharacterFigureService ICharacterFigureMovement.FigureService => this.ICharacterFigure.Service;

        #endregion
    }
}