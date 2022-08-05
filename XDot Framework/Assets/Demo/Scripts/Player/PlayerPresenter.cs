//=====================================================
//  XDot Framework for Unity
//  
//  Developed by Ilya Rastorguev (Pixel Incubator)
//  Provided MIT license
//  
//  @version        1.0.0
//  @url            https://github.com/TinyPlay/XDot-Unity
//  @website        https://pixinc.club/
//=====================================================

using System;
using Demo.Input.Handlers;
using Demo.Player.Handlers;
using XDot.Core;
using UnityEngine;
using XDot.ContentLoader;
using XDot.Events;
using XDot.Handlers;

namespace Demo.Player
{
    /// <summary>
    /// Base Player Controller
    /// </summary>
    internal class PlayerPresenter : BasePresenter
    {
        public struct Context : IContext { }

        // Initialization Event
        public GameEvent Initialized = new GameEvent();

        private bool _isLoaded = false;
        private GameObject _playerObject;
        private float _deltaTime = 0f;

        private readonly GameEvent<PlayerMovement> _playerMovement = new GameEvent<PlayerMovement>();
        private Vector3 _playerPosition = Vector3.zero;
        private Quaternion _playerRotation = Quaternion.identity;
        private float gravityValue = -9.81f;

        /// <summary>
        /// Player Presenter Constructor
        /// </summary>
        /// <param name="context"></param>
        public PlayerPresenter(Context context) : base(context)
        {
            // Load Model
            PlayerModel playerModel = new PlayerModel();
            SetModel(playerModel);
            LoadModel<PlayerModel>(Application.persistentDataPath+"/PlayerSettings.dat", handler =>
            {
                Debug.Log($"Failed to load Player Model. {handler.Message}");
            });
            
            // Load View
            LoadView("Demo/PlayerView", LoaderType.Resources, OnViewLoaded, handler => throw new Exception($"Failed to load Player View. {handler.Message}"));

            // Bind GameLoop Events
            EventContainer.Get<GameEvent<UpdateHandler>>().AddListener(OnUpdateHandler);
            EventContainer.Get<GameEvent<InputHandler>>().AddListener(OnInputHandler);
            EventContainer.Bind(_playerMovement);
        }
        
        /// <summary>
        /// On Player View Loaded
        /// </summary>
        /// <param name="viewObject"></param>
        private void OnViewLoaded(GameObject viewObject)
        {
            // Create Instance of Camera
            _playerObject = GameObject.Instantiate(viewObject);
            
            // Setup Camera View
            PlayerView playerView = _playerObject.GetComponent<PlayerView>();
            playerView.SetContext(new PlayerView.Context
            {
                Position = GetModel<PlayerModel>().Position,
                Rotation = GetModel<PlayerModel>().Rotation
            });
            SetView(playerView);
            
            _isLoaded = true;
            Initialized?.Invoke();
        }
        
        /// <summary>
        /// On GameLoop Update
        /// </summary>
        /// <param name="updateData"></param>
        private void OnUpdateHandler(UpdateHandler updateData)
        {
            if(!_isLoaded)
                return;

            _deltaTime = updateData.DeltaTime;
        }

        /// <summary>
        /// Input Handler Recieved
        /// </summary>
        /// <param name="gameInput"></param>
        private void OnInputHandler(InputHandler gameInput)
        {
            if(!_isLoaded)
                return;

            Vector3 direction = new Vector3(gameInput.movementAxis.x, 0, gameInput.movementAxis.y);
            Vector3 motion = direction * _deltaTime * GetModel<PlayerModel>().Speed;
            float jumpVelocity = 0f;
            if(gameInput.jumpButtonDown)
                jumpVelocity = Mathf.Sqrt(GetModel<PlayerModel>().JumpHeight * -3.0f * gravityValue);
            
            _playerMovement?.Invoke(new PlayerMovement
            {
                Direction = direction,
                Motion = motion,
                JumpVelocity = jumpVelocity,
                Gravity = gravityValue
            });
        }
    }
}