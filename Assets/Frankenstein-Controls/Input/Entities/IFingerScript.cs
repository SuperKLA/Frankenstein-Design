using Frankenstein;
using UnityEngine;

namespace Frankenstein.Controls.Entities
{
    public interface IFingerScript : IAPIEntity<IFingerScriptService, IFingerScriptView>
    {
        
    }

    public interface IFingerScriptService : IAPIEntityService
    {

    }

    public interface IFingerScriptView : IAPIEntityView
    {
        GameObject Container { get; }
    }
}