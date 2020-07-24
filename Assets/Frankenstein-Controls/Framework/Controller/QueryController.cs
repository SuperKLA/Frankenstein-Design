using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frankenstein;
using Frankenstein.Controls.Entities;

namespace Frankenstein.Controls.Controller
{
    public class QueryController : APIController<IQueryable>, IQueryableService
    {
        private static QueryController               _Context;
        private        Dictionary<Guid, QuerySearch> QuerySearches;
        private        IQueryableService             _querySearch;

        protected override void OnEntityCreated(IQueryable query)
        {
            //TODO Addqueryable als sich selbst hinzufügen.
            var context = this._Setup();

            this._querySearch = context._AddQueryable(query);
        }

        public QuerySearch EnsureSearchProvider(Guid layer)
        {
            if (this.QuerySearches.ContainsKey(layer))
            {
                return this.QuerySearches[layer];
            }
            else
            {
                var r = QuerySearch.Create(layer, this);
                this.QuerySearches.Add(layer, r);
                return r;
            }
        }

        QuerySearch _AddQueryable(IQueryable q)
        {
            var         layers = q.Layers;
            QuerySearch last   = null;

            for (int c = 0; c < layers.Count; c++)
            {
                var layer = layers[c];
                last = this.EnsureSearchProvider(layer);
                last.AddQueryable(q);
            }

            return last;
        }

        protected override  void OnEntityDestroy(IQueryable query)
        {
            var context = this._Setup();
            var layers  = query.Layers;

            for (int c = 0; c < layers.Count; c++)
            {
                var layer  = layers[c];
                var search = context.EnsureSearchProvider(layer);
                search.Remove(query);
            }

            query.Service = null;
        }

        QueryController _Setup()
        {
            if (_Context != null) return _Context;

            var self = new QueryController();
            self.QuerySearches = new Dictionary<Guid, QuerySearch>();

            return _Context = self;
        }


        #region IQueryableService

        IQueryableService IQueryableService.GetQueryService(Guid layer)
        {
            return this._querySearch.GetQueryService(layer);
        }

        TQueryService IQueryableService.FindFirstEntity<TQueryService, TQueryable1>()
        {
            return this._querySearch.FindFirstEntity<TQueryService, TQueryable1>();
        }

        TQueryService IQueryableService.FindFirstEntity<TQueryService, TQueryable1, TQueryable2>()
        {
            return this._querySearch.FindFirstEntity<TQueryService, TQueryable1, TQueryable2>();
        }

        TQueryService IQueryableService.FindFirstEntity<TQueryService, TQueryable1, TQueryable2, TQueryable3>()
        {
            return this._querySearch.FindFirstEntity<TQueryService, TQueryable1, TQueryable2, TQueryable3>();
        }

        TQueryService IQueryableService.FindByLambda<TQueryService, TQueryable1>(Func<TQueryable1, bool> lambda)
        {
            return this._querySearch.FindByLambda<TQueryService, TQueryable1>(lambda);
        }

        TQueryService IQueryableService.FindByLambda<TQueryService, TQueryable1, TQueryable2>(Func<TQueryable1, TQueryable2, bool> lambda)
        {
            return this._querySearch.FindByLambda<TQueryService, TQueryable1, TQueryable2>(lambda);
        }

        TQueryService IQueryableService.FindByLambda<TQueryService, TQueryable1, TQueryable2, TQueryable3>(
            Func<TQueryable1, TQueryable2, TQueryable3, bool> lambda)
        {
            return this._querySearch.FindByLambda<TQueryService, TQueryable1, TQueryable2, TQueryable3>(lambda);
        }

        IList<TQueryService> IQueryableService.FindAll<TQueryService, TQueryable1>()
        {
            return this._querySearch.FindAll<TQueryService, TQueryable1>();
        }

        IList<TQueryService> IQueryableService.FindAll<TQueryService, TQueryable1, TQueryable2>()
        {
            return this._querySearch.FindAll<TQueryService, TQueryable1, TQueryable2>();
        }

        IList<TQueryService> IQueryableService.FindAll<TQueryService, TQueryable1, TQueryable2, TQueryable3>()
        {
            return this._querySearch.FindAll<TQueryService, TQueryable1, TQueryable2, TQueryable3>();
        }

        IList<TQueryService> IQueryableService.FindAllByLambda<TQueryService, TQueryable1>(Func<TQueryable1, bool> lambda)
        {
            return this._querySearch.FindAllByLambda<TQueryService, TQueryable1>(lambda);
        }

        bool IQueryableService.Remove(IQueryable q)
        {
            return this._querySearch.Remove(q);        
        }

        bool IQueryableService.SwitchQueryService(IQueryable self, Guid layer)
        {
            return this._querySearch.SwitchQueryService(self, layer);   
        }

        #endregion
    }
}