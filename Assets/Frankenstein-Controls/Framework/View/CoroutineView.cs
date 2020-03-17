using System;
using System.Collections;
using System.Collections.Generic;
using Frankenstein;
using Frankenstein.Controls.Entities;
using UnityEngine;
using UnityEngine.Profiling;

namespace Frankenstein.Controls.Views
{
    internal struct Coroutine
    {
        internal float t;
        internal float lifeT;
        internal Func<float, bool> callback;
        internal Action completed;

        internal bool Tick(float delta)
        {
            this.lifeT += delta;
            var remapT = Math3d.Remap(this.lifeT, 0f, this.t, 0f, 1f);
            //this.lifeT = Mathf.Clamp(this.lifeT + delta, 0, this.t);
            var forceStop = this.callback(remapT);

            if (forceStop || this.lifeT >= this.t)
            {
                this.completed();
                return true;
            }

            return false;
        }
    }
    
    public class CoroutineView : APIViewBehaviour<ICoroutineService>, ICoroutineView
    {
        private List<Coroutine> _coroutines = new List<Coroutine>();
        private bool _theadAlive = true;
        private UnityEngine.Coroutine _threadPointer;
        private float _tick;
        
        public bool RunsOnUpdate = false;
        

        internal void RegisterJob(float t, Func<float, bool> callback, Action completed)
        {
            this._coroutines.Add(new Coroutine
            {
                t = t,
                callback = callback,
                completed = completed
            });
            
            this.RunsOnUpdate = this._tick == 0;
            if (!this.RunsOnUpdate && this._threadPointer == null)
            {
                this._threadPointer = this.StartCoroutine(this.RoutineThread());
            }
        }
        
        public void Setup(float tick)
        {
            this._tick        = tick;
            this.RunsOnUpdate = this._tick == 0;
        }

        public override void Setup(ICoroutineService service)
        {
            base.Setup(service);

            this._tick = service.TickTime;
            this.RunsOnUpdate = this._tick == 0;
            if (!this.RunsOnUpdate && this._threadPointer == null)
            {
                this._threadPointer = this.StartCoroutine(this.RoutineThread());
            }
        }

        internal void ClearAllJobs()
        {
            this.StopAllCoroutines();
            this._coroutines.Clear();
            this._threadPointer = null;
            this._theadAlive = false;
        }

        private void Update()
        {
            if(!this.RunsOnUpdate) return;
            if(this._coroutines.Count == 0) return;
            
            this.TickAll(Time.deltaTime);
        }

        IEnumerator RoutineThread()
        {
            this._theadAlive = true;
            while (this._theadAlive)
            {
                yield return new WaitForSeconds(this._tick);
                
                if(this._coroutines.Count == 0) continue;
                this.TickAll(this._tick);
            }
        }

        private void TickAll(float t)
        {
            Profiler.BeginSample("CoroutineView->TickAll");
            
            var delta = t;
            var remove = new List<int>();
            
            for (int c = 0; c < this._coroutines.Count; c++)
            {
                var routine = this._coroutines[c];
                var completed = routine.Tick(delta);
                if (completed)
                {
                    this._coroutines.RemoveAt(c);
                    c--;
                    continue;
                }

                this._coroutines[c] = routine;
            }

            //this.ClearTicks(remove);
            Profiler.EndSample();
        }

        private void ClearTicks(List<int> toClear)
        {
            for (int c = 0; c < toClear.Count; c++)
            {
                this._coroutines.RemoveAt(toClear[c]);
            }
        }

        public override void PreDestroy()
        {
            this.StopAllCoroutines();
            this._coroutines.Clear();
            base.PreDestroy();
            this._threadPointer = null;
            this._theadAlive = false;
        }
    }
}

