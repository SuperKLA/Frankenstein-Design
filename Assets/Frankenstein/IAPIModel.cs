using System;
using System.Threading.Tasks;

namespace Frankenstein
{
    public interface IAPIModel
    {
        void Boot(params object[] any);
    }

    public abstract class APIModel : IAPIModel
    {
        protected IoCContainer LocalIOC;

        public APIModel()
        {
            this.LocalIOC = new IoCContainer(false);
        }

        public abstract void Boot(params object[] any);

        public virtual void Destroy()
        {
            
        }

        protected T SetupServices<T>() where T : IAPIEntityService
        {
            var controller = IoCContainer.Current.Resolve<T>();
            if (controller is IAPIController)
            {
                var apiCon = controller as IAPIController;
                apiCon.OnCreating(this);
                apiCon.CreateView();
                apiCon.OnControllerReady(this);
            }

            return controller;
        }
        
        protected void DestroyServices(IAPIEntityService service)
        {
            if (service is IAPIController)
            {
                var apiCon = service as IAPIController;
                apiCon.OnDestroy(this);
            }
        }
    }
}