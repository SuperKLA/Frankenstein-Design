using ExampleGame.DTO;
using ExampleGame.Entities;
using Frankenstein;

namespace ExampleGame.Controller
{
    public class GameDataController : APIController<IGameData>, IGameDataService
    {
        private IGameDataService IGameDataService => this;

        #region Controller

        protected override void OnEntityCreated(IGameData entity)
        {
        }

        protected override void OnControllerFinished(IGameData entity)
        {
            var dto = IoCContainer.Current.Resolve<IGameDTO>();
            this.IGameDataService.GameConfig = dto.GameConfig.ToDTO();
        }

        #endregion


        #region IGameDataService

        GameConfig IGameDataQuery.GameConfig => this.IGameDataService.GameConfig;

        GameConfig IGameDataService.GameConfig { get; set; }

        #endregion
    }
}