using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Frankenstein.Controls.Components
{
    public interface ICameraMovementData : IComponentData
    {
        float3 Position { get; set; }
        float3 Offset { get; set; }
    }

    [Serializable]
    public struct CameraMovementData : ICameraMovementData
    {
        public float3 _position;
        public float3 _offset;
        
        public float3 SpawnPosition;

        public float3 Offset
        {
            get => this._offset;
            set => this._offset = value;
        }

        public float3 Position
        {
            get => this._position;
            set => this._position = value;
        }
    }
}