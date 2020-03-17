using Frankenstein;
using Frankenstein.Controls.Entities;
using UnityEngine;

namespace Frankenstein.Controls.Views
{
    public class CameraView : APIViewBehaviour<ICameraService>, ICameraView
    {
        public Transform OwnTransform;
        public UnityEngine.Camera    OwnCamera;


        #region ICameraView

        Transform ICameraView.CameraTransform => OwnTransform;

        UnityEngine.Camera ICameraView.Cam => OwnCamera;

        #endregion
    }
}