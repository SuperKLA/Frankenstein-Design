using System;
using System.Threading.Tasks;
using DigitalRubyShared;
using Frankenstein;
using Frankenstein.Controls.Entities;
using UnityEngine;

namespace Frankenstein.Controls.Controller
{
    internal class GestureInputController : APIController<IGestureInput>, IGestureInputService 
    {
        private TapGestureRecognizer tapGesture;
        private PanGestureRecognizer _panGesture;
        private ScaleGestureRecognizer _scaleGesture;
        

        #region APIController

        protected override void OnEntityCreated(IGestureInput entity)
        {
            this.CreateTapGesture();
            this.CreatePanGesture();
            this.CreateScaleGesture();
        }

        protected override  void OnEntityDestroy(IGestureInput entity)
        {
             base.OnEntityDestroy(entity);
            
            if(this.tapGesture != null)
                FingersScript.Instance.RemoveGesture(this.tapGesture);
            
            if(this._panGesture != null)
                FingersScript.Instance.RemoveGesture(this._panGesture);
            
            if(this._scaleGesture != null)
                FingersScript.Instance.RemoveGesture(this._scaleGesture);
        }

        private void CreateScaleGesture()
        {
            this._scaleGesture =  new ScaleGestureRecognizer();
            this._scaleGesture.StateUpdated += this.ScaleGestureCallback;

            FingersScript.Instance.AddGesture(this._scaleGesture);
        }
        
        private void ScaleGestureCallback(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Executing)
            {
                var scale = this._scaleGesture.ScaleMultiplier;
                Debug.Log("ScaleGestureCallback" + scale);
                this._TriggerScale(scale);
            }
        }
        
        private void CreateTapGesture()
        {
            this.tapGesture              =  new TapGestureRecognizer();
            this.tapGesture.StateUpdated += this.TapGestureCallback;

            FingersScript.Instance.AddGesture(this.tapGesture);
        }

        private void TapGestureCallback(GestureRecognizer gesture)
        {
            var point = new Vector2(gesture.FocusX, gesture.FocusY);
            if (gesture.State == GestureRecognizerState.Ended)
            {
                this._TriggerTap(point);
            }
        }

        private void CreatePanGesture()
        {
            this._panGesture = new PanGestureRecognizer();
#if UNITY_EDITOR
            this._panGesture.MinimumNumberOfTouchesToTrack = 1;
#else
            this._panGesture.MinimumNumberOfTouchesToTrack = 1;
#endif

            this._panGesture.StateUpdated += this.PanGestureCallback;
            FingersScript.Instance.AddGesture(this._panGesture);
        }

        private void PanGestureCallback(GestureRecognizer gesture)
        {
            if (gesture.State == GestureRecognizerState.Executing)
            {
                float   deltaX = this._panGesture.DeltaX / 25.0f;
                float   deltaY = this._panGesture.DeltaY / 25.0f;

                var delta = new Vector2(deltaX, deltaY);

                this._TriggerPan(delta);
            }
        }

        void _TriggerPan(Vector2 delta)
        {
            if (this.OnPanGesture != null)
            {
                this.OnPanGesture(delta);
            }
        }
        
        void _TriggerTap(Vector2 delta)
        {
            if (this.OnTapGesture != null)
            {
                this.OnTapGesture(delta);
            }
        }
        
        void _TriggerScale(float scale)
        {
            if (this.OnScaleGesture != null)
            {
                this.OnScaleGesture(scale);
            }
        }
        #endregion


        #region ICameraService

        public event Action<Vector2> OnPanGesture;
        public event Action<Vector2> OnTapGesture;
        public event Action<float> OnScaleGesture;

        #endregion
    }
}