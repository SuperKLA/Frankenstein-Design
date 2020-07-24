using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Frankenstein.Controls.Entities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Frankenstein.Controls.Controller
{
    public class SceneController : APIController<IScene>, ISceneService
    {
        private SceneInstance _sceneInstance;
        private Scene         _scene;
        private GameObject[]  _roots;

        #region Controller

        protected override void OnEntityCreated(IScene entity)
        {
        }

        protected override void OnEntityDestroy(IScene entity)
        {
            base.OnEntityDestroy(entity);

            if (this._sceneInstance.Scene.IsValid())
            {
                Addressables.UnloadSceneAsync(this._sceneInstance);
            }
            else
            {
                var isDone = SceneManager.UnloadSceneAsync(this._scene);
            }

            this._roots = null;
        }

        #endregion


        #region ISceneService

        string ISceneService.Name => this._scene.name;


        void ISceneService.CreateScene(string name)
        {
            this._scene = SceneManager.CreateScene(name);
            this._roots = new GameObject[0];
        }

        void ISceneService.LoadScene(string name, Action loaded)
        {
            this._scene = SceneManager.GetSceneByName(name);

            if (!this._scene.IsValid())
            {
                SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive).completed+= operation =>
                {
                    this._scene = SceneManager.GetSceneByName(name);
                    this._roots = this._scene.GetRootGameObjects();
                    loaded();
                };
            }
            else
            {
                this._roots = this._scene.GetRootGameObjects();
                loaded();
            }
        }

        IList<T> ISceneService.GetViews<T>()
        {
            var result = new List<T>();
            for (int c = 0; c < this._roots.Length; c++)
            {
                var go     = this._roots[c];
                var childs = go.GetComponentsInChildren<T>(true);
                if (result != null && childs.Length > 0)
                {
                    result.AddRange(childs);
                }
            }

            return result;
        }

        T ISceneService.GetView<T>()
        {
            for (int c = 0; c < this._roots.Length; c++)
            {
                var go     = this._roots[c];
                var result = go.GetComponentInChildren<T>(true);
                if (result != null)
                {
                    return result;
                }
            }

            return default(T);
        }

        void ISceneService.Hide()
        {
            for (int c = 0; c < this._roots.Length; c++)
            {
                this._roots[c].SetActive(false);
            }
        }

        void ISceneService.Show()
        {
            for (int c = 0; c < this._roots.Length; c++)
            {
                this._roots[c].SetActive(true);
            }
        }

        void ISceneService.SetAsMain()
        {
            if (this._scene.IsValid())
            {
                SceneManager.SetActiveScene(this._scene);
            }
        }

        void ISceneService.AddObjectToScene(GameObject obj)
        {
            SceneManager.MoveGameObjectToScene(obj, this._scene);
        }

        #endregion
    }
}