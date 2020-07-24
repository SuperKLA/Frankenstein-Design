using System;
using Unity.Entities;
using Unity.Physics;

namespace Frankenstein.Controls.Components
{
    public interface IOnClickEventData : IComponentData
    {
        bool IsClicked { get; set; }
    }

    [Serializable]
    public struct OnClickEventData : IOnClickEventData
    {
        public bool _isClicked;

        public bool IsClicked
        {
            get => this._isClicked;
            set => this._isClicked = value;
        }
    }
}