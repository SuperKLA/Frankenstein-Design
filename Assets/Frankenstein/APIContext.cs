namespace Frankenstein
{
    public interface IAPIContext
    {
        IoCContainer IoC { get; }
    }
    
    public class APIContext : IAPIContext
    {
        public static IAPIContext Current { get; private set; }
        
        public IoCContainer IoC { get; protected set; }

        public APIContext()
        {
            this.IoC = new IoCContainer();
            
            Current = this;
        }

        public void Destroy()
        {

        }
    }
}