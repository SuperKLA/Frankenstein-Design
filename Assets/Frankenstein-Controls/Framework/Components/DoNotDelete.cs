using System;
using Unity.Entities;
using Unity.Mathematics;

namespace Frankenstein.Controls.Components
{
    public interface IDoNotDeleteData : IComponentData
    {
        bool AllowDelete { get; set; }
    }

    [Serializable]
    public struct DoNotDeleteData : IDoNotDeleteData
    {
        public bool _allowDelete;

        public bool AllowDelete
        {
            get => this._allowDelete;
            set => this._allowDelete = value;
        }
    }
}