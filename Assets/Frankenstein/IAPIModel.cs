using System;
using System.Threading.Tasks;

namespace Frankenstein
{
    public interface IAPIModel
    {
        Task Boot(params object[] any);
    }

    public abstract class APIModel : IAPIModel
    {
        protected IoCContainer LocalIOC;

        public APIModel()
        {
            this.LocalIOC = new IoCContainer(false);
        }

        public abstract Task Boot(params Object[] any);

        public virtual async Task Destroy()
        {
            
        }

        protected async Task<T> SetupServices<T>() where T : IAPIEntityService
        {
            var controller = IoCContainer.Current.Resolve<T>();
            if (controller is IAPIController)
            {
                var apiCon = controller as IAPIController;
                apiCon.OnCreating(this);
                await apiCon.CreateView();
                apiCon.OnControllerReady(this);
            }

            return controller;
        }
        
        protected async Task DestroyServices(IAPIEntityService service)
        {
            if (service is IAPIController)
            {
                var apiCon = service as IAPIController;
                await apiCon.OnDestroy(this);
            }
        }
    }
}