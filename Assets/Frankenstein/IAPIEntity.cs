namespace Frankenstein
{
    public interface IAPIEntity<T_Service> where T_Service : IAPIEntityService
    {
        T_Service Service { get; set; }
    }
    
    public interface IAPIEntity<T_Service, T_View> where T_Service : IAPIEntityService where T_View : IAPIEntityView
    {
        T_Service Service { get; set; }
        T_View    View    { get; set; }
    }
    
    public interface IAPIEntityService
    {
    }

    public interface IAPIEntityView
    {
    }
}
