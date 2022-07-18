using Demo.Camera;
using UnityEngine;
using XDot.Core;

namespace Demo
{
    internal class SceneInstaller : BaseInstaller, ISceneInstaller
    {
        public override void OnSceneStart()
        {
            CameraPresenter camera = new CameraPresenter(new CameraPresenter.Context
            {
                MyMagic = "BlaBlaBla"
            });
            
        }
    }
}