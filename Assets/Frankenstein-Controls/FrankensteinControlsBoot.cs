using FloatingNutshell.Controls.SaveGame.Controller;
using FloatingNutshell.Controls.SaveGame.Entities;
using Frankenstein.Controls.Controller;
using Frankenstein.Controls.Entities;
using MayorMoon.Controls.Controller;
using MayorMoon.Controls.SaveGame.Entities;

namespace Frankenstein.Controls
{
    public class FrankensteinControlsBoot : IAPIBoot
    {
        public static IAPIBoot Create()
        {
            return new FrankensteinControlsBoot();
        }

        void IAPIBoot.Boot(IoCContainer container)
        {
            #region Camera

            container.Register<ICameraService>(() => new CameraController());
            container.Register<ICameraSizeService>(() => new CameraSizeController());
            container.Register<IMainCameraService>(() => new MainCameraController()).AsSingleton();

            #endregion


            #region Framework

            container.Register<ICoroutineService>(() => new CoroutineController());
            container.Register<IGameProgressLoaderService>(() => new GameProgressLoaderController()).AsSingleton();
            container.Register<IQueryableService>(() => new QueryController());
            container.Register<ISaveGameWriterService>(() => new SaveGameWriterController());
            container.Register<ISceneService>(() => new SceneController());

            #endregion


            #region Framework - SaveGame

            container.Register<ISaveGameService>(() => new SaveGameController());
            container.Register<ISaveGamePatcherService>(() => new SaveGamePatcherController());
            container.Register<ISaveGameProviderService>(() => new SaveGameProvider());
            container.Register<ISaveGameSourceProviderService>(() => new SaveGameSourceProvider());

            #endregion


            #region Input

            container.Register<IFingerScriptService>(() => new FingerScriptController()).AsSingleton();
            container.Register<IGestureInputService>(() => new GestureInputController());
            container.Register<ITouch2DRayService>(() => new TouchRay2DController());
            container.Register<ITouch3DRayService>(() => new TouchRay3DController());
            container.Register<IJoyStickService>(() => new JoyStickController());
            container.Register<IMoveByPanService>(() => new MoveByPanController());
            container.Register<IMeshClickableService>(() => new MeshClickableController());

            #endregion


            #region Sprites

            container.Register<ISpriteClickableService>(() => new SpriteClickableController());

            #endregion
        }
    }
}