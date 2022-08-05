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
using Demo.Camera;
using Demo.Input;
using Demo.Player;
using UnityEngine;
using XDot.Core;

namespace Demo
{
    /// <summary>
    /// Base Scene Installer Demonstration
    /// for XDot Framework
    /// </summary>
    internal class SceneInstaller : BaseInstaller
    {
        private InputPresenter _input;
        private PlayerPresenter _player;
        private CameraPresenter _camera;
        
        /// <summary>
        /// All Initializations for General Objects
        /// Must be declared at Scene Start in the General Installer
        /// </summary>
        public override void OnSceneStart()
        {
            // Initialize Input Presenter
            _input = new InputPresenter(new InputPresenter.Context { });
            
            // Initialize Character Presenter
            _player = new PlayerPresenter(new PlayerPresenter.Context { });
            _player.Initialized.AddListener(InitializeCamera);
        }

        /// <summary>
        /// Initialize Camera
        /// </summary>
        private void InitializeCamera()
        {
            _camera = new CameraPresenter(new CameraPresenter.Context
            {
                Target = _player.GetView<PlayerView>().GetViewTransform()
            });
        }
    }
}