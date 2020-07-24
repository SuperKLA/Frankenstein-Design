using System;
using System.Threading.Tasks;
using Frankenstein;
using Frankenstein.Controls.Entities;
using Frankenstein.Controls.Views;
using UnityEngine;

namespace Frankenstein.Controls.Controller
{
    internal class SaveGameWriterController : APIController<ISaveGameWriter>, ISaveGameWriterService
    {
        protected override void OnEntityCreated(ISaveGameWriter entity)
        {
            
        }

        public override  void CreateView()
        {

        }

        protected override  void OnEntityDestroy(ISaveGameWriter entity)
        {
             base.OnEntityDestroy(entity);
        }
    }
}
