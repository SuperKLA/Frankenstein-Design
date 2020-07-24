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

        public override  void Boot(params object[] any)
        {
            this.IQueryable.Service               =  this.SetupServices<IQueryableService>();
            this.IGameData.Service                =  this.SetupServices<IGameDataService>();
            this.ICharacterFigure.Service         =  this.SetupServices<ICharacterFigureService>();
            this.ICharacterFigureMovement.Service =  this.SetupServices<ICharacterFigureMovementService>();
        }

        public override  void Destroy()
        {
             this.DestroyServices(this.IQueryable.Service);
             this.DestroyServices(this.IGameData.Service);
             this.DestroyServices(this.ICharacterFigure.Service);
             this.DestroyServices(this.ICharacterFigureMovement.Service);
             base.Destroy();
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