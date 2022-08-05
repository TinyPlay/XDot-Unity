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

using System.Collections.Generic;

namespace XDot.Events
{
    /// <summary>
    /// General Event Container for Bindings
    /// </summary>
    public class EventContainer
    {
        private static readonly List<IEvent> Events = new List<IEvent>();

        /// <summary>
        /// Bind Event / Field to Container
        /// </summary>
        /// <param name="eventData"></param>
        /// <typeparam name="TEvent"></typeparam>
        public static void Bind<TEvent>(TEvent eventData) where TEvent : IEvent
        {
            if (!Events.Contains(eventData))
                Events.Add(eventData);
        }

        /// <summary>
        /// Unbind Event / Field from Container
        /// </summary>
        /// <param name="eventData"></param>
        /// <typeparam name="TEvent"></typeparam>
        public static void Unbind<TEvent>(TEvent eventData) where TEvent : IEvent
        {
            if (Events.Contains(eventData))
            {
                eventData.RemoveAllListeners();
                Events.Remove(eventData);
            }
        }

        /// <summary>
        /// Unbind All Events
        /// </summary>
        public static void UnbindAll()
        {
            for (int i = 0; i < Events.Count; i++)
            {
                Events[i].RemoveAllListeners();
            }
            
            Events.Clear();
        }
        
        /// <summary>
        /// Get Event
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <returns></returns>
        public static TEvent Get<TEvent>() where TEvent : class
        {
            //Cleanup Nullable Events
            CleanupEvents();
            
            for (int i = 0; i < Events.Count; i++)
            {
                if (Events[i]!=null && Events[i] is TEvent)
                {
                    return (TEvent)Events[i];
                }
            }

            return null;
        }
        
        /// <summary>
        /// Cleanup Events
        /// </summary>
        private static void CleanupEvents()
        {
            Events.RemoveAll(item => item == null);
        }
    }
}