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
    /// Base Game Event Interface
    /// </summary>
    public interface IGameEvent : IEvent, IListener, IInvoker { }
    
    /// <summary>
    /// Base Game Event Interface with Arguments
    /// </summary>
    /// <typeparam name="TArgs"></typeparam>
    public interface IGameEvent<TArgs> : IEvent, IListener<TArgs>, IInvoker<TArgs> { }
}