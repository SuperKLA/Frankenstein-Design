using System;
using Frankenstein;
using UnityEngine;

namespace Frankenstein.Controls.Entities
{
    public interface ISaveGameWriter : IAPIEntity<ISaveGameWriterService>
    {
        
    }

    public interface ISaveGameWriterService : IAPIEntityService
    {
        
    }
}