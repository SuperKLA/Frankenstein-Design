using Frankenstein;
using UnityEngine;

namespace Frankenstein.Controls.Entities
{
    public interface IMoveByPan : IAPIEntity<IMoveByPanService>
    {
        IGestureInputService InputService { get; }
        Vector3              Position     { get; set; }
        Vector3              Forward      { get; }
        Bounds               Bounds       { get; }
        float                Speed        { get; }
        Vector3              Right        { get; }
    }

    public interface IMoveByPanService : IAPIEntityService
    {
        void Unbind();
        void Bind();
    }
}