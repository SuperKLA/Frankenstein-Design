using Frankenstein;
using UnityEngine;

namespace ExampleGame.Entities
{
    public interface ICharacterFigureMovement : IAPIEntity<ICharacterFigureMovementService>
    {
        IGameDataService GameDataService { get; }
        ICharacterFigureService FigureService { get; }
    }

    public interface ICharacterFigureMovementService : IAPIEntityService
    {
        void Move(Vector3 dir);
    }
}