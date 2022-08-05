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
using UnityEngine;
using XDot.Events;
using XDot.Handlers;

namespace XDot.Core
{
    /// <summary>
    /// Base Scene Installer Class
    /// with Mono realisation
    /// </summary>
    internal class BaseInstaller : MonoBehaviour, ISceneInstaller
    {
        // Add General Events
        private readonly GameEvent<ApplicationStartHandler> ApplicationStartEvent = new GameEvent<ApplicationStartHandler>();
        private readonly GameEvent<ApplicationQuitHandler> ApplicationQuitHandler = new GameEvent<ApplicationQuitHandler>();
        private readonly GameEvent<ApplicationErrorHandler> ApplicationErrorHandler = new GameEvent<ApplicationErrorHandler>();
        
        private readonly GameEvent<FixedUpdateHandler> FixedUpdateLoopHandler = new GameEvent<FixedUpdateHandler>();
        private readonly GameEvent<UpdateHandler> UpdateLoopHandler = new GameEvent<UpdateHandler>();
        
        /// <summary>
        /// Awake for All Installers
        /// </summary>
        private void Awake()
        {
            // Bind Base Events
            EventContainer.Bind(ApplicationStartEvent);
            EventContainer.Bind(ApplicationQuitHandler);
            EventContainer.Bind(FixedUpdateLoopHandler);
            EventContainer.Bind(UpdateLoopHandler);
            
            // Get Application Log Handler
            Application.logMessageReceived += HandleAppLog;

            // Call Virtual Method
            OnSceneAwake();
        }
        public virtual void OnSceneAwake(){}

        /// <summary>
        /// On Destroy
        /// </summary>
        private void OnDestroy()
        {
            Application.logMessageReceived -= HandleAppLog;
        }

        /// <summary>
        /// Handle Application Log
        /// </summary>
        /// <param name="logString"></param>
        /// <param name="stackTrace"></param>
        /// <param name="type"></param>
        private void HandleAppLog(string logString, string stackTrace, LogType type)
        {
            if (type == LogType.Error || type == LogType.Exception)
            {
                ApplicationErrorHandler?.Invoke(new ApplicationErrorHandler
                {
                    ErrorType = (type == LogType.Error)?ErrorType.Error:ErrorType.Exception,
                    StackTrace = stackTrace,
                    Message = logString
                });
            }
        }

        /// <summary>
        /// Start for All Installers
        /// </summary>
        private void Start()
        {
            ApplicationStartEvent?.Invoke(new ApplicationStartHandler
            {
                Time = DateTime.Now
            });
            OnSceneStart();
        }
        public virtual void OnSceneStart(){}

        /// <summary>
        /// On Application Quit
        /// </summary>
        private void OnApplicationQuit()
        {
            ApplicationQuitHandler?.Invoke(new ApplicationQuitHandler
            {
                Time = DateTime.Now
            });
        }

        /// <summary>
        /// On Update Every Frame
        /// </summary>
        private void Update()
        {
            UpdateLoopHandler?.Invoke(new UpdateHandler
            {
                DeltaTime = Time.deltaTime
            });
        }

        /// <summary>
        /// On Fixed Time Update
        /// </summary>
        private void FixedUpdate()
        {
            FixedUpdateLoopHandler?.Invoke(new FixedUpdateHandler
            {
                DeltaTime = Time.deltaTime
            });
        }
    }
}