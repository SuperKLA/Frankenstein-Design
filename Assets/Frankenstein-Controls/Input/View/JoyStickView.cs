using Frankenstein;
using Frankenstein.Controls.Controller;
using Frankenstein.Controls.Entities;
using UnityEngine;

namespace Frankenstein.Controls.Views
{
    public class JoyStickView : APIViewBehaviour<IJoyStickService>, IJoyStickView
    {
        public VariableJoystick joystick;
    }
}