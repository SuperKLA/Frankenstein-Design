using ExampleGame.Entities;
using Frankenstein;
using UnityEngine;

namespace ExampleGame.Views
{
    public class GameArenaWinTrigger : APIViewBehaviour<IGameArenaService>
    {
        private void OnTriggerEnter(Collider other)
        {
            this.Service.OnWinTriggerActivate();
        }
    }
}