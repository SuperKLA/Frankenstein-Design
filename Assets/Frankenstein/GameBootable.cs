using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Frankenstein
{
    public class GameBootable : MonoBehaviour
    {
        public IoCContainer IoC;

        public virtual void StartGame()
        {
        }

        public virtual void BootGame()
        {
        }

        public virtual void Setup()
        {
        }
    }
}