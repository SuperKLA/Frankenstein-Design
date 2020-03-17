using System;
using Frankenstein;

namespace FloatingNutshell.Controls.SaveGame.Entities
{
    public interface ISaveGameSourceProvider : IAPIEntity<ISaveGameSourceProviderService>
    {
        string SaveGameSourceKey { get; }
        bool UseSocialCloudData { get; }
        
        string SonFileName { get; }
        string FatherFileName { get; }
        string GrandFatherFileName { get; }
        
        TimeSpan CreationTimeSpan { get; }
        TimeSpan MinRunTime { get; }
        TimeSpan TimeToSaveFather { get; }
        TimeSpan TimeToSaveGrandFather { get; }
    }

    public interface ISaveGameSourceProviderService : IAPIEntityService
    {
        bool SourceSuccess { get; }
        bool HasSource { get; }
        
        byte[] Get();
        bool Set(byte[] value);
    }
}