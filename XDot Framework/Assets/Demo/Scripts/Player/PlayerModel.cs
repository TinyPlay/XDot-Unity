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
using XDot.Core;

namespace Demo.Player
{
    /// <summary>
    /// Player Model
    /// </summary>
    [System.Serializable]
    internal class PlayerModel : BaseModel
    {
        public float Speed = 3f;
        public float JumpHeight = 1f;

        public Vector3 Position = Vector3.zero;
        public Quaternion Rotation = Quaternion.identity;
    }
}