# Frankenstein-Design
Welcome to the Frankenstein Design for Unity. A MVC software design to create games and more.


## What's this all about?

If you ask yourself the following questions:

* how can I create a general solution for all my projects?
* how can I write maintainable, clean and extensible code?
* how can I better control changes and effects in my code?

then Frankenstein is something for you.

## S.O.L.I.D.
Frankenstein is a software design for Unity and is strictly based on S.O.L.I.D. It can be used for small and large projects and is lightweight at the same time.

## MVC
Everything is built according to MVC, keep that in mind when you look at the example project.


# General structure and vocabulary

## Entities
The MVC is joined by entities. They make the whole system very flexible. This is what it looks like.

### Benefits
* The entity layer only serves to describe individual features. An entity always has its own very specific task.  If something is changed here, it has to be adapted everywhere in the project, in this case it is something good.  I see what effects it has on the system and if all models support the change.

```c#
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
         * This is what the controller does and and which functions it provides me with 
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
```


## Controller
Controllers implement entities.

### Benefits
* The controller layer only implements the entity. The controller does not care where data comes from. In the controller you can completely forget the surrounding program/project, everything the controller needs is defined by the entity.
* Controllers are excellent testable, because they only have reference to their entity and are cut off from the rest of the system.

```c#
using System.Threading.Tasks;
using ExampleGame.Entities;
using ExampleGame.Views;
using Frankenstein;
using Frankenstein.Controls.Views;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ExampleGame.Controller
{
    /*
     * There is only one controller per Entity
     */
    public class ExampleEntityController : APIController<IExampleEntity>, IExampleEntityService
    {
        private ExampleEntityView _view;

        #region Controller

        protected override void OnEntityCreated(IExampleEntity entity)
        {
            //A controller has different States, use them to boot properly
        }

        public override async Task CreateView()
        {
            //optional, demonstration with addressables
            var asset = await Addressables.InstantiateAsync("Example").Task;
            var view  = asset.GetComponent<ExampleEntityView>();
            view.Setup(this);

            this._view      = view;
            this.Owner.View = view;
        }

        #endregion


        #region IExampleEntityService

        float IExampleEntityService.Sum()
        {
            // this.Owner is the owner of the entity, the one who is implementing it.
            return this.Owner.A + this.Owner.B;
        }

        #endregion
    }
}
```


## Models / Domain
Models fulfill entities and connect them with each other. Only this layer knows what your project/game is doing.
*This is a very simplified example.*

### Benefits
* Everything that makes up a model is determined by its entities, so you can see directly what the model does. It can get more capability at any time without hurting others.
* Interface Accessors make it easier to read data flow.
* A model can include any entity, it just needs to make sure its conditions/implementation is correct, then it gets this function. Yes, a Super Object that has all function is conceivable.
* Async and await are included, so asynchronous loading is much easier. See Unity.Addressables


```c#
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
```


## The glue that holds everything together
IoC Container

### Benefits
* Controllers can be defined as singleton and allow data exchange between models via entities.
* Depending on the target platform a different controller can be returned.

```c#
using ExampleGame.Controller;
using ExampleGame.Entities;
using Frankenstein;

namespace ExampleGame
{
    public class GameBoot : IAPIBoot
    {
        public static IAPIBoot Create()
        {
            return new GameBoot();
        }

        void IAPIBoot.Boot(IoCContainer container)
        {
            // That's how entities are linked with controller
            // you could also use different controller per platform
            container.Register<IExampleEntityService>(() => new ExampleEntityController());
            
            container.Register<IGameArenaService>(() => new GameArenaController());
            container.Register<IGameArenaGUIService>(() => new GameArenaGUIController());
            
            container.Register<ICharacterFigureService>(() => new CharacterFigureController());
            container.Register<ICharacterFigureMovementService>(() => new CharacterFigureMovementController());
            
            container.Register<IGameDataService>(() => new GameDataController()).AsSingleton();
        }
    }
}
```

