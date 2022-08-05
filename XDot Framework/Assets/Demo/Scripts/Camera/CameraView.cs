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
using XDot.Core;
using XDot.Events;

namespace Demo.Camera
{
    /// <summary>
    /// Base Camera View Demonstration
    /// for XDot Framework
    /// </summary>
    internal class CameraView : BaseView
    {
        public struct Context : IContext
        {
            public CameraHandler BasePosition;
        }
        private Context _context;

        /// <summary>
        /// Set Camera View Context
        /// </summary>
        /// <param name="context"></param>
        public void SetContext(Context context)
        {
            _context = context;
            
            // Setup Base Position
            transform.SetPositionAndRotation(_context.BasePosition.Position, _context.BasePosition.Rotation);

            // Add Handlers
            EventContainer.Get<GameEvent<CameraHandler>>()?.AddListener(OnCameraUpdate);
        }

        /// <summary>
        /// Unbind Events on Destroy
        /// </summary>
        private void OnDestroy()
        {
            EventContainer.Unbind(EventContainer.Get<GameEvent<CameraHandler>>());
        }

        /// <summary>
        /// On Camera Update
        /// </summary>
        /// <param name="cameraData"></param>
        private void OnCameraUpdate(CameraHandler cameraData)
        {
            transform.SetPositionAndRotation(cameraData.Position, cameraData.Rotation);
        }
    }
}