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
    /// Base Game Reactive Field Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGameField<T> : IEvent
    {
        void AddListener(Action<T> listener);
        void RemoveListener(Action<T> listener);
        void Update(T arguments);
        T Value();
    }
}