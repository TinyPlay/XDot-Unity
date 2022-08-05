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

namespace XDot.Events
{
    /// <summary>
    /// Base Invoker Interface
    /// </summary>
    public interface IInvoker
    {
        void AddListener(Action listener);
        void RemoveListener(Action listener);
    }
    
    /// <summary>
    /// Base Invoker Interface with Arguments
    /// </summary>
    /// <typeparam name="TArgs"></typeparam>
    public interface IInvoker<TArgs>
    {
        void AddListener(Action<TArgs> listener);
        void RemoveListener(Action<TArgs> listener);
    }
}