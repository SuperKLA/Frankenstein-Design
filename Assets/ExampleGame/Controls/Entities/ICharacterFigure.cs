using Frankenstein;
using UnityEngine;

namespace ExampleGame.Entities
{
    public interface ICharacterFigure : IAPIEntity<ICharacterFigureService, ICharacterFigureView>
    {
    }

    public interface ICharacterFigureService : IAPIEntityService
    {
        void Move(Vector3 planeDir);
    }
    
    public interface ICharacterFigureView : IAPIEntityView
    {
        Transform Root { get; }
    }
}