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

using UnityEngine;

namespace Demo.Player.Handlers
{
    /// <summary>
    /// Player Movement Handler
    /// </summary>
    public struct PlayerMovement
    {
        public Vector3 Direction;
        public Vector3 Motion;
        public float JumpVelocity;
        public float Gravity;
    }
}