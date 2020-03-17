using Frankenstein;

namespace Frankenstein.Controls.Entities
{
    public interface IGameProgressLoader : IAPIEntity<IGameProgressLoaderService>
    {
        void OnLoadStarted();
        void OnLoadCompleted();
        void OnUserLaunch();
        void OnReportProgressLoading(float f);
        void OnLoginSuccess();
        void OnEnterLoadingScreen();
        void OnLeavingLoadingScreen();
    }

    public interface IGameProgressLoaderService : IAPIEntityService
    {
        bool IsGameLaunched { get; }
        
        void TriggerLoadStarted();
        void TriggerLoadComplete();
        void TriggerUserLaunch();
        void TriggerLoginSuccess();
        
        void TriggerEnterLoadingScreen();
        void TriggerLeavingLoadingScreen();

        void ReportProgressLoading(float t);
    }
}