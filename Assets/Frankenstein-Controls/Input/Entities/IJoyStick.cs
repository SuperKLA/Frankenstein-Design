using System;
using Frankenstein;
using Unity.Entities;
using UnityEngine;

namespace Frankenstein.Controls.Entities
{
    public interface IJoyStick : IAPIEntity<IJoyStickService>
    {
        GameObject Source { get; }
    }

    public interface IJoyStickService : IAPIEntityService, IJoyStickQuery
    {
        
    }
    
    public interface IJoyStickView : IAPIEntityView
    {

    }
    
    public interface IJoyStickQuery : IQueryService
    {
        void AddToEntity(Entity entity);
    }
}