## Congratulations you now know the basics of Frankenstein Design.
But there is more hidden in the details.

# Let's look at a real example
A mini game consisting of a GameArena and a Character, these are also our models.
*Assets/ExampleGame/Controls*


## The Character
*Assets/ExampleGame/Controls/Models/Character.cs*

* IGameData returns the GameConfig.
* ICharacterFigure the figure itself.
* ICharacterFigureMovement only movement, one could also make it more abstract and just call it movement. Then everyone who wants to move could use it.
* Entities can define dependencies on each other, see ICharacterFigureMovement.

```c#
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleGame.Entities;
using Frankenstein;
using Frankenstein.Controls.Entities;
using UnityEngine;

namespace ExampleGame
{
    public class Character : APIModel, IQueryable, ICharacterFigure, IGameData, ICharacterFigureMovement
    {
        #region Interface Accessors

        private IQueryable               IQueryable               => this;
        private ICharacterFigure         ICharacterFigure         => this;
        private IGameData                IGameData                => this;
        private ICharacterFigureMovement ICharacterFigureMovement => this;

        #endregion


        #region Locals
        // You can share variables
        public IGameDataService GameDataService => this.IGameData.Service;

        #endregion


        #region APIModel

        public override async Task Boot(params object[] any)
        {
            this.IQueryable.Service               = await this.SetupServices<IQueryableService>();
            this.IGameData.Service                = await this.SetupServices<IGameDataService>();
            this.ICharacterFigure.Service         = await this.SetupServices<ICharacterFigureService>();
            this.ICharacterFigureMovement.Service = await this.SetupServices<ICharacterFigureMovementService>();
        }

        public override async Task Destroy()
        {
            await this.DestroyServices(this.IQueryable.Service);
            await this.DestroyServices(this.IGameData.Service);
            await this.DestroyServices(this.ICharacterFigure.Service);
            await this.DestroyServices(this.ICharacterFigureMovement.Service);
            await base.Destroy();
        }

        #endregion


        #region IQueryable

        IQueryableService IAPIEntity<IQueryableService>.Service { get; set; }

        List<Guid> IQueryable.Layers => new List<Guid>() { };

        bool IQueryable.Matches<TQuery>()
        {
            return typeof(TQuery).IsInstanceOfType(this);
        }

        TService IQueryable.Provide<TService>()
        {
//            if (typeof(TService) == typeof(ICrewPoolQueryResult))
//                return (TService) this.ICrewPool.Service;
//            else
            return default(TService);
        }

        #endregion


        #region ICharacterFigure

        ICharacterFigureService IAPIEntity<ICharacterFigureService, ICharacterFigureView>.Service { get; set; }

        ICharacterFigureView IAPIEntity<ICharacterFigureService, ICharacterFigureView>.View { get; set; }

        #endregion


        #region IGameData

        IGameDataService IAPIEntity<IGameDataService>.Service { get; set; }

        #endregion


        #region ICharacterFigureMovement

        ICharacterFigureMovementService IAPIEntity<ICharacterFigureMovementService>.Service { get; set; }

        ICharacterFigureService ICharacterFigureMovement.FigureService => this.ICharacterFigure.Service;

        #endregion
    }
}
```


## The GameArena
This is the main playing field.
*Assets/ExampleGame/Controls/Models/GameArena.cs*

* IScene loads the corresponding scene.
* Here the order is critical for both Boot and Destroy.
* The model also implements an entity related to the GUI.

