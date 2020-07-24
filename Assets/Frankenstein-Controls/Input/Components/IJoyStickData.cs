using System;
using Unity.Entities;

namespace Frankenstein.Controls.Components
{
    public interface IJoyStickData : IComponentData
    {
        float VerticalAmount   { get; set; }
        float HorizontalAmount { get; set; }
    }

    [Serializable]
    public struct JoyStickData : IJoyStickData
    {
        public float _verticalAmount;
        public float _horizontalAmount;

        public float VerticalAmount
        {
            get => this._verticalAmount;
            set => this._verticalAmount = value;
        }

        public float HorizontalAmount
        {
            get => this._horizontalAmount;
            set => this._horizontalAmount = value;
        }
    }
}