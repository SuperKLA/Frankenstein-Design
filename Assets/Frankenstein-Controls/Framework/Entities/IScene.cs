﻿using System.Collections.Generic;
 using System.Threading.Tasks;
 using UnityEngine;

 namespace Frankenstein.Controls.Entities
{
    public interface IScene : IAPIEntity<ISceneService>
    {
    }

    public interface ISceneService : IAPIEntityService
    {
        IList<T> GetViews<T>();
        Task LoadScene(string name);
        void CreateScene(string name);
        T GetView<T>();

        void Hide();
        void Show();
        void SetAsMain();
        void AddObjectToScene(GameObject obj);
        
        string Name { get; }
    }
}