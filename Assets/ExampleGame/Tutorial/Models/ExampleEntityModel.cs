using System.Threading.Tasks;
using ExampleGame.Entities;
using Frankenstein;

namespace ExampleGame
{
    /*
     * Demonstration only
     * I am a model, I use Entities to describe something real in a project.
     * The amount of my entities is what makes me.
     * I can reuse different entities, I just need to satisfied what data they need
     */
    public class ExampleEntityModel : APIModel, IExampleEntity, IGameData
    {
        //this is just a gimmick, it makes easier to understand where data is coming from
        #region Interface Accessors
        
        private IExampleEntity IExampleEntity => this;
        private IGameData      IGameData      => this;

        #endregion


        #region Locals

        #endregion


        #region APIModel

        public override async Task Boot(params object[] any)
        {
            this.IExampleEntity.Service = await this.SetupServices<IExampleEntityService>();
            this.IGameData.Service      = await this.SetupServices<IGameDataService>();
        }
        
        #endregion


        #region IExampleEntity

        IExampleEntityService IAPIEntity<IExampleEntityService, IExampleEntityView>.Service { get; set; }

        IExampleEntityView IAPIEntity<IExampleEntityService, IExampleEntityView>.View { get; set; }

        float IExampleEntity.A => 0;

        float IExampleEntity.B => 1;

        #endregion


        #region IGameData

        IGameDataService IAPIEntity<IGameDataService>.Service { get; set; }

        #endregion
    }
}