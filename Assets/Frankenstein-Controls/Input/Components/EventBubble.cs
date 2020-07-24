using System;
using Unity.Entities;
using Unity.Physics;

namespace Frankenstein.Controls.Components
{
    public interface IEventBubbleData : IComponentData
    {
        Entity EventEntity { get; set; }
        RaycastHit Hit { get; set; }
    }

    [Serializable]
    public struct EventBubbleData : IEventBubbleData
    {
        public Entity _eventEntity;
        public RaycastHit _hit;

        public RaycastHit Hit
        {
            get => this._hit;
            set => this._hit = value;
        }

        public Entity EventEntity
        {
            get => this._eventEntity;
            set => this._eventEntity = value;
        }
    }
}