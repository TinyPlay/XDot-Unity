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
    /// Base Listener Interface
    /// </summary>
    public interface IListener
    {
        /// <summary>
        /// Invoke Listener
        /// </summary>
        /// <param name="inverted"></param>
        void Invoke(bool inverted);
    }
    
    /// <summary>
    /// Listener Interface with Args
    /// </summary>
    /// <typeparam name="TArgs"></typeparam>
    public interface IListener<TArgs>
    {
        /// <summary>
        /// Invoke Listener
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="inverted"></param>
        void Invoke(TArgs arguments, bool inverted);
    }
}