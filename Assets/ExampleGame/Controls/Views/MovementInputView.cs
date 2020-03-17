using ExampleGame.Entities;
using Frankenstein;
using UnityEngine;

namespace ExampleGame.Views
{
    public class MovementInputView : APIViewBehaviour<ICharacterFigureMovementService>
    {
        private void Update()
        {
            if (this.Service == null) return;
            
            if (Input.GetKey(KeyCode.A))
                this.Service.Move(Vector3.left);
            if (Input.GetKey(KeyCode.D))
                this.Service.Move(Vector3.right);
            if (Input.GetKey(KeyCode.W))
                this.Service.Move(Vector3.forward);
            if (Input.GetKey(KeyCode.S))
                this.Service.Move(Vector3.back);
        }
    }
}