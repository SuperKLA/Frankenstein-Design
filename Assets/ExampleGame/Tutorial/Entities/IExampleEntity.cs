using Frankenstein;

namespace ExampleGame.Entities
{
    public interface IExampleEntity : IAPIEntity<IExampleEntityService, IExampleEntityView>
    {
        /*
         * Here we think about
         * 
         * what do I need to function?
         * This is what the model has to fulfill
         *
         * Let's say this entity should calculate A + B or get the data from somewhere else
         */
        
        float A { get; }
        float B { get; }
    }

    public interface IExampleEntityService : IAPIEntityService
    {
        /*
         * What does the entity do
         * This is what the controller does
         */
        float Sum();
    }

    public interface IExampleEntityView : IAPIEntityView
    {
        /*
         * The View is a door to connect view visual layer with other entities.
         * it allows other entities to change this value. e.g. A figure exposes his transform
         * to a move entity to give away this responsibility
         */
    }
}