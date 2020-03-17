using Frankenstein;

namespace Frankenstein.Controls.Entities
{
    public interface ICameraSize : IAPIEntity<ICameraSizeService, ICameraSizeView>
    {
      
    }

    public interface ICameraSizeService : IAPIEntityService
    {
        float GetOrthSize();
    }

    public interface ICameraSizeView : IAPIEntityView
    {
        float GetSize();
    }
}