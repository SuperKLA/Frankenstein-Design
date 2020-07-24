using ExampleGame.DTO;
using Frankenstein;
using Frankenstein.Controls.Entities;

namespace ExampleGame.Entities
{
    public interface IGameData : IAPIEntity<IGameDataService>
    {
    }

    public interface IGameDataService : IAPIEntityService, IGameDataQuery
    {
        GameConfig GameConfig { get; set; }
    }

    public interface IGameDataQuery : IQueryService
    {
        GameConfig GameConfig { get; }
    }
}