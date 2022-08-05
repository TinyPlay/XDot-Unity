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
using Demo.Camera.Handlers;
using UnityEngine;
using XDot.ContentLoader;
using XDot.Core;
using XDot.Events;
using XDot.Handlers;

namespace Demo.Camera
{
    /// <summary>
    /// Base Camera Presenter Demonstration
    /// for XDot Framework
    /// </summary>
    internal class CameraPresenter : BasePresenter
    {
        public struct Context : IContext
        {
            public Transform Target;            // Camera Target
        }

        private bool _isLoaded = false;
        private GameObject _cameraObject;

        private readonly GameEvent<CameraHandler> _cameraUpdate = new GameEvent<CameraHandler>();
        private Vector3 _camPosition = Vector3.zero;
        private Quaternion _camRotation = Quaternion.identity;
        
        /// <summary>
        /// Presenter Constructor
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="Exception"></exception>
        public CameraPresenter(Context context) : base(context)
        {
            // Load Model
            CameraModel cameraModel = new CameraModel();
            SetModel(cameraModel);
            LoadModel<CameraModel>(Application.persistentDataPath+"/CameraSettings.dat", handler =>
            {
                Debug.Log($"Failed to load Camera Model. {handler.Message}");
            });
            
            // Load View
            LoadView("Demo/CameraView", LoaderType.Resources, OnViewLoaded, handler => throw new Exception($"Failed to load Camera View. {handler.Message}"));
            
            // Bind GameLoop Events
            EventContainer.Get<GameEvent<FixedUpdateHandler>>().AddListener(OnFixedUpdateHandler);
            EventContainer.Bind(_cameraUpdate);
        }

        /// <summary>
        /// On Camera View Loaded
        /// </summary>
        /// <param name="viewObject"></param>
        private void OnViewLoaded(GameObject viewObject)
        {
            // Create Instance of Camera
            _cameraObject = GameObject.Instantiate(viewObject);
            
            // Setup Camera View
            CameraView camView = _cameraObject.GetComponent<CameraView>();
            camView.SetContext(new CameraView.Context
            {
                BasePosition = new CameraHandler
                {
                    Position = _camPosition,
                    Rotation = _camRotation
                }
            });
            SetView(camView);
            
            _isLoaded = true;
        }

        /// <summary>
        /// Gameloop Update
        /// </summary>
        /// <param name="updateData"></param>
        private void OnFixedUpdateHandler(FixedUpdateHandler updateData)
        {
            Transform currentTarget = GetContext<Context>().Target;
            if(currentTarget==null || !_isLoaded)
                return;
            
            Vector3 dirFromTarget = Quaternion.Euler(new Vector3(GetModel<CameraModel>().CameraAngle, 0f, 0f)) * Vector3.back;
            _camPosition = currentTarget.position + dirFromTarget * GetModel<CameraModel>().CameraDistance;
            _camRotation = Quaternion.LookRotation(-dirFromTarget, Vector3.up);
            _cameraUpdate?.Invoke(new CameraHandler
            {
                Position = _camPosition,
                Rotation = _camRotation
            });
        }
    }
}