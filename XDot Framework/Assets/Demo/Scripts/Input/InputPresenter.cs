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

using Demo.Input.Handlers;
using XDot.Core;
using XDot.Events;
using XDot.Handlers;
using UnityEngine;

namespace Demo.Input
{
    /// <summary>
    /// Base Input Presenter
    /// </summary>
    internal class InputPresenter : BasePresenter
    {
        public struct Context : IContext { }
        private readonly GameEvent<InputHandler> _inputHandler = new GameEvent<InputHandler>();
        
        /// <summary>
        /// Input Presenter Constructor
        /// </summary>
        /// <param name="context"></param>
        public InputPresenter(Context context) : base(context)
        {
            // Bind GameLoop Events
            EventContainer.Get<GameEvent<UpdateHandler>>().AddListener(OnUpdateHandler);
            EventContainer.Bind(_inputHandler);
        }

        /// <summary>
        /// Update Loop Handler
        /// </summary>
        /// <param name="updateData"></param>
        private void OnUpdateHandler(UpdateHandler updateData)
        {
            float horizontal = UnityEngine.Input.GetAxis("Horizontal");
            float vertical = UnityEngine.Input.GetAxis("Vertical");
            bool jumpButton = UnityEngine.Input.GetKeyDown(KeyCode.Space);
            
            _inputHandler?.Invoke(new InputHandler
            {
                movementAxis = new Vector2(horizontal,vertical),
                jumpButtonDown = jumpButton
            });
        }
    }
}