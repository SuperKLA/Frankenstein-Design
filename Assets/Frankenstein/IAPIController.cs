using System.Threading.Tasks;

namespace Frankenstein
{
    public interface IAPIController
    {
        void OnCreating(IAPIModel model);
        Task CreateView();
        void OnControllerReady(IAPIModel model);
        Task OnDestroy(IAPIModel model);
    }
    
    public abstract class APIController<T_Entity> : IAPIController
    {
        protected T_Entity Owner;

        public virtual void OnCreating(IAPIModel model)
        {
            var entity = (T_Entity) model;
            if (this.Owner == null)
            {
                this.Owner = entity;
            }

            //this is intentional that model can be something different than the owner, it helps to use singleton pattern with a share alike relation
            this.OnEntityCreated(entity);
        }

        public virtual async Task CreateView()
        {
            
        }
        
        public async Task OnDestroy(IAPIModel model)
        {
            await this.OnEntityDestroy((T_Entity)model);
            this.Owner = default(T_Entity);
        }

        public void OnControllerReady(IAPIModel model)
        {
            this.OnControllerFinished((T_Entity)model);
        }
        
        protected virtual void OnControllerFinished(T_Entity entity)
        {
            
        }

        protected abstract void OnEntityCreated(T_Entity entity);

        protected virtual async Task OnEntityDestroy(T_Entity entity)
        {
            
        }
    }
}
