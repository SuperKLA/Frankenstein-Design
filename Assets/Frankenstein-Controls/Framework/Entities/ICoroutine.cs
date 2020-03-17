using System;
using Frankenstein;
using UnityEngine;

namespace Frankenstein.Controls.Entities
{
    public interface ICoroutine : IAPIEntity<ICoroutineService>
    {
        GameObject AnyGameObject { get; }
        float TickTime { get; }
    }

    public interface ICoroutineService : IAPIEntityService
    {
        float TickTime { get; }
        
        void RegisterJob(float t, Func<float, bool> callback, Action completed);
        void RegisterJob(Func<float, bool> callback, Action completed);
        void ClearAllJobs();
    }

    public interface ICoroutineView : IAPIEntityView
    {
        
    }
}