using Unity.Entities;

namespace Frankenstein
{
    public interface IAPIContext
    {
        IoCContainer IoC { get; }
        BlobAssetStore MainBlob { get; }
    }
    
    public class APIContext : IAPIContext
    {
        public static IAPIContext Current { get; private set; }
        
        public IoCContainer IoC { get; protected set; }
        public BlobAssetStore MainBlob { get; set; }

        public APIContext()
        {
            this.MainBlob = new BlobAssetStore();
            this.IoC = new IoCContainer();
            
            Current = this;
        }

        public void Destroy()
        {
            this.MainBlob.Dispose();
        }
    }
}