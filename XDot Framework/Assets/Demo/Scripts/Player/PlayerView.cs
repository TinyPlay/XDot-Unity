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

using Demo.Player.Handlers;
using UnityEngine;
using XDot.Core;
using XDot.Events;

namespace Demo.Player
{
    /// <summary>
    /// Player View
    /// </summary>
    internal class PlayerView: BaseView
    {
        public struct Context : IContext
        {
            public Vector3 Position;
            public Quaternion Rotation;
        }
        private Context _context;

        [SerializeField] private CharacterController _controller;
        
        private bool groundedPlayer;
        private Vector3 playerVelocity;
        
        /// <summary>
        /// Set Player View Context
        /// </summary>
        /// <param name="context"></param>
        public void SetContext(Context context)
        {
            _context = context;
            
            // Setup Base Position and Rotation
            transform.SetPositionAndRotation(_context.Position, _context.Rotation);
            
            // Add Handlers
            EventContainer.Get<GameEvent<PlayerMovement>>()?.AddListener(OnPlayerMovement);
        }
        
        /// <summary>
        /// Unbind Events on Destroy
        /// </summary>
        private void OnDestroy()
        {
            EventContainer.Unbind(EventContainer.Get<GameEvent<PlayerMovement>>());
        }
        
        /// <summary>
        /// On Player Movement
        /// </summary>
        /// <param name="playerPosition"></param>
        private void OnPlayerMovement(PlayerMovement playerPosition)
        {
            if(_controller == null)
                return;
            
            // Check Player Velocity
            groundedPlayer = _controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
            
            // Move Player View
            _controller.Move(playerPosition.Motion);
            if (playerPosition.Direction != Vector3.zero)
            {
                gameObject.transform.forward = playerPosition.Direction;
            }
            
            // Jumping Movement
            if (playerPosition.JumpVelocity>0f && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(playerPosition.JumpVelocity);
            }
            
            playerVelocity.y += playerPosition.Gravity * Time.deltaTime;
            _controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}