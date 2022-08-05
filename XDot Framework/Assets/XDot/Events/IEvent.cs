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
namespace XDot.Events
{
    /// <summary>
    /// Base Event Interface
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Remove All Listeners
        /// </summary>
        void RemoveAllListeners();
    }
}