using System;
using System.Collections.Generic;
using Frankenstein;

namespace Frankenstein.Controls.Entities
{
    public interface IQueryable : IAPIEntity<IQueryableService>
    {
        List<Guid> Layers { get; }
        bool Matches<TQuery>();
        TService Provide<TService>() where TService : IQueryService;
    }

    public interface IQueryableService : IAPIEntityService
    {
        IQueryableService GetQueryService(Guid layer);

        TQueryService FindFirstEntity<TQueryService, TQueryable1>() where TQueryService : IQueryService;
        TQueryService FindFirstEntity<TQueryService, TQueryable1, TQueryable2>() where TQueryService : IQueryService;
        TQueryService FindFirstEntity<TQueryService, TQueryable1, TQueryable2, TQueryable3>() where TQueryService : IQueryService;

        TQueryService FindByLambda<TQueryService, TQueryable1>(System.Func<TQueryable1, bool> lambda) where TQueryService : IQueryService;
        TQueryService FindByLambda<TQueryService, TQueryable1, TQueryable2>(System.Func<TQueryable1, TQueryable2, bool> lambda) where TQueryService : IQueryService;
        TQueryService FindByLambda<TQueryService, TQueryable1, TQueryable2, TQueryable3>(System.Func<TQueryable1, TQueryable2, TQueryable3, bool> lambda) where TQueryService : IQueryService;

        IList<TQueryService> FindAll<TQueryService, TQueryable1>() where TQueryService : IQueryService;
        IList<TQueryService> FindAll<TQueryService, TQueryable1, TQueryable2>() where TQueryService : IQueryService;
        IList<TQueryService> FindAll<TQueryService, TQueryable1, TQueryable2, TQueryable3>() where TQueryService : IQueryService;
        
        IList<TQueryService> FindAllByLambda<TQueryService, TQueryable1>(System.Func<TQueryable1, bool> lambda) where TQueryService : IQueryService;

        bool Remove(IQueryable q);
        bool SwitchQueryService(IQueryable self, Guid layer);
    }

    public interface IQueryService
    {
    }
}