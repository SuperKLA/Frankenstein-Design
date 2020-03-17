using ExampleGame.DTO;
using Frankenstein;
using Frankenstein.Controls.Entities;
using Frankenstein.DTO;

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