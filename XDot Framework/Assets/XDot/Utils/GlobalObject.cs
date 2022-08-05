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

namespace XDot.Utils
{
    /// <summary>
    /// Simple Util for Global Mono Objects
    /// </summary>
    public class GlobalObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}