```c#
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExampleGame.Entities;
using Frankenstein;
using Frankenstein.Controls.Entities;

namespace ExampleGame
{
    public class GameArena : APIModel, IQueryable, IScene, IGameArena, IGameArenaGUI
    {
        #region Interface Accessors

        private IQueryable    IQueryable    => this;
        private IScene        IScene        => this;
        private IGameArena    IGameArena    => this;
        private IGameArenaGUI IGameArenaGUI => this;

        #endregion


        #region Locals

        public ISceneService SceneService => this.IScene.Service;
        
        #endregion


        #region APIModel

        public override async Task Boot(params object[] any)
        {
            this.IQueryable.Service = await this.SetupServices<IQueryableService>();
            this.IScene.Service     = await this.SetupServices<ISceneService>();

            await this.IScene.Service.LoadScene("ExampleGame1");
            this.IScene.Service.SetAsMain();

            this.IGameArena.Service    = await this.SetupServices<IGameArenaService>();
            this.IGameArenaGUI.Service = await this.SetupServices<IGameArenaGUIService>();

            // For the sake of simplicity no ObjectPool is used here
            await new Character().Boot();
        }

        public override async Task Destroy()
        {
            await this.DestroyServices(this.IQueryable.Service);
            await this.DestroyServices(this.IGameArena.Service);
            await this.DestroyServices(this.IGameArenaGUI.Service);
            await this.DestroyServices(this.IScene.Service);
            await base.Destroy();
        }

        #endregion


        #region IQueryable

        IQueryableService IAPIEntity<IQueryableService>.Service { get; set; }

        List<Guid> IQueryable.Layers => new List<Guid>() { };

        bool IQueryable.Matches<TQuery>()
        {
            return typeof(TQuery).IsInstanceOfType(this);
        }

        TService IQueryable.Provide<TService>()
        {
//            if (typeof(TService) == typeof(ICrewPoolQueryResult))
//                return (TService) this.ICrewPool.Service;
//            else
            return default(TService);
        }

        #endregion


        #region IScene

        ISceneService IAPIEntity<ISceneService>.Service { get; set; }

        #endregion


        #region IGameArena

        IGameArenaService IAPIEntity<IGameArenaService, IGameArenaView>.Service { get; set; }

        IGameArenaView IAPIEntity<IGameArenaService, IGameArenaView>.View { get; set; }

        void IGameArena.OnWin()
        {
            this.IGameArenaGUI.Service.ShowWinScreen();
        }

        #endregion


        #region IGameArenaGUI

        IGameArenaGUIService IAPIEntity<IGameArenaGUIService, IGameArenaGUIView>.Service { get; set; }

        IGameArenaGUIView IAPIEntity<IGameArenaGUIService, IGameArenaGUIView>.View { get; set; }
        
        #endregion
    }
}
```


You have only seen the models and you already know how they are built, without knowing the exact function in detail

# How does Frankenstein solve the problems mentioned above?

* *how can I create a general solution for all my projects?*
  * Although Frankenstein and this example project is a Unity project, it is not mandatory to use Unity. Once you understand it, it can be implemented in any programming language. 
  * Since each function / entity stands alone as a small unit, it doesn't matter in which project it is used, whether it is an FPS game or something else.
  * You can decide for yourself what level of abstraction you want to give the entity and thus how general it is in relation to all your projects.
  
* *how can I write maintainable, clean and extensible code?*
  * Models get more functionality by adding entities or by extending them. 
  * Models have very little own code.
  * There are no hard references. Models don't know each other. Only a controller knows one entity. Entities can offer each other their services. The layers are clearly separated.
  * Since each entity stands alone, there are fewer cross connections and less spaghetti code.

  
* *how can I better control changes and effects in my code?*
    * The way entities are structured, they always represent only one descriptive layer.  All your changes will be more predictable. 
    * All interface implementations are explicit, so any change must go through the whole project and is not lost.

  
# Q&A
* How can I notify other models or move data between them?
Look at the IQueryable Entity. Each one that implements it is collected by it in a list and made available for searching. Here the singleton pattern is used to interact with all models. Later I will discuss such entities in detail.
* I want to see more examples.
Look at Frankenstein.Controls *Assets/Frankenstein-Controls/Framework*
