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

        public override async Task CreateView()
        {

        }

        protected override async Task OnEntityDestroy(ISaveGameWriter entity)
        {
            await base.OnEntityDestroy(entity);
        }
    }
}
