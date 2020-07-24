using System;
using ExampleGame.DTO;
using Frankenstein;
using Frankenstein.Controls;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ExampleGame
{
    public class GameSetup : APISetup, IGameDTO
    {
        public Camera EditorCamera;

        public GameConfigDTO _gameConfig;

        public GameConfigDTO GameConfig
        {
            get => this._gameConfig;
            set => this._gameConfig = value;
        }


        void Start()
        {
            try
            {
                this.Setup();

                if (this.EditorCamera != null) this.EditorCamera.gameObject.SetActive(false);

                Addressables.InitializeAsync();

                this.BootEntities();

                new GameArena().Boot();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        void BootEntities()
        {
            this.context.IoC.Register<IGameDTO>(() => this);

            FrankensteinControlsBoot.Create().Boot(this.context.IoC); //Frankenstein Boot
            GameBoot.Create().Boot(this.context.IoC);                 //your own game boot
        }
    }
}