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
using System.Collections.Generic;

namespace XDot.Events
{
    /// <summary>
    /// Base Game Field (Reactive Field)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GameField<T> : IGameField<T>
    {
        private readonly List<Action<T>> _listeners = new List<Action<T>>();
        private readonly bool _invokeOnAdd = false;
        
        private T _fieldData;
        
        /// <summary>
        /// Game Field Constructor
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <param name="invokeOnAdd"></param>
        public GameField(T defaultValue = default, bool invokeOnAdd = false)
        {
            _invokeOnAdd = invokeOnAdd;
            _fieldData = defaultValue;
        }
        
        ~GameField()
        {
            RemoveAllListeners();
        }

        /// <summary>
        /// Add Listener
        /// </summary>
        /// <param name="listener"></param>
        public void AddListener(Action<T> listener)
        {
            _listeners.Add(listener);
            if (_invokeOnAdd)
                listener.Invoke(Value());
        }
        
        /// <summary>
        /// Remove Listener
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveListener(Action<T> listener)
        {
            if (_listeners.Contains(listener))
                _listeners.Remove(listener);
        }

        /// <summary>
        /// Remove All Listeners
        /// </summary>
        public void RemoveAllListeners()
        {
            _listeners.Clear();
        }
        
        /// <summary>
        /// Update Value
        /// </summary>
        /// <param name="data"></param>
        public void Update(T data)
        {
            _fieldData = data;

            // Cleanup Null Listeners
            CleanupListeners();

            // Invoke Data
            for (int i = 0; i < _listeners.Count; i++)
            {
                if (_listeners.Count > i && _listeners[i] != null)
                    _listeners[i]?.Invoke(data);
            }
        }
        
        /// <summary>
        /// Get Value
        /// </summary>
        /// <returns></returns>
        public T Value()
        {
            return _fieldData;
        }

        /// <summary>
        /// Cleanup Listeners
        /// </summary>
        private void CleanupListeners()
        {
            _listeners.RemoveAll(item => item == null);
        }
    }
}