using Frankenstein;
using Frankenstein.Controls.Entities;
using UnityEngine;

namespace Frankenstein.Controls.Views
{
    public class FingerScriptClient : APIViewBehaviour<IFingerScriptService>, IFingerScriptView
    {
        GameObject IFingerScriptView.Container
        {
            get { return this.gameObject; }
        }
    }
}