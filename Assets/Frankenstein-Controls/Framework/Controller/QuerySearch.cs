using System;
using System.Collections.Generic;
using Frankenstein.Controls.Entities;
using UnityEngine.Profiling;

namespace Frankenstein.Controls.Controller
{
    public class QuerySearch : IQueryableService
    {
        private IQueryableService IQueryableService;
        private QueryController   ParentController;
        private List<IQueryable>  AllQueryable;
        public  Guid              Layer { get; private set; }

        public static QuerySearch Create(Guid layer, QueryController parent)
        {
            var result = new QuerySearch();
            result.AllQueryable     = new List<IQueryable>();
            result.ParentController = parent;
            result.Layer            = layer;
            return result;
        }

        public void AddQueryable(IQueryable q)
        {
            if (!this.AllQueryable.Contains(q)) this.AllQueryable.Add(q);
        }

        public bool Remove(IQueryable q)
        {
            return this.AllQueryable.Remove(q);
        }

        #region IQueryableService

        TQueryService IQueryableService.FindFirstEntity<TQueryService, TQueryable1>()
        {
            Profiler.BeginSample("IQueryableService.FindByEntities<TQueryService, TQueryable1>");
            {
                for (int c = 0; c < this.AllQueryable.Count; c++)
                {
                    var q = this.AllQueryable[c];
                    if (q.Matches<TQueryable1>())
                        return (TQueryService) q.Provide<TQueryService>();
                }
            }
            Profiler.EndSample();
            return default(TQueryService);
        }

        TQueryService IQueryableService.FindFirstEntity<TQueryService, TQueryable1, TQueryable2>()
        {
            Profiler.BeginSample("IQueryableService.FindByEntities<TQueryService, TQueryable1, TQueryable2>");
            {
                for (int c = 0; c < this.AllQueryable.Count; c++)
                {
                    var q = this.AllQueryable[c];
                    if (q.Matches<TQueryable1>() && q.Matches<TQueryable2>())
                        return (TQueryService) q.Provide<TQueryService>();
                }
            }
            Profiler.EndSample();
            return default(TQueryService);
        }

        TQueryService IQueryableService.FindFirstEntity<TQueryService, TQueryable1, TQueryable2, TQueryable3>()
        {
            Profiler.BeginSample("IQueryableService.FindByEntities<TQueryService, TQueryable1, TQueryable2, TQueryable3>");
            {
                for (int c = 0; c < this.AllQueryable.Count; c++)
                {
                    var q = this.AllQueryable[c];
                    if (q.Matches<TQueryable1>() && q.Matches<TQueryable2>() && q.Matches<TQueryable3>())
                        return (TQueryService) q.Provide<TQueryService>();
                }
            }
            Profiler.EndSample();
            return default(TQueryService);
        }

        TQueryService IQueryableService.FindByLambda<TQueryService, TQueryable1>(Func<TQueryable1, bool> lambda)
        {
            for (int c = 0; c < this.AllQueryable.Count; c++)
            {
                var q = this.AllQueryable[c];
                if (q.Matches<TQueryable1>() && lambda((TQueryable1) q))
                    return (TQueryService) q.Provide<TQueryService>();
            }

            return default(TQueryService);
        }

        TQueryService IQueryableService.FindByLambda<TQueryService, TQueryable1, TQueryable2>(Func<TQueryable1, TQueryable2, bool> lambda)
        {
            for (int c = 0; c < this.AllQueryable.Count; c++)
            {
                var q = this.AllQueryable[c];
                if (q.Matches<TQueryable1>() && q.Matches<TQueryable2>() && lambda((TQueryable1) q, (TQueryable2) q))
                    return (TQueryService) q.Provide<TQueryService>();
            }

            return default(TQueryService);
        }

        TQueryService IQueryableService.FindByLambda<TQueryService, TQueryable1, TQueryable2, TQueryable3>(
            Func<TQueryable1, TQueryable2, TQueryable3, bool> lambda)
        {
            for (int c = 0; c < this.AllQueryable.Count; c++)
            {
                var q = this.AllQueryable[c];
                if (q.Matches<TQueryable1>() && q.Matches<TQueryable2>() && q.Matches<TQueryable3>() && lambda((TQueryable1) q, (TQueryable2) q, (TQueryable3) q))
                    return (TQueryService) q.Provide<TQueryService>();
            }

            return default(TQueryService);
        }

        IQueryableService IQueryableService.GetQueryService(Guid layer)
        {
            return this.ParentController.EnsureSearchProvider(layer);
        }


        IList<TQueryService> IQueryableService.FindAll<TQueryService, TQueryable1>()
        {
            var result = new List<TQueryService>();
            Profiler.BeginSample("IQueryableService.FindAllByEntity<TQueryService, TQueryable1>");
            {
                for (int c = 0; c < this.AllQueryable.Count; c++)
                {
                    var q = this.AllQueryable[c];
                    if (q.Matches<TQueryable1>())
                        result.Add(q.Provide<TQueryService>());
                }
            }
            Profiler.EndSample();
            return result;
        }

        IList<TQueryService> IQueryableService.FindAll<TQueryService, TQueryable1, TQueryable2>()
        {
            var result = new List<TQueryService>();
            Profiler.BeginSample("IQueryableService.FindAllByEntity<TQueryService, TQueryable1, TQueryable2>");
            {
                for (int c = 0; c < this.AllQueryable.Count; c++)
                {
                    var q = this.AllQueryable[c];
                    if (q.Matches<TQueryable1>() && q.Matches<TQueryable2>())
                        result.Add(q.Provide<TQueryService>());
                }
            }
            Profiler.EndSample();
            return result;
        }

        IList<TQueryService> IQueryableService.FindAll<TQueryService, TQueryable1, TQueryable2, TQueryable3>()
        {
            var result = new List<TQueryService>();
            Profiler.BeginSample("IQueryableService.FindAllByEntity<TQueryService, TQueryable1, TQueryable2, TQueryable3>");
            {
                for (int c = 0; c < this.AllQueryable.Count; c++)
                {
                    var q = this.AllQueryable[c];
                    if (q.Matches<TQueryable1>() && q.Matches<TQueryable2>() && q.Matches<TQueryable3>())
                        result.Add(q.Provide<TQueryService>());
                }
            }
            Profiler.EndSample();
            return result;
        }

        IList<TQueryService> IQueryableService.FindAllByLambda<TQueryService, TQueryable1>(Func<TQueryable1, bool> lambda)
        {
            var result = new List<TQueryService>();
            Profiler.BeginSample("IQueryableService.FindAllByLambda<TQueryService, TQueryable1>");
            {
                for (int c = 0; c < this.AllQueryable.Count; c++)
                {
                    var q = this.AllQueryable[c];
                    if (q.Matches<TQueryable1>() && lambda((TQueryable1) q))
                        result.Add(q.Provide<TQueryService>());
                }
            }
            
            Profiler.EndSample();
            return result;
        }

        bool IQueryableService.SwitchQueryService(IQueryable self, Guid layer)
        {
            if (!this.IQueryableService.Remove(self))
            {
                //Debug.LogError("SwitchQueryService-> can not switch, can not find own instance");
                return false;
            }

            var svc = this.IQueryableService.GetQueryService(layer);
            if (svc == null)
            {
                //Debug.LogError("SwitchQueryService-> service not available");
                return false;
            }

            var instance = svc as QuerySearch;
            instance.AddQueryable(self);

            self.Service = svc;
            return true;
        }


        public void Dispose()
        {
        }

        #endregion
    }
}