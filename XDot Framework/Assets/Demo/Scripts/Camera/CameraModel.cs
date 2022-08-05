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
using XDot.Core;

namespace Demo.Camera
{
    /// <summary>
    /// Base Camera Model Demonstration
    /// for XDot Framework
    /// </summary>
    [System.Serializable]
    internal class CameraModel : BaseModel
    {
        public float CameraAngle = 45f;
        public float CameraDistance = 10f;
    }
}