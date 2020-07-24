using System;
using Frankenstein;
using UnityEngine;

namespace Frankenstein.Controls.Entities
{
    public interface IGestureInput : IAPIEntity<IGestureInputService>
    {
        
    }

    public interface IGestureInputService : IAPIEntityService
    {
        event Action<Vector2> OnPanGesture;
        event Action<Vector2> OnTapGesture;
        event Action<float> OnScaleGesture;
    }
}