using ExampleGame.Entities;
using Frankenstein;
using UnityEngine;

namespace ExampleGame.Views
{
    public class CharacterFigureView : APIViewBehaviour<ICharacterFigureService>, ICharacterFigureView
    {
        public Rigidbody OwnRigidBody;
        public Transform Root => this.transform;

        public void Move(Vector3 planeDir)
        {
            OwnRigidBody.AddForce(planeDir);
        }
    }
}