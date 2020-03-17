namespace Frankenstein
{
    public interface IAPIBoot
    {
        void Boot(IoCContainer container);
    }
    
    public abstract class APIBootable : APIScriptable, IAPIBoot
    {
        public abstract void Boot(IoCContainer container);
    }
}